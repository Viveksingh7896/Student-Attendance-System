using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceSystem.Data;

namespace StudentAttendanceSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Dashboard statistics
            var totalStudents = await _context.Students.CountAsync(s => s.IsActive);
            var totalClasses = await _context.Classes.CountAsync();
            var todayDate = DateTime.Today;
            var todayAttendance = await _context.Attendances
                .Where(a => a.AttendanceDate == todayDate)
                .CountAsync();

            ViewBag.TotalStudents = totalStudents;
            ViewBag.TotalClasses = totalClasses;
            ViewBag.TodayAttendance = todayAttendance;

            return View();
        }
    }
}
