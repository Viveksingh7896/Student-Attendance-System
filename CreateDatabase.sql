-- Create Database
CREATE DATABASE StudentAttendanceDB;
GO

USE StudentAttendanceDB;
GO

-- Create Classes Table
CREATE TABLE Classes (
    ClassId INT PRIMARY KEY IDENTITY(1,1),
    ClassName NVARCHAR(50) NOT NULL,
    Section NVARCHAR(10),
    ClassTeacher NVARCHAR(100),
    AcademicYear NVARCHAR(20)
);
GO

-- Create Students Table
CREATE TABLE Students (
    StudentId INT PRIMARY KEY IDENTITY(1,1),
    StudentIdNumber NVARCHAR(20) NOT NULL UNIQUE,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(20),
    ClassId INT NOT NULL,
    EnrollmentDate DATE NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (ClassId) REFERENCES Classes(ClassId)
);
GO

-- Create Attendances Table
CREATE TABLE Attendances (
    AttendanceId INT PRIMARY KEY IDENTITY(1,1),
    StudentId INT NOT NULL,
    ClassId INT NOT NULL,
    AttendanceDate DATE NOT NULL,
    Status INT NOT NULL, -- 0=Present, 1=Absent, 2=Late, 3=Excused
    Remarks NVARCHAR(200),
    MarkedBy NVARCHAR(100),
    MarkedAt DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId) ON DELETE CASCADE,
    FOREIGN KEY (ClassId) REFERENCES Classes(ClassId),
    CONSTRAINT UQ_Student_Date UNIQUE (StudentId, AttendanceDate)
);
GO

-- Create Indexes for better performance
CREATE INDEX IX_Students_ClassId ON Students(ClassId);
CREATE INDEX IX_Students_StudentIdNumber ON Students(StudentIdNumber);
CREATE INDEX IX_Students_Email ON Students(Email);
CREATE INDEX IX_Attendances_StudentId ON Attendances(StudentId);
CREATE INDEX IX_Attendances_ClassId ON Attendances(ClassId);
CREATE INDEX IX_Attendances_Date ON Attendances(AttendanceDate);
CREATE INDEX IX_Attendances_Student_Date ON Attendances(StudentId, AttendanceDate);
GO

PRINT 'Database created successfully!';
