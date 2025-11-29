using Microsoft.AspNetCore.Mvc;
using School.Core.Interfaces;
using School.Core.Models;
using SchoolManagement.Filters;

namespace SchoolManagement.Controllers
{
    [AuthorizeSession]
    public class ClassroomsController : Controller
    {
        private readonly IClassroomService _classroomService;

        public ClassroomsController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        public async Task<IActionResult> Index()
        {
            var classrooms = await _classroomService.GetAllClassroomsAsync();
            return View(classrooms);
        }

        public async Task<IActionResult> Details(int id)
        {
            var classroom = await _classroomService.GetClassroomByIdAsync(id);
            
            if (classroom == null)
            {
                return NotFound();
            }

            var studentCount = await _classroomService.GetClassroomStudentCountAsync(id);
            var isFull = await _classroomService.IsClassroomFullAsync(id);
            
            ViewBag.StudentCount = studentCount;
            ViewBag.IsFull = isFull;

            return View(classroom);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Classroom classroom)
        {
            if (ModelState.IsValid)
            {
                  var errors = ModelState.Values.SelectMany(v => v.Errors);
    foreach (var error in errors)
    {
        Console.WriteLine(error.ErrorMessage);
    }
                await _classroomService.CreateClassroomAsync(classroom);
                return RedirectToAction(nameof(Index));
            }
            return View(classroom);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var classroom = await _classroomService.GetClassroomByIdAsync(id);
            
            if (classroom == null)
            {
                return NotFound();
            }
            
            return View(classroom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Classroom classroom)
        {
            if (id != classroom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _classroomService.UpdateClassroomAsync(classroom);
                return RedirectToAction(nameof(Index));
            }
            
            return View(classroom);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var classroom = await _classroomService.GetClassroomByIdAsync(id);
            
            if (classroom == null)
            {
                return NotFound();
            }
            
            return View(classroom);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _classroomService.DeleteClassroomAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
