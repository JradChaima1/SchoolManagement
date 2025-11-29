using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Core.Interfaces;
using School.Core.Models;
using SchoolManagement.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolManagement.Controllers
{
    [AuthorizeSession]
    public class EnrollmentsController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IStudentService _studentService;
        private readonly IClassroomService _classroomService;

        public EnrollmentsController(
            IEnrollmentService enrollmentService,
            IStudentService studentService,
            IClassroomService classroomService)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _classroomService = classroomService;
        }

public async Task<IActionResult> Index(string searchTerm)
{
    ViewBag.SearchTerm = searchTerm;
    
    var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
    
    var students = await _studentService.GetAllStudentsAsync();
    var classrooms = await _classroomService.GetAllClassroomsAsync();
    
    ViewBag.Students = students.ToDictionary(s => s.Id, s => $"{s.FirstName} {s.LastName}");
    ViewBag.Classrooms = classrooms.ToDictionary(c => c.Id, c => c.Name);
    
   
    if (!string.IsNullOrEmpty(searchTerm))
    {
        enrollments = enrollments.Where(e =>
            (ViewBag.Students.ContainsKey(e.StudentId) && 
             ViewBag.Students[e.StudentId].Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (ViewBag.Classrooms.ContainsKey(e.ClassroomId) && 
             ViewBag.Classrooms[e.ClassroomId].Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            e.AcademicYear.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            e.Semester.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
        ).ToList();
    }
    
    return View(enrollments);
}



        public async Task<IActionResult> Create(int? studentId, int? classroomId)
        {
            var students = await _studentService.GetAllStudentsAsync();
            var classrooms = await _classroomService.GetAllClassroomsAsync();

            ViewBag.Students = students.Select(s => new SelectListItem
{
           Value = s.Id.ToString(),
         Text = $"{s.FirstName} {s.LastName}"
       }).ToList();

          ViewBag.Classrooms = classrooms.Select(c => new SelectListItem
{
    Value = c.Id.ToString(),
    Text = c.Name
}).ToList();

            
            var enrollment = new Enrollment
            {
                StudentId = studentId ?? 0,
                ClassroomId = classroomId ?? 0
            };

            return View(enrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Enrollment enrollment, string academicYear, string semester)
        {
    Console.WriteLine($"StudentId: {enrollment.StudentId}");
    Console.WriteLine($"ClassroomId: {enrollment.ClassroomId}");
    Console.WriteLine($"AcademicYear: {academicYear}");
    Console.WriteLine($"Semester: {semester}");
    Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");
            if (ModelState.IsValid)
            {    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine($"Error: {error.ErrorMessage}");
        }
                var isEnrolled = await _enrollmentService.IsStudentEnrolledAsync(
                    enrollment.StudentId, 
                    enrollment.ClassroomId);

                if (isEnrolled)
                {
                    ViewBag.Error = "Student is already enrolled in this classroom";
                    var students = await _studentService.GetAllStudentsAsync();
                    var classrooms = await _classroomService.GetAllClassroomsAsync();
                    ViewBag.Students = new SelectList(students, "Id", "FirstName");
                    ViewBag.Classrooms = new SelectList(classrooms, "Id", "Name");
                    return View(enrollment);
                }

                enrollment.AcademicYear = academicYear;
                enrollment.Semester = semester;
                await _enrollmentService.CreateEnrollmentAsync(enrollment);
                return RedirectToAction(nameof(Index));
            }

            var studentsReload = await _studentService.GetAllStudentsAsync();
            var classroomsReload = await _classroomService.GetAllClassroomsAsync();
            ViewBag.Students = new SelectList(studentsReload, "Id", "FirstName");
            ViewBag.Classrooms = new SelectList(classroomsReload, "Id", "Name");
            return View(enrollment);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
            
            if (enrollment == null)
            {
                return NotFound();
            }

            var students = await _studentService.GetAllStudentsAsync();
            var classrooms = await _classroomService.GetAllClassroomsAsync();

            ViewBag.Students = new SelectList(students, "Id", "FirstName", enrollment.StudentId);
            ViewBag.Classrooms = new SelectList(classrooms, "Id", "Name", enrollment.ClassroomId);
            
            return View(enrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _enrollmentService.UpdateEnrollmentAsync(enrollment);
                return RedirectToAction(nameof(Index));
            }

            var students = await _studentService.GetAllStudentsAsync();
            var classrooms = await _classroomService.GetAllClassroomsAsync();
            ViewBag.Students = new SelectList(students, "Id", "FirstName");
            ViewBag.Classrooms = new SelectList(classrooms, "Id", "Name");
            return View(enrollment);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
            
            if (enrollment == null)
            {
                return NotFound();
            }
            
            return View(enrollment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _enrollmentService.DeleteEnrollmentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
