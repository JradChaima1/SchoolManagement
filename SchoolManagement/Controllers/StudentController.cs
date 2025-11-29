using Microsoft.AspNetCore.Mvc;
using School.Core.Interfaces;
using School.Core.Models;
using SchoolManagement.Filters;

namespace SchoolManagement.Controllers
{
    [AuthorizeSession]
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IParentService _parentService;

        public StudentsController(IStudentService studentService, IParentService parentService)
        {
            _studentService = studentService;
            _parentService = parentService;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            IEnumerable<Student> students;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                students = await _studentService.SearchStudentsAsync(searchTerm);
                ViewBag.SearchTerm = searchTerm;
            }
            else
            {
                students = await _studentService.GetAllStudentsAsync();
            }

            return View(students);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            
            if (student == null)
            {
                return NotFound();
            }

            var averageGrade = await _studentService.GetStudentAverageGradeAsync(id);
            ViewBag.AverageGrade = averageGrade;

            return View(student);
        }

    public async Task<IActionResult> Create()
   {
    ViewBag.Parents = await _parentService.GetAllParentsAsync();
    return View();
   }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                student.EnrollmentDate = DateTime.Now;
                await _studentService.CreateStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

       public async Task<IActionResult> Edit(int id)
{
    var student = await _studentService.GetStudentByIdAsync(id);
    
    if (student == null)
    {
        return NotFound();
    }
    
    ViewBag.Parents = await _parentService.GetAllParentsAsync();
    return View(student);
}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _studentService.UpdateStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }
            
            return View(student);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            
            if (student == null)
            {
                return NotFound();
            }
            
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> TopPerformers()
        {
            var topStudents = await _studentService.GetTopPerformingStudentsAsync(10);
            return View(topStudents);
        }
    }
}
