-- ============================================================
-- SAMG Campus Event Management System
-- Task 3 - Database Schema
-- SQL Server Compatible
-- ============================================================

-- ============================================================
-- TABLE: Users
-- ============================================================

CREATE TABLE dbo.Users
(
    UserId INT IDENTITY(1,1) NOT NULL,
    FullName NVARCHAR(150) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Role VARCHAR(20) NOT NULL,

    CONSTRAINT PK_Users
        PRIMARY KEY (UserId),

    CONSTRAINT UQ_Users_Email
        UNIQUE (Email),

    CONSTRAINT CK_Users_Role
        CHECK (Role IN ('Student', 'Administrator'))
);
GO


-- ============================================================
-- TABLE: Events
-- ============================================================

CREATE TABLE dbo.Events
(
    EventId INT IDENTITY(1,1) NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000) NULL,
    EventDate DATETIME2 NOT NULL,
    Venue NVARCHAR(200) NOT NULL,
    Capacity INT NOT NULL,

    CONSTRAINT PK_Events
        PRIMARY KEY (EventId),

    CONSTRAINT CK_Events_Capacity
        CHECK (Capacity > 0)
);
GO


-- ============================================================
-- TABLE: Registrations
-- ============================================================

CREATE TABLE dbo.Registrations
(
    RegistrationId INT IDENTITY(1,1) NOT NULL,
    UserId INT NOT NULL,
    EventId INT NOT NULL,
    RegistrationDate DATETIME2 NOT NULL
        CONSTRAINT DF_Registrations_RegistrationDate
        DEFAULT SYSUTCDATETIME(),

    Status VARCHAR(20) NOT NULL
        CONSTRAINT DF_Registrations_Status
        DEFAULT 'Registered',

    CONSTRAINT PK_Registrations
        PRIMARY KEY (RegistrationId),

    CONSTRAINT FK_Registrations_Users
        FOREIGN KEY (UserId)
        REFERENCES dbo.Users(UserId)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,

    CONSTRAINT FK_Registrations_Events
        FOREIGN KEY (EventId)
        REFERENCES dbo.Events(EventId)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,

    CONSTRAINT CK_Registrations_Status
        CHECK (Status IN ('Registered', 'Cancelled')),

    CONSTRAINT UQ_Registrations_User_Event
        UNIQUE (UserId, EventId)
);
GO


-- ============================================================
-- NON-CLUSTERED INDEXES ON FOREIGN KEY COLUMNS
-- Required explicitly for Task 3 examination compliance
-- ============================================================

CREATE NONCLUSTERED INDEX IX_Registrations_UserId
    ON dbo.Registrations(UserId);
GO

CREATE NONCLUSTERED INDEX IX_Registrations_EventId
    ON dbo.Registrations(EventId);
GO