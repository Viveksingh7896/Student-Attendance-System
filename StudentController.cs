using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceSystem.Data;
using StudentAttendanceSystem.Models;

namespace StudentAttendanceSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index(string searchString, int? classFilter)
        {
            var students = _context.Students.Include(s => s.Class).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.FirstName.Contains(searchString)
                                           || s.LastName.Contains(searchString)
                                           || s.StudentIdNumber.Contains(searchString)
                                           || s.Email.Contains(searchString));
            }

            if (classFilter.HasValue)
            {
                students = students.Where(s => s.ClassId == classFilter.Value);
            }

            ViewBag.Classes = new SelectList(await _context.Classes.ToListAsync(), "ClassId", "FullClassName");
            ViewBag.SearchString = searchString;
            ViewBag.ClassFilter = classFilter;

            return View(await students.ToListAsync());
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Class)
                .Include(s => s.Attendances)
                .FirstOrDefaultAsync(m => m.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            // Calculate attendance statistics
            var totalDays = student.Attendances?.Count ?? 0;
            var presentDays = student.Attendances?.Count(a => a.Status == AttendanceStatus.Present) ?? 0;
            var attendancePercentage = totalDays > 0 ? (decimal)presentDays / totalDays * 100 : 0;

            ViewBag.TotalDays = totalDays;
            ViewBag.PresentDays = presentDays;
            ViewBag.AttendancePercentage = attendancePercentage;

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "FullClassName");
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,StudentIdNumber,FirstName,LastName,Email,PhoneNumber,ClassId,EnrollmentDate,IsActive")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Student created successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "FullClassName", student.ClassId);
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "FullClassName", student.ClassId);
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentIdNumber,FirstName,LastName,Email,PhoneNumber,ClassId,EnrollmentDate,IsActive")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Student updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "FullClassName", student.ClassId);
            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Class)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Student deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
