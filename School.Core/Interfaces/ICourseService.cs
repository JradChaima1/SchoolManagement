namespace School.Core.Interfaces;

using School.Core.Models;

public interface ICourseService
{
    Task<IEnumerable<Course>> GetAllCoursesAsync();
    Task<Course?> GetCourseByIdAsync(int id);
    Task<Course> CreateCourseAsync(Course course);
    Task UpdateCourseAsync(Course course);
    Task DeleteCourseAsync(int id);
    Task<IEnumerable<Course>> GetCoursesByClassroomAsync(int classroomId);
    Task<IEnumerable<Course>> GetCoursesByTeacherAsync(int teacherId);
}
