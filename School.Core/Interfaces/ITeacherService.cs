namespace School.Core.Interfaces;

using School.Core.Models;

public interface ITeacherService
{
    Task<IEnumerable<Teacher>> GetAllTeachersAsync();
    Task<Teacher?> GetTeacherByIdAsync(int id);
    Task<Teacher> CreateTeacherAsync(Teacher teacher);
    Task UpdateTeacherAsync(Teacher teacher);
    Task DeleteTeacherAsync(int id);
    Task<IEnumerable<Teacher>> GetTeachersBySubjectAsync(string subjectName);
    Task<int> GetTeacherCourseCountAsync(int teacherId);
}
