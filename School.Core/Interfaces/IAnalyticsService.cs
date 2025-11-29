namespace School.Core.Interfaces;

public interface IAnalyticsService
{
    Task<DashboardStatistics> GetDashboardStatisticsAsync();
    Task<IEnumerable<SubjectPerformance>> GetSubjectPerformanceAsync();
    Task<IEnumerable<ClassroomStatistics>> GetClassroomStatisticsAsync();
    Task<IEnumerable<MonthlyEnrollment>> GetMonthlyEnrollmentsAsync();
    Task<IEnumerable<TeacherWorkload>> GetTeacherWorkloadAsync();
}

public class DashboardStatistics
{
    public int TotalStudents { get; set; }
    public int TotalTeachers { get; set; }
    public int TotalClassrooms { get; set; }
    public int TotalCourses { get; set; }
    public int TotalSubjects { get; set; }
    public int TotalParents { get; set; }
    public double AverageGrade { get; set; }
}

public class SubjectPerformance
{
    public string SubjectName { get; set; } = string.Empty;
    public double AverageScore { get; set; }
    public int StudentCount { get; set; }
    public double PassRate { get; set; }
}

public class ClassroomStatistics
{
    public string ClassroomName { get; set; } = string.Empty;
    public int StudentCount { get; set; }
    public int CourseCount { get; set; }
    public double AverageGrade { get; set; }
}

public class MonthlyEnrollment
{
    public string Month { get; set; } = string.Empty;
    public int Count { get; set; }
    public int Year { get; set; }
}

public class TeacherWorkload
{
    public string TeacherName { get; set; } = string.Empty;
    public int CourseCount { get; set; }
    public int StudentCount { get; set; }
}
