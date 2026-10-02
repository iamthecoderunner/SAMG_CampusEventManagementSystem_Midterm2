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