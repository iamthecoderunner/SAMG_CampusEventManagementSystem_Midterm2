# SAMG Campus Event Management System - Midterm Submission

## Task 1 - Requirements Analysis & Prompt Architecture

### Exact Prompt Used

ROLE

Act as a Lead Systems Architect designing a practical prototype architecture for a 4th Year BSIT midterm laboratory examination.

CONTEXT

The project is an Online Campus Event Management System.

The system must allow:
- students to view upcoming campus events
- students to register for an event
- administrators to view registered attendees

The project will be completed by a 4-member team within approximately 3 hours.

The shared repository will contain:
- frontend source files
- database schema and SQL scripts
- backend/security-related code
- unit tests
- a consolidated SUBMISSION.md

TASK

Design the overall system architecture for this prototype.

Provide:

1. System overview
2. Main user roles
3. Core functional requirements
4. High-level architecture
5. Major system components
6. Data flow between frontend, backend, and database
7. Recommended core entities
8. Suggested repository structure
9. Integration points between the frontend, database, backend, and testing components
10. A short explanation of why the architecture is realistic for a 3-hour prototype

CONSTRAINTS

- Keep the architecture simple enough for a 3-hour student prototype.
- Use only technologies necessary for the required functionality.
- Do not introduce microservices.
- Do not use third-party state management libraries such as Redux.
- Do not add authentication frameworks, cloud deployment, payment systems, notification services, or other features not required by the scenario.
- Keep the system centered on students viewing events, registering, and administrators viewing attendees.
- Use relational database concepts compatible with a 3NF design.
- Ensure the proposed architecture can support a semantic HTML frontend, SQL database schema, backend validation, and unit testing.
- Avoid unnecessary enterprise-level complexity.

OUTPUT FORMAT

Use the following headings:

## System Overview
## User Roles
## Functional Requirements
## High-Level Architecture
## System Components
## Data Flow
## Core Data Entities
## Repository Structure
## Integration Points
## 3-Hour Prototype Feasibility

### AI-Generated Architecture

## System Overview

The Online Campus Event Management System is a small web-based prototype that allows students to view upcoming campus events and register for an event, while administrators can view the students registered for each event.

The prototype should use a simple three-layer structure:

1. Frontend — displays events and collects registration input.
2. Backend — handles validation and database interaction.
3. Relational Database — stores users, events, and registrations.

