using Microsoft.AspNetCore.Mvc;
using School.Core.Interfaces;
using School.Core.Models;
using SchoolManagement.Filters;

namespace SchoolManagement.Controllers
{
    [AuthorizeSession]
    public class GradesController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly IStudentService _studentService;
        private readonly ISubjectService _subjectService;

        public GradesController(
            IGradeService gradeService,
            IStudentService studentService,
            ISubjectService subjectService)
        {
            _gradeService = gradeService;
            _studentService = studentService;
            _subjectService = subjectService;
        }

        public async Task<IActionResult> Index()
        {
            var grades = await _gradeService.GetAllGradesAsync();
            return View(grades);
        }

        public async Task<IActionResult> Details(int id)
        {
            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null)
                return NotFound();

            return View(grade);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Students = await _studentService.GetAllStudentsAsync();
            ViewBag.Subjects = await _subjectService.GetAllSubjectsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Grade grade)
        {
            if (ModelState.IsValid)
            {
                await _gradeService.CreateGradeAsync(grade);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Students = await _studentService.GetAllStudentsAsync();
            ViewBag.Subjects = await _subjectService.GetAllSubjectsAsync();
            return View(grade);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null)
                return NotFound();

            ViewBag.Students = await _studentService.GetAllStudentsAsync();
            ViewBag.Subjects = await _subjectService.GetAllSubjectsAsync();
            return View(grade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Grade grade)
        {
            if (id != grade.GradeId)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _gradeService.UpdateGradeAsync(grade);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Students = await _studentService.GetAllStudentsAsync();
            ViewBag.Subjects = await _subjectService.GetAllSubjectsAsync();
            return View(grade);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null)
                return NotFound();

            return View(grade);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _gradeService.DeleteGradeAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ByStudent(int studentId)
        {
            var grades = await _gradeService.GetGradesByStudentIdAsync(studentId);
            var average = await _gradeService.GetStudentAverageAsync(studentId);
            ViewBag.StudentId = studentId;
            ViewBag.Average = average;
            return View("Index", grades);
        }
    }
}
