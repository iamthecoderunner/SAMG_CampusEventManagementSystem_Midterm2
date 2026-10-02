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


# TASK 2 -

ROLE

Act as the Frontend Engineer responsible for Task 2 of a 4th Year BSIT midterm laboratory examination.

CONTEXT

The project is the SAMG Online Campus Event Management System.

The system must allow:
- Students to view upcoming campus events.
- Students to register for an event.
- Administrators to view registered attendees.

The frontend must remain consistent with the database design already established by Task 3.

DATABASE CONTRACT

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

Valid Roles:
- Student
- Administrator

Valid Registration Status:
- Registered
- Cancelled

PROJECT FILES

The frontend must be implemented under:

/frontend/index.html
/frontend/styles.css
/frontend/script.js

TASK

Design and implement a simple but polished prototype frontend for the Online Campus Event Management System.

The interface must include:

1. A semantic page header identifying the Campus Event Management System.
2. An Upcoming Events catalog.
3. Event cards showing:
   - Title
   - Description
   - Event Date
   - Venue
   - Capacity
4. A Student Registration Form containing:
   - Full Name
   - Email
   - Event selection
   - Submit button
5. A Registered Attendees section suitable for an administrator view.
6. Basic JavaScript interaction allowing a student registration submitted through the form to appear in the Registered Attendees section for the selected event.
7. User-friendly validation and confirmation messages.

ACCESSIBILITY REQUIREMENTS

Use semantic HTML5 and basic WCAG accessibility practices.

Explicitly include:
- header
- nav where appropriate
- main
- section
- article where appropriate
- form
- footer
- properly associated label elements
- aria-label attributes where useful
- accessible form controls
- visible keyboard focus states
- sufficient foreground/background contrast
- alt text for meaningful images if images are used

TECHNICAL CONSTRAINTS

- Use only HTML5, CSS3, and vanilla JavaScript.
- Do not use React.
- Do not use Redux or another state-management library.
- Do not use Bootstrap or another CSS framework.
- Do not introduce authentication.
- Do not introduce APIs or backend frameworks.
- Do not add payment, notification, or unrelated features.
- Keep the implementation realistic for a 3-hour student prototype.
- Use mock event and attendee data in JavaScript where necessary.
- Do not pretend that frontend mock data is already connected to SQL Server.
- Keep names and concepts consistent with the Task 3 database schema.

OUTPUT REQUIREMENTS

Return exactly three complete code blocks in this order:

1. frontend/index.html
2. frontend/styles.css
3. frontend/script.js

After the three code blocks, provide:

## Accessibility Checklist

Briefly identify where each required accessibility feature was implemented.

## Integration Notes

Explain how the frontend fields correspond to Users, Events, and Registrations in Task 3 and clearly distinguish the current mock frontend behavior from future backend/database integration.


## RESPONSE

### 1. `frontend/index.html`

