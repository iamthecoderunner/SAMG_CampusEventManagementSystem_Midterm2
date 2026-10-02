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