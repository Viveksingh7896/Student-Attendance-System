# Student Attendance System - Complete Setup Guide

## Overview
This is a fully functional Student Attendance Management System built with ASP.NET Core MVC, Entity Framework Core, and SQL Server.

## Prerequisites
- **Visual Studio 2022** (Community, Professional, or Enterprise)
- **.NET 8.0 SDK**
- **SQL Server 2019+** (Express edition works fine)
- **SQL Server Management Studio (SSMS)**

## Project Features
✅ Student Management (CRUD operations)
✅ Class/Section Management
✅ Daily Attendance Marking (Bulk Entry)
✅ Multiple Attendance Status (Present, Absent, Late, Excused)
✅ Attendance Reports by Class
✅ Attendance Reports by Student
✅ Attendance Percentage Calculation
✅ Search and Filter Functionality
✅ Responsive Bootstrap 5 Design
✅ Print-Friendly Reports

## Folder Structure

```
StudentAttendanceSystem/
│
├── 📄 Program.cs                     # Application entry point
├── 📄 StudentAttendanceSystem.csproj # Project file
├── 📄 appsettings.json               # Configuration
├── 📄 .gitignore                     # Git ignore rules
├── 📄 README.md                      # Project documentation
│
├── 📁 Controllers/
│   ├── HomeController.cs             # Dashboard
│   ├── StudentController.cs          # Student CRUD
│   ├── AttendanceController.cs       # Attendance marking
│   └── ReportController.cs           # Reports generation
│
├── 📁 Models/
│   ├── Student.cs                    # Student entity
│   ├── Class.cs                      # Class entity
│   ├── Attendance.cs                 # Attendance entity
│   └── ViewModels/
│       └── AttendanceViewModel.cs    # ViewModels for attendance
│
├── 📁 Data/
│   └── ApplicationDbContext.cs       # EF Core DbContext
│
├── 📁 Views/
│   ├── _ViewImports.cshtml
│   ├── _ViewStart.cshtml
│   │
│   ├── Home/
│   │   └── Index.cshtml              # Dashboard
│   │
│   ├── Student/
│   │   ├── Index.cshtml              # List students
│   │   ├── Create.cshtml             # Add student
│   │   ├── Edit.cshtml               # Edit student
│   │   └── Details.cshtml            # Student details
│   │
│   ├── Attendance/
│   │   ├── MarkAttendance.cshtml     # Bulk attendance marking
│   │   └── ViewAttendance.cshtml     # View daily attendance
│   │
│   ├── Report/
│   │   ├── ClassReport.cshtml        # Class attendance report
│   │   └── StudentReport.cshtml      # Student attendance report
│   │
│   └── Shared/
│       ├── _Layout.cshtml            # Main layout
│       └── _ValidationScriptsPartial.cshtml
│
├── 📁 wwwroot/
│   ├── css/
│   │   └── site.css                  # Custom styles
│   └── js/
│       └── site.js                   # Custom JavaScript
│
└── 📁 Database/
    ├── CreateDatabase.sql            # Database creation
    └── SeedData.sql                  # Sample data
```

## Step-by-Step Setup

### 1. Create Project Structure

Create all folders as shown above in your repository.

### 2. Database Setup

**Option A: Using SQL Scripts (Recommended)**

1. Open **SQL Server Management Studio (SSMS)**
2. Connect to your SQL Server instance
3. Open `Database/CreateDatabase.sql`
4. Execute the script (F5)
5. (Optional) Run `Database/SeedData.sql` to insert sample data

**Option B: Using Entity Framework Migrations**

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 3. Configure Connection String

Edit `appsettings.json` and update the connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=StudentAttendanceDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Replace `YOUR_SERVER_NAME` with:
- `localhost` for local SQL Server
- `(localdb)\MSSQLLocalDB` for LocalDB
- Your actual server name for remote SQL Server

### 4. Restore NuGet Packages

```bash
dotnet restore
```

Or in Visual Studio:
- Right-click solution → "Restore NuGet Packages"

### 5. Build the Project

```bash
dotnet build
```

### 6. Run the Application

**Visual Studio:**
- Press **F5** or click the "Play" button

**Command Line:**
```bash
dotnet run
```

The application will launch at:
- HTTPS: `https://localhost:7XXX`
- HTTP: `http://localhost:5XXX`

## Using the System

### 1. Dashboard
- View total students, classes, and today's attendance
- Quick action buttons for common tasks

### 2. Add Students
1. Navigate to **Students** → **Add New Student**
2. Fill in student details
3. Assign to a class
4. Click **Create Student**