```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description"
          content="SAMG Campus Event Management System prototype">
    <title>SAMG Campus Event Management System</title>
    <link rel="stylesheet" href="styles.css">
</head>

<body>
    <header class="site-header">
        <div class="container">
            <h1>SAMG Campus Event Management System</h1>
            <p>Discover, register, and participate in upcoming campus events.</p>

            <nav aria-label="Main navigation">
                <a href="#events">Upcoming Events</a>
                <a href="#registration">Register</a>
                <a href="#attendees">Attendees</a>
            </nav>
        </div>
    </header>

    <main class="container">
        <section id="events" aria-labelledby="events-heading">
            <div class="section-heading">
                <p class="eyebrow">Campus Activities</p>
                <h2 id="events-heading">Upcoming Events</h2>
                <p>Browse the available campus events below.</p>
            </div>

            <div id="event-list"
                 class="event-grid"
                 aria-live="polite">
                <!-- Event cards are generated by script.js -->
            </div>
        </section>

        <section id="registration"
                 class="panel"
                 aria-labelledby="registration-heading">
            <div class="section-heading">
                <p class="eyebrow">Student Registration</p>
                <h2 id="registration-heading">Register for an Event</h2>
                <p>Complete the form to join an upcoming campus event.</p>
            </div>

            <form id="registration-form">
                <div class="form-group">
                    <label for="full-name">Full Name</label>
                    <input
                        type="text"
                        id="full-name"
                        name="fullName"
                        autocomplete="name"
                        required
                        maxlength="150"
                        aria-describedby="full-name-help">
                    <small id="full-name-help">
                        Enter your complete name.
                    </small>
                </div>

                <div class="form-group">
                    <label for="email">Email Address</label>
                    <input
                        type="email"
                        id="email"
                        name="email"
                        autocomplete="email"
                        required
                        maxlength="255"
                        aria-describedby="email-help">
                    <small id="email-help">
                        Enter a valid student email address.
                    </small>
                </div>

                <div class="form-group">
                    <label for="event-select">Select Event</label>
                    <select
                        id="event-select"
                        name="eventId"
                        required
                        aria-describedby="event-help">
                        <option value="">Choose an event</option>
                    </select>
                    <small id="event-help">
                        Select one of the available campus events.
                    </small>
                </div>

                <button type="submit">Register for Event</button>

                <div id="form-message"
                     class="form-message"
                     role="status"
                     aria-live="polite"></div>
            </form>
        </section>

        <section id="attendees"
                 aria-labelledby="attendees-heading">
            <div class="section-heading">
                <p class="eyebrow">Administrator View</p>
                <h2 id="attendees-heading">Registered Attendees</h2>
                <p>
                    Select an event to view its currently registered students.
                </p>
            </div>

            <div class="form-group attendee-filter">
                <label for="attendee-event-select">
                    View attendees for
                </label>
                <select
                    id="attendee-event-select"
                    aria-label="Select event to view registered attendees">
                    <option value="">Choose an event</option>
                </select>
            </div>

            <div id="attendee-summary"
                 class="attendee-summary"
                 aria-live="polite">
                Select an event to display its attendees.
            </div>

            <div class="table-wrapper">
                <table>
                    <caption>
                        Students registered for the selected event
                    </caption>
                    <thead>
                        <tr>
                            <th scope="col">Full Name</th>
                            <th scope="col">Email</th>
                            <th scope="col">Status</th>
                        </tr>
                    </thead>
                    <tbody id="attendee-list">
                        <tr>
                            <td colspan="3">
                                No event selected.
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </section>
    </main>

    <footer class="site-footer">
        <div class="container">
            <p>
                SAMG Campus Event Management System —
                Midterm Prototype
            </p>
        </div>
    </footer>

    <script src="script.js"></script>
</body>
</html>
```

### 2. `frontend/styles.css`

