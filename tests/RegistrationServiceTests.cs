using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAMG.CampusEvents;

namespace SAMG.CampusEvents.Tests
{
    [TestClass]
    public class RegistrationServiceTests
    {
        [TestMethod]
        public void BlankStudentName_IsRejected()
        {
            FakeRegistrationRepository repository =
                new FakeRegistrationRepository();

            RegistrationService service =
                new RegistrationService(repository);

            RegistrationResult result =
                service.RegisterStudent(
                    "",
                    "student@example.edu",
                    1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(
                "Full name is required.",
                result.Message);
        }

        [TestMethod]
        public void InvalidEmail_IsRejected()
        {
            FakeRegistrationRepository repository =
                new FakeRegistrationRepository();

            RegistrationService service =
                new RegistrationService(repository);

            RegistrationResult result =
                service.RegisterStudent(
                    "Gio Malcolm Ilas",
                    "not-an-email",
                    1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(
                "A valid email address is required.",
                result.Message);
        }

        [TestMethod]
        public void InvalidEventId_IsRejected()
        {
            FakeRegistrationRepository repository =
                new FakeRegistrationRepository();

            RegistrationService service =
                new RegistrationService(repository);

            RegistrationResult result =
                service.RegisterStudent(
                    "Gio Malcolm Ilas",
                    "gio.ilas@example.edu",
                    0);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(
                "A valid EventId is required.",
                result.Message);
        }

        [TestMethod]
        public void DuplicateRegistration_IsRejected()
        {
            FakeRegistrationRepository repository =
                new FakeRegistrationRepository();

            repository.Events[1] =
                new EventInfo
                {
                    EventId = 1,
                    Capacity = 100
                };

            repository.Users["gio.ilas@example.edu"] = 10;

            repository.Registrations.Add((10, 1));

            RegistrationService service =
                new RegistrationService(repository);

            RegistrationResult result =
                service.RegisterStudent(
                    "Gio Malcolm Ilas",
                    "gio.ilas@example.edu",
                    1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(
                "This student is already registered for the selected event.",
                result.Message);
        }

        [TestMethod]
        public void FullEvent_IsRejected()
        {
            FakeRegistrationRepository repository =
                new FakeRegistrationRepository();

            repository.Events[1] =
                new EventInfo
                {
                    EventId = 1,
                    Capacity = 1
                };

            repository.RegisteredCounts[1] = 1;

            RegistrationService service =
                new RegistrationService(repository);

            RegistrationResult result =
                service.RegisterStudent(
                    "Gio Malcolm Ilas",
                    "gio.ilas@example.edu",
                    1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(
                "Registration is unavailable because this event is full.",
                result.Message);
        }

        [TestMethod]
        public void NonexistentEvent_IsRejected()
        {
            FakeRegistrationRepository repository =
                new FakeRegistrationRepository();

            RegistrationService service =
                new RegistrationService(repository);

            RegistrationResult result =
                service.RegisterStudent(
                    "Gio Malcolm Ilas",
                    "gio.ilas@example.edu",
                    999);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(
                "The selected event does not exist.",
                result.Message);
        }

        [TestMethod]
        public void ValidRegistration_Succeeds()
        {
            FakeRegistrationRepository repository =
                new FakeRegistrationRepository();

            repository.Events[1] =
                new EventInfo
                {
                    EventId = 1,
                    Capacity = 100
                };

            repository.RegisteredCounts[1] = 0;

            RegistrationService service =
                new RegistrationService(repository);

            RegistrationResult result =
                service.RegisterStudent(
                    "Gio Malcolm Ilas",
                    "gio.ilas@example.edu",
                    1);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(
                "Registration successful.",
                result.Message);

            Assert.HasCount(
                1,
                repository.Registrations);
        }
    }

    /*
     * Fake repository used only by the unit tests.
     * It allows the business rules to be tested
     * without requiring a live SQL Server.
     */
    public class FakeRegistrationRepository :
        IRegistrationRepository
    {
        public Dictionary<int, EventInfo> Events { get; } =
            new Dictionary<int, EventInfo>();

        public Dictionary<string, int> Users { get; } =
            new Dictionary<string, int>();

        public HashSet<(int UserId, int EventId)> Registrations { get; } =
            new HashSet<(int, int)>();

        public Dictionary<int, int> RegisteredCounts { get; } =
            new Dictionary<int, int>();

        private int _nextUserId = 1;

        public EventInfo? GetEvent(int eventId)
        {
            if (Events.TryGetValue(
                eventId,
                out EventInfo? eventInfo))
            {
                return eventInfo;
            }

            return null;
        }

        public int? GetUserIdByEmail(string email)
        {
            if (Users.TryGetValue(
                email,
                out int userId))
            {
                return userId;
            }

            return null;
        }

        public int CreateStudent(
            string fullName,
            string email)
        {
            int userId = _nextUserId++;

            Users[email] = userId;

            return userId;
        }

        public bool RegistrationExists(
            int userId,
            int eventId)
        {
            return Registrations.Contains(
                (userId, eventId));
        }

        public int GetRegisteredCount(int eventId)
        {
            if (RegisteredCounts.TryGetValue(
                eventId,
                out int count))
            {
                return count;
            }

            return 0;
        }

        public void InsertRegistration(
            int userId,
            int eventId)
        {
            Registrations.Add(
                (userId, eventId));

            int currentCount =
                GetRegisteredCount(eventId);

            RegisteredCounts[eventId] =
                currentCount + 1;
        }
    }
}