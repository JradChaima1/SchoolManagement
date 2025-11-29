using Microsoft.AspNetCore.Mvc;
using School.Core.Interfaces;
using School.Core.Models;
using SchoolManagement.Filters;

namespace SchoolManagement.Controllers
{
    [AuthorizeSession]
    public class SubjectsController : Controller
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public async Task<IActionResult> Index()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            return View(subjects);
        }

        public async Task<IActionResult> Details(int id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);
            
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectService.CreateSubjectAsync(subject);
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);
            
            if (subject == null)
            {
                return NotFound();
            }
            
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _subjectService.UpdateSubjectAsync(subject);
                return RedirectToAction(nameof(Index));
            }
            
            return View(subject);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);
            
            if (subject == null)
            {
                return NotFound();
            }
            
            return View(subject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _subjectService.DeleteSubjectAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}