```css
:root {
    --primary: #163a63;
    --primary-dark: #0d2948;
    --accent: #f0b429;
    --background: #f4f6f8;
    --surface: #ffffff;
    --text: #17202a;
    --muted: #536170;
    --border: #cbd3dc;
    --success: #176b3a;
    --error: #a61b1b;
    --focus: #005fcc;
    --radius: 10px;
}

* {
    box-sizing: border-box;
}

html {
    scroll-behavior: smooth;
}

body {
    margin: 0;
    font-family: Arial, Helvetica, sans-serif;
    line-height: 1.6;
    color: var(--text);
    background: var(--background);
}

.container {
    width: min(1100px, 92%);
    margin: 0 auto;
}

.site-header {
    padding: 2.5rem 0;
    color: #ffffff;
    background: var(--primary);
}

.site-header h1 {
    margin: 0 0 0.5rem;
    font-size: clamp(1.8rem, 4vw, 2.7rem);
}

.site-header p {
    margin: 0 0 1.5rem;
}

nav {
    display: flex;
    flex-wrap: wrap;
    gap: 0.75rem;
}

nav a {
    display: inline-block;
    padding: 0.6rem 0.85rem;
    color: #ffffff;
    font-weight: 700;
    text-decoration: none;
    border: 1px solid rgba(255, 255, 255, 0.55);
    border-radius: 6px;
}

nav a:hover {
    background: #ffffff;
    color: var(--primary-dark);
}

main {
    padding: 2rem 0 3rem;
}

section {
    margin-bottom: 3rem;
    scroll-margin-top: 1rem;
}

.section-heading {
    margin-bottom: 1.25rem;
}

.section-heading h2 {
    margin: 0.2rem 0;
    color: var(--primary-dark);
}

.section-heading p {
    margin-top: 0.25rem;
}

.eyebrow {
    margin: 0;
    color: var(--primary);
    font-size: 0.85rem;
    font-weight: 700;
    letter-spacing: 0.08em;
    text-transform: uppercase;
}

.event-grid {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(240px, 1fr));
    gap: 1rem;
}

.event-card {
    padding: 1.25rem;
    background: var(--surface);
    border: 1px solid var(--border);
    border-top: 5px solid var(--accent);
    border-radius: var(--radius);
    box-shadow: 0 3px 12px rgba(0, 0, 0, 0.06);
}

.event-card h3 {
    margin-top: 0;
    color: var(--primary-dark);
}

.event-card dl {
    margin-bottom: 0;
}

.event-card dt {
    font-weight: 700;
}

.event-card dd {
    margin: 0 0 0.5rem;
}

.panel {
    padding: 1.5rem;
    background: var(--surface);
    border: 1px solid var(--border);
    border-radius: var(--radius);
}

form {
    max-width: 650px;
}

.form-group {
    margin-bottom: 1.15rem;
}

label {
    display: block;
    margin-bottom: 0.35rem;
    font-weight: 700;
}

input,
select,
button {
    width: 100%;
    min-height: 44px;
    font: inherit;
    border-radius: 6px;
}

input,
select {
    padding: 0.7rem;
    color: var(--text);
    background: #ffffff;
    border: 1px solid #7c8996;
}

small {
    display: block;
    margin-top: 0.3rem;
    color: var(--muted);
}

button {
    padding: 0.75rem 1rem;
    color: #ffffff;
    font-weight: 700;
    cursor: pointer;
    background: var(--primary);
    border: 2px solid var(--primary);
}

button:hover {
    background: var(--primary-dark);
}

a:focus-visible,
input:focus-visible,
select:focus-visible,
button:focus-visible {
    outline: 3px solid var(--focus);
    outline-offset: 3px;
}

.form-message {
    min-height: 1.5rem;
    margin-top: 1rem;
    font-weight: 700;
}

.form-message.success {
    color: var(--success);
}

.form-message.error {
    color: var(--error);
}

.attendee-filter {
    max-width: 500px;
}

.attendee-summary {
    margin: 1rem 0;
    padding: 0.85rem;
    background: #e9eef4;
    border-left: 4px solid var(--primary);
}

.table-wrapper {
    overflow-x: auto;
    background: var(--surface);
    border: 1px solid var(--border);
    border-radius: var(--radius);
}

table {
    width: 100%;
    border-collapse: collapse;
}

caption {
    padding: 0.8rem;
    font-weight: 700;
    text-align: left;
}

th,
td {
    padding: 0.8rem;
    text-align: left;
    border-bottom: 1px solid var(--border);
}

th {
    color: #ffffff;
    background: var(--primary);
}

tbody tr:last-child td {
    border-bottom: 0;
}

.site-footer {
    padding: 1.5rem 0;
    color: #ffffff;
    text-align: center;
    background: var(--primary-dark);
}

@media (max-width: 600px) {
    nav {
        flex-direction: column;
    }

    nav a {
        text-align: center;
    }

    .panel {
        padding: 1rem;
    }
}
```

### 3. `frontend/script.js`

