# Student-Attendance-System
Basic ASP.NET MVC project for managing student attendance
# Student Attendance System

ASP.NET Core MVC application for tracking and managing student attendance records.

## Tech Stack
- **Frontend**: HTML, CSS, JavaScript, Bootstrap 5
- **Backend**: ASP.NET Core MVC
- **Database**: SQL Server
- **Tools**: Visual Studio 2022, SSMS

## Project Structure

```
StudentAttendanceSystem/
│
├── Controllers/              # MVC Controllers
│   ├── HomeController.cs
│   ├── StudentController.cs
│   ├── AttendanceController.cs
│   └── ReportController.cs
│
├── Models/                   # Data Models & ViewModels
│   ├── Student.cs
│   ├── Class.cs
│   ├── Attendance.cs
│   └── ViewModels/
│       ├── AttendanceViewModel.cs
│       └── AttendanceReportViewModel.cs
│
├── Views/                    # Razor Views
│   ├── Home/
│   │   └── Index.cshtml
│   ├── Student/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   └── Details.cshtml
│   ├── Attendance/
│   │   ├── Index.cshtml
│   │   ├── MarkAttendance.cshtml
│   │   └── ViewAttendance.cshtml
│   ├── Report/
│   │   ├── Index.cshtml
│   │   └── StudentReport.cshtml
│   ├── Shared/
│   │   ├── _Layout.cshtml
│   │   └── _ValidationScriptsPartial.cshtml
│   └── _ViewImports.cshtml
│
├── Data/                     # Database Context
│   └── ApplicationDbContext.cs
│
├── wwwroot/                  # Static Files
│   ├── css/
│   │   └── site.css
│   ├── js/
│   │   └── site.js
│   └── images/
│
├── Database/                 # SQL Scripts
│   ├── CreateDatabase.sql
│   └── SeedData.sql
│
├── appsettings.json         # Configuration
├── Program.cs               # Application entry point
└── StudentAttendanceSystem.csproj
```

## Features
- ✅ Student management (CRUD operations)
- ✅ Class/Course management
- ✅ Daily attendance marking
- ✅ Bulk attendance entry
- ✅ Attendance reports by student
- ✅ Attendance reports by date/class
- ✅ Attendance percentage calculation
- ✅ Search and filter functionality
- ✅ Responsive design with Bootstrap

## Setup Instructions

### 1. Database Setup
1. Open SSMS
2. Run `Database/CreateDatabase.sql`
3. Update connection string in `appsettings.json`

### 2. Run Application
```bash
dotnet restore
dotnet build
dotnet run
```

## Quick Start
1. Navigate to Students → Add students
2. Navigate to Classes → Add classes
3. Navigate to Attendance → Mark daily attendance
4. Navigate to Reports → View attendance statistics
