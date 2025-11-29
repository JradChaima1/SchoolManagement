using Microsoft.AspNetCore.Mvc;
using School.Core.Interfaces;
using School.Core.Models;
using SchoolManagement.Filters;

namespace SchoolManagement.Controllers
{
    [AuthorizeSession]
    public class ParentsController : Controller
    {
        private readonly IParentService _parentService;
        private readonly IStudentService _studentService;

        public ParentsController(
            IParentService parentService,
            IStudentService studentService)
        {
            _parentService = parentService;
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var parents = await _parentService.GetAllParentsAsync();
            return View(parents);
        }

        public async Task<IActionResult> Details(int id)
        {
            var parent = await _parentService.GetParentByIdAsync(id);
            if (parent == null)
                return NotFound();

            return View(parent);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Students = await _studentService.GetAllStudentsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Parent parent)
        {
            if (ModelState.IsValid)
            {
                await _parentService.CreateParentAsync(parent);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Students = await _studentService.GetAllStudentsAsync();
            return View(parent);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var parent = await _parentService.GetParentByIdAsync(id);
            if (parent == null)
                return NotFound();

            ViewBag.Students = await _studentService.GetAllStudentsAsync();
            return View(parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Parent parent)
        {
            if (id != parent.ParentId)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _parentService.UpdateParentAsync(parent);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Students = await _studentService.GetAllStudentsAsync();
            return View(parent);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var parent = await _parentService.GetParentByIdAsync(id);
            if (parent == null)
                return NotFound();

            return View(parent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _parentService.DeleteParentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