```javascript
"use strict";

/*
 * SAMG Campus Event Management System
 * Task 2 - Frontend Prototype
 *
 * The data below is mock frontend data only.
 * It is not connected directly to the SQL Server database.
 */

const events = [
    {
        EventId: 1,
        Title: "Technology Innovation Forum",
        Description:
            "Explore emerging technologies and student-led IT projects.",
        EventDate: "2026-10-10T09:00:00",
        Venue: "University Auditorium",
        Capacity: 100
    },
    {
        EventId: 2,
        Title: "Campus Leadership Summit",
        Description:
            "A student leadership event focused on teamwork and service.",
        EventDate: "2026-10-15T13:00:00",
        Venue: "Multipurpose Hall",
        Capacity: 80
    },
    {
        EventId: 3,
        Title: "Cybersecurity Awareness Seminar",
        Description:
            "Learn practical cybersecurity habits and common online risks.",
        EventDate: "2026-10-20T10:00:00",
        Venue: "IT Laboratory",
        Capacity: 50
    }
];

/*
 * Mock records use the same conceptual fields as the Task 3 schema.
 * New UserId and RegistrationId values are generated locally.
 */
const users = [
    {
        UserId: 1,
        FullName: "Sample Student",
        Email: "student@example.edu",
        Role: "Student"
    }
];

const registrations = [
    {
        RegistrationId: 1,
        UserId: 1,
        EventId: 1,
        RegistrationDate: new Date().toISOString(),
        Status: "Registered"
    }
];

const eventList = document.getElementById("event-list");
const registrationForm = document.getElementById("registration-form");
const fullNameInput = document.getElementById("full-name");
const emailInput = document.getElementById("email");
const eventSelect = document.getElementById("event-select");
const attendeeEventSelect =
    document.getElementById("attendee-event-select");
const attendeeList = document.getElementById("attendee-list");
const attendeeSummary = document.getElementById("attendee-summary");
const formMessage = document.getElementById("form-message");

function formatEventDate(dateValue) {
    return new Intl.DateTimeFormat("en-PH", {
        dateStyle: "medium",
        timeStyle: "short"
    }).format(new Date(dateValue));
}

function getRegisteredCount(eventId) {
    return registrations.filter(
        (registration) =>
            registration.EventId === eventId &&
            registration.Status === "Registered"
    ).length;
}

function renderEvents() {
    eventList.innerHTML = "";

    events.forEach((event) => {
        const article = document.createElement("article");
        article.className = "event-card";

        const title = document.createElement("h3");
        title.textContent = event.Title;

        const description = document.createElement("p");
        description.textContent = event.Description;

        const details = document.createElement("dl");

        const dateTerm = document.createElement("dt");
        dateTerm.textContent = "Date";
        const dateValue = document.createElement("dd");
        dateValue.textContent = formatEventDate(event.EventDate);

        const venueTerm = document.createElement("dt");
        venueTerm.textContent = "Venue";
        const venueValue = document.createElement("dd");
        venueValue.textContent = event.Venue;

        const capacityTerm = document.createElement("dt");
        capacityTerm.textContent = "Capacity";
        const capacityValue = document.createElement("dd");
        capacityValue.textContent =
            `${getRegisteredCount(event.EventId)} / ${event.Capacity} registered`;

        details.append(
            dateTerm,
            dateValue,
            venueTerm,
            venueValue,
            capacityTerm,
            capacityValue
        );

        article.append(title, description, details);
        eventList.appendChild(article);
    });
}

function populateEventSelections() {
    events.forEach((event) => {
        const registrationOption = document.createElement("option");
        registrationOption.value = String(event.EventId);
        registrationOption.textContent = event.Title;
        eventSelect.appendChild(registrationOption);

        const attendeeOption = document.createElement("option");
        attendeeOption.value = String(event.EventId);
        attendeeOption.textContent = event.Title;
        attendeeEventSelect.appendChild(attendeeOption);
    });
}

function findUser(userId) {
    return users.find((user) => user.UserId === userId);
}

function renderAttendees(eventId) {
    attendeeList.innerHTML = "";

    if (!eventId) {
        attendeeSummary.textContent =
            "Select an event to display its attendees.";

        const row = document.createElement("tr");
        const cell = document.createElement("td");
        cell.colSpan = 3;
        cell.textContent = "No event selected.";
        row.appendChild(cell);
        attendeeList.appendChild(row);
        return;
    }

    const event = events.find((item) => item.EventId === eventId);

    const eventRegistrations = registrations.filter(
        (registration) => registration.EventId === eventId
    );

    attendeeSummary.textContent =
        `${event.Title}: ${eventRegistrations.length} registration(s).`;

    if (eventRegistrations.length === 0) {
        const row = document.createElement("tr");
        const cell = document.createElement("td");
        cell.colSpan = 3;
        cell.textContent = "No students are registered for this event.";
        row.appendChild(cell);
        attendeeList.appendChild(row);
        return;
    }

    eventRegistrations.forEach((registration) => {
        const user = findUser(registration.UserId);

        if (!user) {
            return;
        }

        const row = document.createElement("tr");

        const nameCell = document.createElement("td");
        nameCell.textContent = user.FullName;

        const emailCell = document.createElement("td");
        emailCell.textContent = user.Email;

        const statusCell = document.createElement("td");
        statusCell.textContent = registration.Status;

        row.append(nameCell, emailCell, statusCell);
        attendeeList.appendChild(row);
    });
}

function showMessage(message, type) {
    formMessage.textContent = message;
    formMessage.className = `form-message ${type}`;
}

function nextUserId() {
    if (users.length === 0) {
        return 1;
    }

    return Math.max(...users.map((user) => user.UserId)) + 1;
}

function nextRegistrationId() {
    if (registrations.length === 0) {
        return 1;
    }

    return (
        Math.max(
            ...registrations.map(
                (registration) => registration.RegistrationId
            )
        ) + 1
    );
}

registrationForm.addEventListener("submit", (event) => {
    event.preventDefault();

    const fullName = fullNameInput.value.trim();
    const email = emailInput.value.trim().toLowerCase();
    const eventId = Number(eventSelect.value);

    if (!fullName || !email || !eventId) {
        showMessage(
            "Please complete all required registration fields.",
            "error"
        );
        return;
    }

    if (!registrationForm.checkValidity()) {
        registrationForm.reportValidity();
        return;
    }

    const selectedEvent = events.find(
        (campusEvent) => campusEvent.EventId === eventId
    );

    if (!selectedEvent) {
        showMessage("The selected event could not be found.", "error");
        return;
    }

    if (getRegisteredCount(eventId) >= selectedEvent.Capacity) {
        showMessage(
            "Registration is unavailable because this event is full.",
            "error"
        );
        return;
    }

    let user = users.find(
        (existingUser) => existingUser.Email.toLowerCase() === email
    );

    if (!user) {
        user = {
            UserId: nextUserId(),
            FullName: fullName,
            Email: email,
            Role: "Student"
        };

        users.push(user);
    }

    const duplicateRegistration = registrations.some(
        (registration) =>
            registration.UserId === user.UserId &&
            registration.EventId === eventId
    );

    if (duplicateRegistration) {
        showMessage(
            "This student is already registered for the selected event.",
            "error"
        );
        return;
    }

    const newRegistration = {
        RegistrationId: nextRegistrationId(),
        UserId: user.UserId,
        EventId: eventId,
        RegistrationDate: new Date().toISOString(),
        Status: "Registered"
    };

    registrations.push(newRegistration);

    showMessage(
        `Registration successful for ${selectedEvent.Title}.`,
        "success"
    );

    registrationForm.reset();

    renderEvents();

    attendeeEventSelect.value = String(eventId);
    renderAttendees(eventId);
});

attendeeEventSelect.addEventListener("change", () => {
    const eventId = Number(attendeeEventSelect.value);

    if (!eventId) {
        renderAttendees(null);
        return;
    }

    renderAttendees(eventId);
});

populateEventSelections();
renderEvents();
renderAttendees(null);
```

