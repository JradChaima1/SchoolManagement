using Microsoft.EntityFrameworkCore;
using School.Core.Interfaces;
using School.Data;

namespace School.Services.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly SchoolContext _context;

        public AnalyticsService(SchoolContext context)
        {
            _context = context;
        }

        public async Task<DashboardStatistics> GetDashboardStatisticsAsync()
        {
            var totalStudents = await _context.Students.CountAsync();
            var totalTeachers = await _context.Teachers.CountAsync();
            var totalClassrooms = await _context.Classrooms.CountAsync();
            var totalCourses = await _context.Courses.CountAsync();
            var totalSubjects = await _context.Subjects.CountAsync();
            var totalParents = await _context.Parents.CountAsync();

            var grades = await _context.Grades.ToListAsync();
            var averageGrade = grades.Any() ? (double)grades.Average(g => g.Score) : 0;

            return new DashboardStatistics
            {
                TotalStudents = totalStudents,
                TotalTeachers = totalTeachers,
                TotalClassrooms = totalClassrooms,
                TotalCourses = totalCourses,
                TotalSubjects = totalSubjects,
                TotalParents = totalParents,
                AverageGrade = Math.Round(averageGrade, 2)
            };
        }

        public async Task<IEnumerable<SubjectPerformance>> GetSubjectPerformanceAsync()
        {
            var subjectPerformance = await _context.Grades
                .Include(g => g.Subject)
                .GroupBy(g => new { g.SubjectId, g.Subject!.Name })
                .Select(g => new SubjectPerformance
                {
                    SubjectName = g.Key.Name,
                    AverageScore = Math.Round((double)g.Average(x => x.Score), 2),
                    StudentCount = g.Select(x => x.StudentId).Distinct().Count(),
                    PassRate = Math.Round((double)g.Count(x => x.Score >= 10) / g.Count() * 100, 2)
                })
                .OrderByDescending(s => s.AverageScore)
                .ToListAsync();

            return subjectPerformance;
        }

        public async Task<IEnumerable<ClassroomStatistics>> GetClassroomStatisticsAsync()
        {
            var classroomStats = await _context.Classrooms
                .Select(c => new ClassroomStatistics
                {
                    ClassroomName = c.Name,
                    StudentCount = c.Enrollments.Count(e => e.Status.ToLower() == "active"),
                    CourseCount = c.Courses.Count(),
                    AverageGrade = c.Enrollments
                        .Where(e => e.Student!.Grades.Any())
                        .SelectMany(e => e.Student!.Grades)
                        .Average(g => (double?)g.Score) ?? 0
                })
                .OrderByDescending(c => c.StudentCount)
                .ToListAsync();

            return classroomStats.Select(c => new ClassroomStatistics
            {
                ClassroomName = c.ClassroomName,
                StudentCount = c.StudentCount,
                CourseCount = c.CourseCount,
                AverageGrade = Math.Round(c.AverageGrade, 2)
            });
        }



      public async Task<IEnumerable<MonthlyEnrollment>> GetMonthlyEnrollmentsAsync()
{
    var currentYear = DateTime.Now.Year;
    var startYear = currentYear - 4; // Show last 5 years including current year
    
    var enrollments = await _context.Enrollments
        .Where(e => e.EnrollmentDate.Year >= startYear)
        .GroupBy(e => e.EnrollmentDate.Year)
        .Select(g => new MonthlyEnrollment
        {
            Year = g.Key,
            Month = g.Key.ToString(), // Use year as the label
            Count = g.Count()
        })
        .OrderBy(m => m.Year)
        .ToListAsync();

    return enrollments;
}


        public async Task<IEnumerable<TeacherWorkload>> GetTeacherWorkloadAsync()
        {
            var teacherWorkload = await _context.Teachers
                .Include(t => t.Courses)
                    .ThenInclude(c => c.Classroom)
                        .ThenInclude(cl => cl!.Enrollments)
                .Select(t => new TeacherWorkload
                {
                    TeacherName = t.FirstName + " " + t.LastName,
                    CourseCount = t.Courses.Count(),
                    StudentCount = t.Courses
                        .Where(c => c.Classroom != null)
                        .SelectMany(c => c.Classroom!.Enrollments)
                        .Select(e => e.StudentId)
                        .Distinct()
                        .Count()
                })
                .OrderByDescending(t => t.CourseCount)
                .ToListAsync();

            return teacherWorkload;
        }
    }
}
