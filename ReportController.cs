using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceSystem.Data;
using StudentAttendanceSystem.Models;
using StudentAttendanceSystem.Models.ViewModels;

namespace StudentAttendanceSystem.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Report/Index
        public IActionResult Index()
        {
            ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "FullClassName");
            return View();
        }

        // GET: Report/ClassReport
        public async Task<IActionResult> ClassReport(int? classId, DateTime? startDate, DateTime? endDate)
        {
            if (!classId.HasValue)
            {
                ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "FullClassName");
                return View(new List<AttendanceReportViewModel>());
            }

            var start = startDate ?? DateTime.Today.AddMonths(-1);
            var end = endDate ?? DateTime.Today;

            var students = await _context.Students
                .Where(s => s.ClassId == classId.Value)
                .Include(s => s.Attendances)
                .Include(s => s.Class)
                .ToListAsync();

            var reportData = students.Select(s =>
            {
                var attendances = s.Attendances?
                    .Where(a => a.AttendanceDate >= start && a.AttendanceDate <= end)
                    .ToList() ?? new List<Attendance>();

                var totalDays = attendances.Count;
                var presentDays = attendances.Count(a => a.Status == AttendanceStatus.Present);
                var absentDays = attendances.Count(a => a.Status == AttendanceStatus.Absent);
                var lateDays = attendances.Count(a => a.Status == AttendanceStatus.Late);
                var excusedDays = attendances.Count(a => a.Status == AttendanceStatus.Excused);

                return new AttendanceReportViewModel
                {
                    StudentId = s.StudentId,
                    StudentName = s.FullName,
                    StudentIdNumber = s.StudentIdNumber,
                    ClassName = s.Class?.FullClassName ?? "",
                    TotalDays = totalDays,
                    PresentDays = presentDays,
                    AbsentDays = absentDays,
                    LateDays = lateDays,
                    ExcusedDays = excusedDays,
                    AttendancePercentage = totalDays > 0 ? Math.Round((decimal)presentDays / totalDays * 100, 2) : 0
                };
            }).ToList();

            ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "FullClassName", classId);
            ViewBag.StartDate = start;
            ViewBag.EndDate = end;
            ViewBag.SelectedClassId = classId;

            return View(reportData);
        }

        // GET: Report/StudentReport
        public async Task<IActionResult> StudentReport(int? studentId, DateTime? startDate, DateTime? endDate)
        {
            if (!studentId.HasValue)
            {
                ViewBag.Students = new SelectList(
                    await _context.Students.Include(s => s.Class).ToListAsync(),
                    "StudentId",
                    "FullName"
                );
                return View();
            }

            var start = startDate ?? DateTime.Today.AddMonths(-1);
            var end = endDate ?? DateTime.Today;

            var student = await _context.Students
                .Include(s => s.Class)
                .Include(s => s.Attendances)
                .FirstOrDefaultAsync(s => s.StudentId == studentId.Value);

            if (student == null)
            {
                return NotFound();
            }

            var attendances = await _context.Attendances
                .Where(a => a.StudentId == studentId.Value 
                       && a.AttendanceDate >= start 
                       && a.AttendanceDate <= end)
                .OrderByDescending(a => a.AttendanceDate)
                .ToListAsync();

            ViewBag.Student = student;
            ViewBag.StartDate = start;
            ViewBag.EndDate = end;
            ViewBag.Students = new SelectList(
                await _context.Students.Include(s => s.Class).ToListAsync(),
                "StudentId",
                "FullName",
                studentId
            );

            // Calculate statistics
            var totalDays = attendances.Count;
            var presentDays = attendances.Count(a => a.Status == AttendanceStatus.Present);
            var absentDays = attendances.Count(a => a.Status == AttendanceStatus.Absent);
            var lateDays = attendances.Count(a => a.Status == AttendanceStatus.Late);
            var excusedDays = attendances.Count(a => a.Status == AttendanceStatus.Excused);

            ViewBag.TotalDays = totalDays;
            ViewBag.PresentDays = presentDays;
            ViewBag.AbsentDays = absentDays;
            ViewBag.LateDays = lateDays;
            ViewBag.ExcusedDays = excusedDays;
            ViewBag.AttendancePercentage = totalDays > 0 ? Math.Round((decimal)presentDays / totalDays * 100, 2) : 0;

            return View(attendances);
        }
    }
}