The architecture is intentionally limited to the functionality required for the laboratory examination so the four-member team can complete and integrate the project within approximately three hours. The examination scenario specifically requires students to view events and register, while administrators view registered attendees. Laboratory_Midterm_Examination(…

## User Roles

### Student

The Student user should be able to:

- View available or upcoming campus events.
- View basic event information.
- Select an event.
- Submit registration information.
- Receive confirmation that the registration request has been processed.

### Administrator

The Administrator should be able to:

- View campus events.
- Select an event.
- View the students registered for that event.

A complete authentication or authorization framework is outside the required prototype scope and is therefore not necessary for this examination.

## Functional Requirements

The prototype should support the following core functions:

### Event Catalog

The system displays a list of upcoming campus events.

Each event may contain information such as:

- Event title
- Event description
- Event date
- Venue
- Capacity

### Student Registration

A student selects an event and submits the information required for registration.

The system should validate the input before creating the registration.

### Registration Validation

The backend should verify basic rules such as:

- Required fields are supplied.
- Student email is valid according to the chosen validation rule.
- The selected event exists.
- Available seats have not been exceeded.
- Duplicate registration can be prevented where appropriate.

### Administrator Attendee View

The administrator can retrieve the registrations associated with a selected event and view the corresponding student information.

## High-Level Architecture

```text
┌──────────────────────────────┐
│          Frontend            │
│                              │
│ Event Catalog                │
│ Registration Form            │
│ Attendee View                │
│                              │
│ Semantic HTML5 / CSS / JS    │
└──────────────┬───────────────┘
               │
               │ Registration /
               │ Event Requests
               ▼
┌──────────────────────────────┐
│           Backend            │
│                              │
│ Input Validation             │
│ Registration Logic           │
│ Event Queries                │
│ Attendee Queries             │
│ Parameterized DB Access      │
└──────────────┬───────────────┘
               │
               │ SQL
               ▼
┌──────────────────────────────┐
│      Relational Database     │
│                              │
│ Users                        │
│ Events                       │
│ Registrations                │
└──────────────────────────────┘
```

The architecture follows a straightforward separation of presentation, application logic, and data storage. This avoids unnecessary complexity such as microservices or external infrastructure.

## System Components

### 1. Frontend

The frontend is responsible for the user interface.

It should contain:

- Event Catalog
- Student Registration Form
- Registered Attendees View

The frontend should use semantic HTML5 elements such as:

- `<header>`
- `<main>`
- `<section>`
- `<article>`
- `<footer>`

It should also include the accessibility requirements specified for Task 2, including proper form labels, `aria-label` attributes where appropriate, accessible contrast, and image alternative text. Laboratory_Midterm_Examination(…

### 2. Backend

The backend provides the application logic between the user interface and database.

Its responsibilities include:

- Validating registration information.
- Checking event availability.
- Processing registration requests.
- Retrieving attendee information.
- Performing database operations safely.
- Using parameterized SQL queries.
- Disposing database resources correctly.

The examination provides a C# `SqlConnection` example for the security-refactoring exercise, so C# is a practical backend technology for the prototype, although the overall backend framework is not explicitly specified. Laboratory_Midterm_Examination(…

### 3. Database

The relational database stores the persistent system information.

The minimum core entities are:

- Users
- Events
- Registrations

The schema should be designed in Third Normal Form and contain appropriate:

- Primary keys
- Foreign keys
- Foreign-key rules
- CHECK constraints
- Non-clustered indexes on foreign-key columns

These are explicit requirements of Task 3. Laboratory_Midterm_Examination(…

### 4. Unit Tests

Unit tests should verify a core validation routine independently from external dependencies.

Potential validation targets include:

- Student email-domain validation
- Seat-availability validation

Mocks should be used where an external dependency must be isolated, as required by Task 4. Laboratory_Midterm_Examination(…

## Data Flow

### Student Views Events

```text
Student
   ↓
Event Catalog
   ↓
Backend requests event data
   ↓
Events table
   ↓
Backend returns event information
   ↓
Frontend displays events
```

### Student Registers

```text
Student
   ↓
Registration Form
   ↓
Frontend sends registration information
   ↓
Backend validates input
   ↓
Backend checks User and Event data
   ↓
Registration is inserted
   ↓
Registration result returned to frontend
```

### Administrator Views Attendees

```text
Administrator
   ↓
Select Event
   ↓
Backend receives Event ID
   ↓
Registrations joined with Users
   ↓
Attendee information returned
   ↓
Frontend displays registered students
```

## Core Data Entities

### Users

Represents individuals who interact with the system.

Possible attributes:

```text
UserId
FullName
Email
Role
```

### Events

Represents campus events that students may register for.

Possible attributes:

```text
EventId
Title
Description
EventDate
Venue
Capacity
```

### Registrations

Represents the relationship between a user and an event.

Possible attributes:

```text
RegistrationId
UserId
EventId
RegistrationDate
Status
```

The basic relationships are:

```text
Users 1 ───────< Registrations >─────── 1 Events
```

This means:

- One User may have many Registrations.
- One Event may have many Registrations.
- Each Registration references one User.
- Each Registration references one Event.

Task 3 explicitly requires at least three relational entities and gives Users, Events, and Registrations as examples. Laboratory_Midterm_Examination(…

## Repository Structure

A simple repository organization is:

```text
SAMG_CampusEventManagementSystem_Midterm/
│
├── frontend/
│   ├── index.html
│   ├── styles.css
│   └── script.js
│
├── database/
│   └── schema.sql
│
├── backend/
│   ├── RegistrationService.cs
│   └── tests/
│
├── README.md
│
└── SUBMISSION.md
```

The examination explicitly requires frontend code under `/frontend`, the SQL DDL as `/database/schema.sql`, the refactored backend solution under `/backend/RegistrationService.cs`, and a consolidated `SUBMISSION.md` in the repository root. Laboratory_Midterm_Examination(… Laboratory_Midterm_Examination(… Laboratory_Midterm_Examination(…

## Integration Points

### Task 1 → All Tasks

Task 1 establishes the common architecture, scope, entities, terminology, and integration assumptions.

All later tasks should use these shared definitions.

### Task 2 → Task 3

The frontend registration fields should correspond to the database model.

For example:

```text
Frontend                 Database

Student Email    →       Users.Email
Selected Event   →       Registrations.EventId
Student          →       Registrations.UserId
```

### Task 3 → Task 4

The backend should use the same table and column names defined by the database schema.

For example:

```text
Users
Events
Registrations
```

should remain consistent between:

- Mermaid ERD
- `schema.sql`
- backend queries
- unit-test assumptions

### Task 4 → Task 3

Validation logic may depend on database information.

For example, seat-availability validation may compare:

```text
Events.Capacity
```

against the number of existing registrations.

### All Tasks → Task 5

The final `SUBMISSION.md` consolidates:

- team roster
- Task 1 prompt and AI output
- Mermaid ERD
- setup instructions
- AI disclosure
- verification log

The examination requires at least three documented cases where AI-generated work was manually corrected or refined. Laboratory_Midterm_Examination(…

## 3-Hour Prototype Feasibility

This architecture is realistic for a three-hour prototype because it limits the project to the three required business activities: viewing events, registering students, and viewing attendees. It uses only three core relational entities and separates the work into frontend, database, backend/security, and documentation responsibilities that correspond closely to the four team roles. No unnecessary infrastructure such as microservices, external authentication platforms, cloud deployment, or third-party state-management frameworks is required. The architecture therefore provides enough structure to demonstrate system design, database normalization, accessibility, security, unit testing, and AI-assisted development without expanding beyond the scope of the examination.

### Manual Grounding Evaluation

The AI-generated architecture is realistic for a 3-hour team prototype because it limits the system to the required core functions: viewing events, registering students, and viewing registered attendees. The proposed three-layer structure is simple enough to divide across the team while still allowing the frontend, backend, and database tasks to integrate consistently. The use of three core entities—Users, Events, and Registrations—keeps the database scope manageable while supporting the required workflows. Some additional validation rules and data fields were treated as design choices rather than mandatory requirements, so they will be verified during implementation before being finalized.





# Task 3 - Database Design & ERD Generation

## AI Prompt Used

ROLE

Act as a Database Engineer responsible for Task 3 of a 4th Year BSIT midterm laboratory examination.

CONTEXT

The project is an Online Campus Event Management System.

The agreed system baseline is:

Actors:
- Student
- Administrator

Core system functions:
- Students can view upcoming campus events.
- Students can register for an event.
- Administrators can view registered attendees.

The agreed architecture uses:
- Frontend
- Backend
- Relational Database

The agreed core entities are:
- Users
- Events
- Registrations

The database design must remain consistent with the frontend, backend, unit tests, and SUBMISSION.md documentation.

TASK

Design a Third Normal Form (3NF) relational database schema for the Online Campus Event Management System.

Use at least the following relational entities:

1. Users
2. Events
3. Registrations

For each entity, define:
- Primary Key
- attributes
- data types
- nullability
- uniqueness rules where appropriate
- Foreign Keys where applicable
- purpose of each important attribute

Then:

1. Explain why the design satisfies 1NF, 2NF, and 3NF.
2. Define the cardinality between Users, Events, and Registrations.
3. Identify business rules enforced by the schema.
4. Generate a Mermaid.js Entity-Relationship Diagram.
5. Generate a production-grade SQL DDL script.
6. Audit the Mermaid ERD and SQL script against each other.

SQL REQUIREMENTS

The SQL DDL must explicitly include:

- CREATE TABLE statements
- Primary Key constraints
- Foreign Key constraints
- explicit referential actions/rules
- CHECK constraints
- UNIQUE constraints where appropriate
- NOT NULL constraints where appropriate
- non-clustered indexes on Foreign Key columns

Use SQL Server-compatible syntax because the provided backend security example uses C# SqlConnection and SqlCommand.

DATABASE DESIGN RULES

Use these initial design choices unless they create a normalization or integrity problem:

Users:
- UserId
- FullName
- Email
- Role

Events:
- EventId
- Title
- Description
- EventDate
- Venue
- Capacity

Registrations:
- RegistrationId
- UserId
- EventId
- RegistrationDate
- Status

Apply appropriate integrity rules, including:

- User email must be unique.
- User Role must be limited to valid system roles.
- Event Capacity must be greater than zero.
- Registration Status must be limited to valid values.
- A User must not be able to register for the same Event more than once.
- Registrations must reference valid Users and Events.

Keep the schema appropriate for a 3-hour student prototype.

Do not introduce unnecessary entities, authentication tables, payment tables, notification tables, or enterprise-level database features.

Do not redesign the project beyond the required event-management scope.

OUTPUT FORMAT

Use exactly these headings:

## Proposed 3NF Schema
## Entity Details
## Relationship Cardinalities
## Normalization Analysis
## Business Rules
## Mermaid.js ERD
## SQL DDL
## ERD and SQL Consistency Audit
## Manual Verification Points

---

## Proposed 3NF Schema

The database uses three core relational entities:

1. Users
2. Events
3. Registrations

`Registrations` acts as the associative entity between `Users` and `Events`, resolving the many-to-many relationship into two one-to-many relationships.

### Users

| Attribute | Data Type | Rule |
|---|---|---|
| UserId | INT IDENTITY(1,1) | Primary Key |
| FullName | NVARCHAR(150) | NOT NULL |
| Email | NVARCHAR(255) | NOT NULL, UNIQUE |
| Role | VARCHAR(20) | NOT NULL, CHECK |

Valid roles:

- Student
- Administrator

### Events

| Attribute | Data Type | Rule |
|---|---|---|
| EventId | INT IDENTITY(1,1) | Primary Key |
| Title | NVARCHAR(200) | NOT NULL |
| Description | NVARCHAR(1000) | NULL |
| EventDate | DATETIME2 | NOT NULL |
| Venue | NVARCHAR(200) | NOT NULL |
| Capacity | INT | NOT NULL, CHECK > 0 |

### Registrations

| Attribute | Data Type | Rule |
|---|---|---|
| RegistrationId | INT IDENTITY(1,1) | Primary Key |
| UserId | INT | NOT NULL, Foreign Key |
| EventId | INT | NOT NULL, Foreign Key |
| RegistrationDate | DATETIME2 | NOT NULL, DEFAULT |
| Status | VARCHAR(20) | NOT NULL, CHECK |

Valid registration statuses:

- Registered
- Cancelled

The combination `(UserId, EventId)` is unique to prevent duplicate registration for the same event.

---

## Relationship Cardinalities

The schema uses these relationships:

- One User may have zero or many Registrations.
- Each Registration belongs to exactly one User.
- One Event may have zero or many Registrations.
- Each Registration belongs to exactly one Event.
- Users and Events therefore have a many-to-many relationship resolved through Registrations.

---

## Normalization Analysis

### First Normal Form - 1NF

The schema satisfies 1NF because all attributes contain atomic values, each table has a primary key, and there are no repeating groups.

### Second Normal Form - 2NF

The schema satisfies 2NF because each table uses a single-column primary key and all non-key attributes depend on the complete primary key. The `(UserId, EventId)` combination in Registrations is a unique business constraint rather than the primary key.

### Third Normal Form - 3NF

The schema satisfies 3NF because there are no inappropriate transitive dependencies.

User information is stored only in `Users`, event information is stored only in `Events`, and registration-specific information is stored only in `Registrations`.

This prevents unnecessary duplication and update anomalies.

---

## Business Rules

The schema enforces the following rules:

1. Each user has a unique UserId.
2. Each user email must be unique.
3. User Role must be either Student or Administrator.
4. Each event has a unique EventId.
5. Event Capacity must be greater than zero.
6. Each registration must reference an existing User.
7. Each registration must reference an existing Event.
8. Registration Status must be either Registered or Cancelled.
9. A User cannot have more than one registration for the same Event.
10. Foreign Keys use explicit NO ACTION rules for delete and update operations.

Seat availability is not enforced by a simple CHECK constraint because it depends on the number of related registration rows. This rule will be handled through backend validation.

---

## Mermaid.js ERD

The following Mermaid.js ERD was manually rendered and verified successfully:

```mermaid
erDiagram

    USERS ||--o{ REGISTRATIONS : "has"
    EVENTS ||--o{ REGISTRATIONS : "receives"

    USERS {
        INT UserId PK
        NVARCHAR FullName
        NVARCHAR Email UK
        VARCHAR Role
    }

    EVENTS {
        INT EventId PK
        NVARCHAR Title
        NVARCHAR Description
        DATETIME2 EventDate
        NVARCHAR Venue
        INT Capacity
    }

    REGISTRATIONS {
        INT RegistrationId PK
        INT UserId FK
        INT EventId FK
        DATETIME2 RegistrationDate
        VARCHAR Status
    }