### 3. Mark Attendance
1. Navigate to **Attendance** → **Mark Attendance**
2. Select a class and date
3. Click **Load Students**
4. Mark attendance status for each student:
   - ✅ **Present** (Green)
   - ❌ **Absent** (Red)
   - ⏰ **Late** (Yellow)
   - ℹ️ **Excused** (Blue)
5. Add optional remarks
6. Click **Save Attendance**

**Quick Actions:**
- **Mark All Present** button
- **Mark All Absent** button

### 4. View Attendance
1. Navigate to **Attendance** → **View Attendance**
2. Select class and date filters
3. Click **Filter**
4. View attendance summary with statistics

### 5. Generate Reports

**Class Report:**
1. Navigate to **Reports** → **Class Report**
2. Select class and date range
3. Click **Generate**
4. View attendance statistics for all students
5. Use **Print Report** button for printing

**Student Report:**
1. Navigate to **Reports** → **Student Report**
2. Select student and date range
3. Click **Generate**
4. View detailed attendance history
5. Use **Print Report** button for printing

## Database Schema

### Tables

**Classes**
- ClassId (PK)
- ClassName
- Section
- ClassTeacher
- AcademicYear

**Students**
- StudentId (PK)
- StudentIdNumber (Unique)
- FirstName
- LastName
- Email (Unique)
- PhoneNumber
- ClassId (FK)
- EnrollmentDate
- IsActive

**Attendances**
- AttendanceId (PK)
- StudentId (FK)
- ClassId (FK)
- AttendanceDate
- Status (0=Present, 1=Absent, 2=Late, 3=Excused)
- Remarks
- MarkedBy
- MarkedAt
- Unique constraint: (StudentId, AttendanceDate)

## Key Features Explained

### Bulk Attendance Marking
- Load all students in a class at once
- Mark attendance for entire class in one form
- Color-coded status dropdowns
- Quick action buttons (Mark All Present/Absent)

### Attendance Reports
- **Class Report**: Shows attendance summary for all students in a class
- **Student Report**: Shows detailed attendance history for one student
- Calculates attendance percentage automatically
- Color-coded performance indicators:
  - 🟢 Green: ≥75% (Good)
  - 🟡 Yellow: 50-74% (Average)
  - 🔴 Red: <50% (Poor)

### Search and Filter
- Search students by name, ID, or email
- Filter by class
- Filter attendance by date and class

## Troubleshooting

### Cannot Connect to Database
**Solution:**
- Verify SQL Server is running
- Check connection string in `appsettings.json`
- Ensure database exists (run CreateDatabase.sql)
- Test connection in SSMS

### Build Errors
**Solution:**
```bash
dotnet clean
dotnet restore
dotnet build
```

### Missing NuGet Packages
**Solution:**
```bash
dotnet restore
```

### Port Already in Use
**Solution:**
- Edit `Properties/launchSettings.json`
- Change port numbers
- Or stop the process using the port

### Attendance Not Saving
**Solution:**
- Check database connection
- Verify ClassId is selected
- Ensure date is valid
- Check browser console for errors

## Next Steps & Enhancements

### Recommended Additions:
1. **Authentication & Authorization**
   - Add ASP.NET Core Identity
   - Role-based access (Admin, Teacher, Student)

2. **Additional Features**
   - SMS/Email notifications for absences
   - Parent portal to view attendance
   - Export reports to Excel/PDF
   - Attendance calendar view
   - Biometric integration

3. **Analytics**
   - Attendance trends over time
   - Class-wise comparison charts
   - Monthly/weekly summaries

4. **Mobile App**
   - Develop mobile app for teachers
   - Quick attendance marking via mobile

## Testing Checklist

- [ ] Create a new class
- [ ] Add multiple students
- [ ] Mark attendance for a class
- [ ] View today's attendance
- [ ] Generate class report
- [ ] Generate student report
- [ ] Search for students
- [ ] Edit student information
- [ ] Test different attendance statuses
- [ ] Print reports

## Deployment

### Deploy to IIS:
1. Publish project in Visual Studio
2. Copy published files to IIS wwwroot
3. Configure IIS application pool (.NET Core)
4. Update connection string for production database

### Deploy to Azure:
1. Create Azure SQL Database
2. Create Azure App Service
3. Configure connection string in Azure Portal
4. Deploy via Visual Studio or GitHub Actions

## Support

For issues or questions:
- Check this README
- Review code comments
- Refer to ASP.NET Core documentation

---

**Version:** 1.0  
**Framework:** ASP.NET Core 8.0  
**Database:** SQL Server  
**Last Updated:** February 2024
