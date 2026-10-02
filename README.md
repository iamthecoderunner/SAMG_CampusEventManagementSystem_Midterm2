# SAMG_CampusEventManagementSystem_Midterm2# SAMG Online Campus Event Management System

A 4th Year BSIT Laboratory Midterm Examination prototype for an Online Campus Event Management System.

The prototype demonstrates three core functions:

- Students can view upcoming campus events.
- Students can register for an event.
- Administrators can view registered attendees.

## Team Members

- Gio Ilas
- Jelaine San Jose
- Mark Camino
- Shantel De Guzman

## Project Structure

```text
frontend/
  index.html
  styles.css
  script.js

database/
  schema.sql

backend/
  RegistrationService.cs
  SAMG.CampusEvents.csproj

tests/
  RegistrationServiceTests.cs
  SAMG.CampusEvents.Tests.csproj
  MSTestSettings.cs

SUBMISSION.md
```

## Prerequisites

For the frontend:

- Modern web browser
- Optional local HTTP server such as Python 3

For the backend and unit tests:

- .NET SDK compatible with the project target framework

For the database schema:

- Microsoft SQL Server or another environment capable of executing SQL Server-compatible T-SQL

## Run the Frontend

The frontend uses HTML5, CSS3, and vanilla JavaScript.

From the repository root:

```bash
cd frontend
python3 -m http.server 8000
```

Then open:

```text
http://localhost:8000
```

Alternatively, `frontend/index.html` may be opened directly in a browser.

The frontend currently uses mock in-memory JavaScript data for prototype interaction. It is not connected directly to SQL Server or the C# backend.

## Build the Backend

From the repository root:

```bash
dotnet restore backend/SAMG.CampusEvents.csproj
dotnet build backend/SAMG.CampusEvents.csproj
```

The backend uses C# and Microsoft.Data.SqlClient.

No database credentials are hard-coded in the source code. A connection string must be supplied externally when using the SQL Server repository implementation.

## Run the Unit Tests

From the repository root:

```bash
dotnet test tests/SAMG.CampusEvents.Tests.csproj
```

The test suite contains seven registration tests covering:

1. Blank student name
2. Invalid email
3. Invalid EventId
4. Duplicate registration
5. Full event
6. Nonexistent event
7. Valid registration

Expected result:

```text
Test summary: total: 7, failed: 0, succeeded: 7, skipped: 0
```

The tests isolate the registration business logic from SQL Server through a test implementation of `IRegistrationRepository`, so a live database is not required to execute the unit tests.

## Database Setup

The SQL Server-compatible database schema is located at:

```text
database/schema.sql
```

Execute this script in the target SQL Server database.

It creates:

- `dbo.Users`
- `dbo.Events`
- `dbo.Registrations`

The schema includes Primary Keys, Foreign Keys, CHECK constraints, UNIQUE constraints, explicit referential actions, and non-clustered indexes on the Foreign Key columns.

## Important Prototype Scope

The frontend and backend/database components demonstrate the intended architecture but are not connected through an HTTP API in this midterm prototype.

Current behavior is therefore:

```text
Frontend
  -> mock in-memory event and registration data

RegistrationService
  -> registration validation/business logic
  -> SQL Server repository implementation

Unit Tests
  -> RegistrationService
  -> isolated test repository
```

This separation is intentional and is documented in `SUBMISSION.md`.

## Examination Documentation

See:

```text
SUBMISSION.md
```

for the complete:

- Task 1 architecture and AI grounding evaluation
- Task 2 frontend documentation
- Task 3 database design and Mermaid ERD
- Task 4 backend security and unit testing documentation
- Task 5 integration report
- AI disclosure
- Group verification log