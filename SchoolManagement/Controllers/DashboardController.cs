using Microsoft.AspNetCore.Mvc;
using School.Core.Interfaces;
using SchoolManagement.Filters;

namespace SchoolManagement.Controllers
{
    [AuthorizeSession]
    public class DashboardController : Controller
    {
        private readonly IAnalyticsService _analyticsService;

        public DashboardController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        public async Task<IActionResult> Index()
        {
            var statistics = await _analyticsService.GetDashboardStatisticsAsync();
            return View(statistics);
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjectPerformance()
        {
            var data = await _analyticsService.GetSubjectPerformanceAsync();
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetClassroomStatistics()
        {
            var data = await _analyticsService.GetClassroomStatisticsAsync();
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetMonthlyEnrollments()
        {
            var data = await _analyticsService.GetMonthlyEnrollmentsAsync();
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetTeacherWorkload()
        {
            var data = await _analyticsService.GetTeacherWorkloadAsync();
            return Json(data);
        }
    }
}
