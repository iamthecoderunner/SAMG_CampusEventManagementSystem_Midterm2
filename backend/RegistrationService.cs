using System;
using System.Data;
using System.Net.Mail;
using Microsoft.Data.SqlClient;

namespace SAMG.CampusEvents
{
    public class RegistrationResult
    {
        public bool Success { get; }
        public string Message { get; }

        public RegistrationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }

    public class EventInfo
    {
        public int EventId { get; set; }
        public int Capacity { get; set; }
    }

    /*
     * Small data-access contract used by RegistrationService.
     * This keeps the registration business rules testable
     * without requiring a live SQL Server during unit tests.
     */
    public interface IRegistrationRepository
    {
        EventInfo? GetEvent(int eventId);

        int? GetUserIdByEmail(string email);

        int CreateStudent(string fullName, string email);

        bool RegistrationExists(int userId, int eventId);

        int GetRegisteredCount(int eventId);

        void InsertRegistration(int userId, int eventId);
    }

    public class RegistrationService
    {
        private readonly IRegistrationRepository _repository;

        public RegistrationService(IRegistrationRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        public RegistrationResult RegisterStudent(
            string fullName,
            string email,
            int eventId)
        {
            fullName = fullName?.Trim() ?? string.Empty;
            email = email?.Trim() ?? string.Empty;

            RegistrationResult validation =
                ValidateInput(fullName, email, eventId);

            if (!validation.Success)
            {
                return validation;
            }

            try
            {
                EventInfo? selectedEvent =
                    _repository.GetEvent(eventId);

                if (selectedEvent == null)
                {
                    return new RegistrationResult(
                        false,
                        "The selected event does not exist.");
                }

                int? existingUserId =
                    _repository.GetUserIdByEmail(email);

                if (existingUserId.HasValue &&
                    _repository.RegistrationExists(
                        existingUserId.Value,
                        eventId))
                {
                    return new RegistrationResult(
                        false,
                        "This student is already registered for the selected event.");
                }

                int registeredCount =
                    _repository.GetRegisteredCount(eventId);

                if (registeredCount >= selectedEvent.Capacity)
                {
                    return new RegistrationResult(
                        false,
                        "Registration is unavailable because this event is full.");
                }

                int userId;

                if (existingUserId.HasValue)
                {
                    userId = existingUserId.Value;
                }
                else
                {
                    userId = _repository.CreateStudent(
                        fullName,
                        email);
                }

                /*
                 * Final application-level duplicate check.
                 * The database UNIQUE(UserId, EventId) constraint
                 * remains the final integrity safeguard.
                 */
                if (_repository.RegistrationExists(
                    userId,
                    eventId))
                {
                    return new RegistrationResult(
                        false,
                        "This student is already registered for the selected event.");
                }

                _repository.InsertRegistration(
                    userId,
                    eventId);

                return new RegistrationResult(
                    true,
                    "Registration successful.");
            }
            catch
            {
                /*
                 * Do not expose database implementation details,
                 * connection information, or stack traces to the caller.
                 */
                return new RegistrationResult(
                    false,
                    "Registration could not be completed. Please try again.");
            }
        }

        public RegistrationResult ValidateInput(
            string fullName,
            string email,
            int eventId)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return new RegistrationResult(
                    false,
                    "Full name is required.");
            }

            if (fullName.Trim().Length > 150)
            {
                return new RegistrationResult(
                    false,
                    "Full name must not exceed 150 characters.");
            }

            if (!IsValidEmail(email))
            {
                return new RegistrationResult(
                    false,
                    "A valid email address is required.");
            }

            if (email.Trim().Length > 255)
            {
                return new RegistrationResult(
                    false,
                    "Email must not exceed 255 characters.");
            }

            if (eventId <= 0)
            {
                return new RegistrationResult(
                    false,
                    "A valid EventId is required.");
            }

            return new RegistrationResult(
                true,
                "Input is valid.");
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                MailAddress address =
                    new MailAddress(email);

                return address.Address.Equals(
                    email.Trim(),
                    StringComparison.OrdinalIgnoreCase);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }

