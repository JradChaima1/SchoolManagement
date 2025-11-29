using Microsoft.AspNetCore.Mvc;
using School.Core.Interfaces;
using School.Core.Models;
using SchoolManagement.Filters;

namespace SchoolManagement.Controllers
{
    [AuthorizeSession]
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;
        private readonly ISubjectService _subjectService;
        private readonly IClassroomService _classroomService;

        public CoursesController(
            ICourseService courseService,
            ITeacherService teacherService,
            ISubjectService subjectService,
            IClassroomService classroomService)
        {
            _courseService = courseService;
            _teacherService = teacherService;
            _subjectService = subjectService;
            _classroomService = classroomService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return View(courses);
        }

        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Teachers = await _teacherService.GetAllTeachersAsync();
            ViewBag.Subjects = await _subjectService.GetAllSubjectsAsync();
            ViewBag.Classrooms = await _classroomService.GetAllClassroomsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                await _courseService.CreateCourseAsync(course);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Teachers = await _teacherService.GetAllTeachersAsync();
            ViewBag.Subjects = await _subjectService.GetAllSubjectsAsync();
            ViewBag.Classrooms = await _classroomService.GetAllClassroomsAsync();
            return View(course);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();

            ViewBag.Teachers = await _teacherService.GetAllTeachersAsync();
            ViewBag.Subjects = await _subjectService.GetAllSubjectsAsync();
            ViewBag.Classrooms = await _classroomService.GetAllClassroomsAsync();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (id != course.CourseId)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _courseService.UpdateCourseAsync(course);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Teachers = await _teacherService.GetAllTeachersAsync();
            ViewBag.Subjects = await _subjectService.GetAllSubjectsAsync();
            ViewBag.Classrooms = await _classroomService.GetAllClassroomsAsync();
            return View(course);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
