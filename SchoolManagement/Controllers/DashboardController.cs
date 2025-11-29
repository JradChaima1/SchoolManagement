using Microsoft.AspNetCore.Mvc;
using School.Core.Interfaces;
using SchoolManagement.Filters;

namespace SchoolManagement.Controllers
{
    [AuthorizeSession]
    public class DashboardController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly IClassroomService _classroomService;

        public DashboardController(
            IStudentService studentService,
            ITeacherService teacherService,
            IClassroomService classroomService)
        {
            _studentService = studentService;
            _teacherService = teacherService;
            _classroomService = classroomService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllStudentsAsync();
            var teachers = await _teacherService.GetAllTeachersAsync();
            var classrooms = await _classroomService.GetAllClassroomsAsync();
            var topStudents = await _studentService.GetTopPerformingStudentsAsync(5);

            ViewBag.TotalStudents = students.Count();
            ViewBag.TotalTeachers = teachers.Count();
            ViewBag.TotalClassrooms = classrooms.Count();
            ViewBag.TopStudents = topStudents;

            return View();
        }
    }
}
