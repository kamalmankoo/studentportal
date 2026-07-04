using Microsoft.AspNetCore.Mvc;
using mymainproject.Data;
using mymainproject.Models;
using System.Diagnostics;

namespace mymainproject.Controllers
{
    public class CourseController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var students = _context.Courses.ToList();
            return View(students);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                // ➕ CREATE MODE
                return View(new CouseModel());
            }

            // ✏️ EDIT MODE
            var student = _context.Courses.Find(id);

            if (student == null)
                return NotFound();

            return View(student);
        }


        [HttpPost]
        public IActionResult Upsert(CouseModel student)
        {
            if (!ModelState.IsValid)
                return View("Upsert", student);

            if (student.Id == 0)
            {
                // INSERT
                _context.Courses.Add(student);
            }
            else
            {
                // UPDATE
                var existing = _context.Courses.Find(student.Id);

                if (existing == null)
                    return NotFound();

                existing.CourseName = student.CourseName;
                existing.Description = student.Description;
              
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
