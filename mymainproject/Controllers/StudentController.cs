using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mymainproject.Data;
using mymainproject.Models;
using System.Diagnostics;

namespace mymainproject.Controllers
{
    public class StudentController : Controller
    {

        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var students = _context.Students
                          .Include(s => s.Course)
                          .ToList();

            return View(students);
        }

        public IActionResult Upsert(int? id)
        {
            var courses = _context.Courses.ToList();

            if (!courses.Any())
            {
                TempData["Error"] = "Please create a course before adding a student.";
                return RedirectToAction("Index", "Course");
            }

            Student student;

            if (id == null)
            {
                student = new Student();
            }
            else
            {
                student = _context.Students.Find(id);
            }

            student.Courses = courses;

            return View(student);
        }

        [HttpPost]
        public IActionResult Upsert(Student student)
        {
            if (!ModelState.IsValid)
                return View("Upsert", student);

            if (student.Id == 0)
            {
                // INSERT
                _context.Students.Add(student);
            }
            else
            {
                // UPDATE
                var existing = _context.Students.Find(student.Id);

                if (existing == null)
                    return NotFound();

                existing.Name = student.Name;
                existing.Age = student.Age;
                existing.Email = student.Email;
                existing.Phone = student.Phone;
                existing.Address = student.Address;
                existing.CourseId = student.CourseId;
                existing.Year = student.Year;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