## Accessibility Checklist

The frontend uses semantic `header`, `nav`, `main`, `section`, `article`, `form`, and `footer` elements. Every registration control has a properly associated `<label>`, while navigation and the administrator event selector also use appropriate ARIA labeling.

Form help text is associated with controls through `aria-describedby`; registration feedback and attendee information use `aria-live` so status changes can be announced by assistive technology. The attendee table uses a `<caption>` and scoped column headings. Keyboard users receive clearly visible `:focus-visible` outlines, controls have practical minimum target heights, and the foreground/background combinations use high-contrast dark text or white text against dark surfaces.

No decorative or meaningful images were introduced, so unnecessary `alt` attributes were avoided. If meaningful images are added later, each should receive descriptive `alt` text.

## Integration Notes

The frontend deliberately follows the Task 3 database contract. Registration `Full Name` and `Email` correspond to `Users.FullName` and `Users.Email`; newly created mock users receive `Role: "Student"`. Event options use `Events.EventId`, while event cards represent `Title`, `Description`, `EventDate`, `Venue`, and `Capacity`.

A submitted registration creates a mock object corresponding to `Registrations`, including `RegistrationId`, `UserId`, `EventId`, `RegistrationDate`, and `Status: "Registered"`. The JavaScript also mirrors two important database/application rules: duplicate `UserId + EventId` registrations are rejected, and registration is prevented when the mock registered count reaches `Events.Capacity`.

For this midterm prototype, the arrays in `script.js` are **in-memory mock data only**. The browser is not directly connected to SQL Server, and refreshing the page resets changes. In an integrated implementation, the frontend would submit registration data to the backend; the backend would validate the request and perform parameterized database operations against the Task 3 schema.



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