    /*
     * SQL Server implementation of the repository.
     *
     * The connection string is supplied externally.
     * No database username, password, or other credentials
     * are stored in this source file.
     *
     * All values are supplied to SQL Server through
     * parameters rather than SQL string concatenation.
     */
    public class SqlRegistrationRepository :
        IRegistrationRepository
    {
        private readonly string _connectionString;

        public SqlRegistrationRepository(
            string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException(
                    "A database connection string is required.",
                    nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        public EventInfo? GetEvent(int eventId)
        {
            const string sql = @"
                SELECT EventId, Capacity
                FROM dbo.Events
                WHERE EventId = @EventId;";

            using SqlConnection connection =
                new SqlConnection(_connectionString);

            using SqlCommand command =
                new SqlCommand(sql, connection);

            command.Parameters.Add(
                "@EventId",
                SqlDbType.Int).Value = eventId;

            connection.Open();

            using SqlDataReader reader =
                command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            return new EventInfo
            {
                EventId = reader.GetInt32(0),
                Capacity = reader.GetInt32(1)
            };
        }

        public int? GetUserIdByEmail(string email)
        {
            const string sql = @"
                SELECT UserId
                FROM dbo.Users
                WHERE Email = @Email;";

            using SqlConnection connection =
                new SqlConnection(_connectionString);

            using SqlCommand command =
                new SqlCommand(sql, connection);

            command.Parameters.Add(
                "@Email",
                SqlDbType.NVarChar,
                255).Value = email;

            connection.Open();

            object? result = command.ExecuteScalar();

            if (result == null || result == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt32(result);
        }

        public int CreateStudent(
            string fullName,
            string email)
        {
            const string sql = @"
                INSERT INTO dbo.Users
                    (FullName, Email, Role)
                OUTPUT INSERTED.UserId
                VALUES
                    (@FullName, @Email, @Role);";

            using SqlConnection connection =
                new SqlConnection(_connectionString);

            using SqlCommand command =
                new SqlCommand(sql, connection);

            command.Parameters.Add(
                "@FullName",
                SqlDbType.NVarChar,
                150).Value = fullName;

            command.Parameters.Add(
                "@Email",
                SqlDbType.NVarChar,
                255).Value = email;

            command.Parameters.Add(
                "@Role",
                SqlDbType.VarChar,
                20).Value = "Student";

            connection.Open();

            object? result = command.ExecuteScalar();

            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException(
                    "Student creation did not return a UserId.");
            }

            return Convert.ToInt32(result);
        }

        public bool RegistrationExists(
            int userId,
            int eventId)
        {
            const string sql = @"
                SELECT COUNT(1)
                FROM dbo.Registrations
                WHERE UserId = @UserId
                  AND EventId = @EventId;";

            using SqlConnection connection =
                new SqlConnection(_connectionString);

            using SqlCommand command =
                new SqlCommand(sql, connection);

            command.Parameters.Add(
                "@UserId",
                SqlDbType.Int).Value = userId;

            command.Parameters.Add(
                "@EventId",
                SqlDbType.Int).Value = eventId;

            connection.Open();

            object? result = command.ExecuteScalar();

            return result != null &&
                   result != DBNull.Value &&
                   Convert.ToInt32(result) > 0;
        }

        public int GetRegisteredCount(int eventId)
        {
            const string sql = @"
                SELECT COUNT(1)
                FROM dbo.Registrations
                WHERE EventId = @EventId
                  AND Status = @Status;";

            using SqlConnection connection =
                new SqlConnection(_connectionString);

            using SqlCommand command =
                new SqlCommand(sql, connection);

            command.Parameters.Add(
                "@EventId",
                SqlDbType.Int).Value = eventId;

            command.Parameters.Add(
                "@Status",
                SqlDbType.VarChar,
                20).Value = "Registered";

            connection.Open();

            object? result = command.ExecuteScalar();

            if (result == null || result == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToInt32(result);
        }

        public void InsertRegistration(
            int userId,
            int eventId)
        {
            const string sql = @"
                INSERT INTO dbo.Registrations
                    (UserId, EventId, Status)
                VALUES
                    (@UserId, @EventId, @Status);";

            using SqlConnection connection =
                new SqlConnection(_connectionString);

            using SqlCommand command =
                new SqlCommand(sql, connection);

            command.Parameters.Add(
                "@UserId",
                SqlDbType.Int).Value = userId;

            command.Parameters.Add(
                "@EventId",
                SqlDbType.Int).Value = eventId;

            command.Parameters.Add(
                "@Status",
                SqlDbType.VarChar,
                20).Value = "Registered";

            connection.Open();

            command.ExecuteNonQuery();
        }
    }
}