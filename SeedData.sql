USE StudentAttendanceDB;
GO

-- Insert Sample Classes
INSERT INTO Classes (ClassName, Section, ClassTeacher, AcademicYear)
VALUES
('Grade 10', 'A', 'Mrs. Johnson', '2023-2024'),
('Grade 10', 'B', 'Mr. Smith', '2023-2024'),
('Grade 11', 'A', 'Ms. Williams', '2023-2024'),
('Grade 11', 'B', 'Dr. Brown', '2023-2024'),
('Grade 12', 'A', 'Prof. Davis', '2023-2024');
GO

-- Insert Sample Students
INSERT INTO Students (StudentIdNumber, FirstName, LastName, Email, PhoneNumber, ClassId, EnrollmentDate, IsActive)
VALUES
-- Grade 10 A
('STU2024001', 'John', 'Doe', 'john.doe@school.com', '555-0101', 1, '2023-09-01', 1),
('STU2024002', 'Jane', 'Smith', 'jane.smith@school.com', '555-0102', 1, '2023-09-01', 1),
('STU2024003', 'Michael', 'Johnson', 'michael.j@school.com', '555-0103', 1, '2023-09-01', 1),
('STU2024004', 'Emily', 'Williams', 'emily.w@school.com', '555-0104', 1, '2023-09-01', 1),
('STU2024005', 'David', 'Brown', 'david.b@school.com', '555-0105', 1, '2023-09-01', 1),

-- Grade 10 B
('STU2024006', 'Sarah', 'Davis', 'sarah.d@school.com', '555-0106', 2, '2023-09-01', 1),
('STU2024007', 'James', 'Miller', 'james.m@school.com', '555-0107', 2, '2023-09-01', 1),
('STU2024008', 'Emma', 'Wilson', 'emma.w@school.com', '555-0108', 2, '2023-09-01', 1),
('STU2024009', 'Oliver', 'Moore', 'oliver.m@school.com', '555-0109', 2, '2023-09-01', 1),
('STU2024010', 'Sophia', 'Taylor', 'sophia.t@school.com', '555-0110', 2, '2023-09-01', 1),

-- Grade 11 A
('STU2024011', 'William', 'Anderson', 'william.a@school.com', '555-0111', 3, '2023-09-01', 1),
('STU2024012', 'Ava', 'Thomas', 'ava.t@school.com', '555-0112', 3, '2023-09-01', 1),
('STU2024013', 'Lucas', 'Jackson', 'lucas.j@school.com', '555-0113', 3, '2023-09-01', 1),
('STU2024014', 'Mia', 'White', 'mia.w@school.com', '555-0114', 3, '2023-09-01', 1),
('STU2024015', 'Alexander', 'Harris', 'alex.h@school.com', '555-0115', 3, '2023-09-01', 1);
GO

-- Insert Sample Attendance Records (Last 7 days for Grade 10 A)
DECLARE @StartDate DATE = DATEADD(DAY, -6, GETDATE());
DECLARE @CurrentDate DATE;
DECLARE @StudentId INT;

DECLARE StudentCursor CURSOR FOR 
SELECT StudentId FROM Students WHERE ClassId = 1;

OPEN StudentCursor;
FETCH NEXT FROM StudentCursor INTO @StudentId;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @CurrentDate = @StartDate;
    
    WHILE @CurrentDate <= GETDATE()
    BEGIN
        -- Random attendance status (mostly present)
        DECLARE @Status INT = CASE 
            WHEN RAND() < 0.85 THEN 0  -- 85% Present
            WHEN RAND() < 0.95 THEN 1  -- 10% Absent
            ELSE 2                      -- 5% Late
        END;
        
        INSERT INTO Attendances (StudentId, ClassId, AttendanceDate, Status, MarkedBy, MarkedAt)
        VALUES (@StudentId, 1, @CurrentDate, @Status, 'System Admin', @CurrentDate);
        
        SET @CurrentDate = DATEADD(DAY, 1, @CurrentDate);
    END
    
    FETCH NEXT FROM StudentCursor INTO @StudentId;
END

CLOSE StudentCursor;
DEALLOCATE StudentCursor;
GO

PRINT 'Sample data inserted successfully!';
PRINT 'Classes: 5';
PRINT 'Students: 15';
PRINT 'Attendance Records: Created for last 7 days for Grade 10 A';
