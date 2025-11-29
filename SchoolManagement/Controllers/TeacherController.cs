using Microsoft.AspNetCore.Mvc;
using School.Core.Interfaces;
using School.Core.Models;
using SchoolManagement.Filters;

namespace SchoolManagement.Controllers
{
    [AuthorizeSession]
    public class TeachersController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly ISubjectService _subjectService;

        public TeachersController(ITeacherService teacherService, ISubjectService subjectService)
        {
            _teacherService = teacherService;
            _subjectService = subjectService;
        }

        public async Task<IActionResult> Index()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return View(teachers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            
            if (teacher == null)
            {
                return NotFound();
            }

            var courseCount = await _teacherService.GetTeacherCourseCountAsync(id);
            ViewBag.CourseCount = courseCount;

            return View(teacher);
        }

        public async Task<IActionResult> Create()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            ViewBag.Subjects = subjects;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.HireDate = DateTime.Now;
                await _teacherService.CreateTeacherAsync(teacher);
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            
            if (teacher == null)
            {
                return NotFound();
            }
            
            var subjects = await _subjectService.GetAllSubjectsAsync();
            ViewBag.Subjects = subjects;
            
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _teacherService.UpdateTeacherAsync(teacher);
                return RedirectToAction(nameof(Index));
            }
            
            return View(teacher);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            
            if (teacher == null)
            {
                return NotFound();
            }
            
            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _teacherService.DeleteTeacherAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
