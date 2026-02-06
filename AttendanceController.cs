using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceSystem.Data;
using StudentAttendanceSystem.Models;
using StudentAttendanceSystem.Models.ViewModels;

namespace StudentAttendanceSystem.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attendance
        public IActionResult Index()
        {
            ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "FullClassName");
            return View();
        }

        // GET: Attendance/MarkAttendance
        public async Task<IActionResult> MarkAttendance(int? classId, DateTime? date)
        {
            if (!classId.HasValue)
            {
                ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "FullClassName");
                return View(new MarkAttendanceViewModel());
            }

            var attendanceDate = date ?? DateTime.Today;
            var classInfo = await _context.Classes.FindAsync(classId.Value);

            if (classInfo == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Where(s => s.ClassId == classId.Value && s.IsActive)
                .OrderBy(s => s.FirstName)
                .ToListAsync();

            // Check if attendance already marked for this date
            var existingAttendance = await _context.Attendances
                .Where(a => a.ClassId == classId.Value && a.AttendanceDate.Date == attendanceDate.Date)
                .ToListAsync();

            var viewModel = new MarkAttendanceViewModel
            {
                ClassId = classId.Value,
                AttendanceDate = attendanceDate,
                ClassName = classInfo.FullClassName,
                Students = students.Select(s =>
                {
                    var existing = existingAttendance.FirstOrDefault(a => a.StudentId == s.StudentId);
                    return new AttendanceViewModel
                    {
                        StudentId = s.StudentId,
                        StudentName = s.FullName,
                        StudentIdNumber = s.StudentIdNumber,
                        Status = existing?.Status ?? AttendanceStatus.Present,
                        Remarks = existing?.Remarks
                    };
                }).ToList()
            };

            ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "FullClassName", classId.Value);
            return View(viewModel);
        }

        // POST: Attendance/MarkAttendance
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAttendance(MarkAttendanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Remove existing attendance for this class and date
                var existingAttendance = await _context.Attendances
                    .Where(a => a.ClassId == model.ClassId && a.AttendanceDate.Date == model.AttendanceDate.Date)
                    .ToListAsync();

                _context.Attendances.RemoveRange(existingAttendance);

                // Add new attendance records
                foreach (var student in model.Students)
                {
                    var attendance = new Attendance
                    {
                        StudentId = student.StudentId,
                        ClassId = model.ClassId,
                        AttendanceDate = model.AttendanceDate,
                        Status = student.Status,
                        Remarks = student.Remarks,
                        MarkedBy = "Admin", // TODO: Get from authentication
                        MarkedAt = DateTime.Now
                    };
                    _context.Attendances.Add(attendance);
                }

                await _context.SaveChangesAsync();
                TempData["Success"] = $"Attendance marked successfully for {model.AttendanceDate:dd/MM/yyyy}!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "FullClassName", model.ClassId);
            return View(model);
        }

        // GET: Attendance/ViewAttendance
        public async Task<IActionResult> ViewAttendance(int? classId, DateTime? date)
        {
            var attendanceDate = date ?? DateTime.Today;

            var query = _context.Attendances
                .Include(a => a.Student)
                .Include(a => a.Class)
                .Where(a => a.AttendanceDate.Date == attendanceDate.Date);

            if (classId.HasValue)
            {
                query = query.Where(a => a.ClassId == classId.Value);
            }

            var attendances = await query.OrderBy(a => a.Class.ClassName).ThenBy(a => a.Student.FirstName).ToListAsync();

            ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "FullClassName", classId);
            ViewBag.SelectedDate = attendanceDate;
            ViewBag.SelectedClassId = classId;

            return View(attendances);
        }
    }
}
