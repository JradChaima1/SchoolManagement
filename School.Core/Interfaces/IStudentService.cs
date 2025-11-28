namespace School.Core.Interfaces;

using School.Core.Models;

public interface IStudentService
{
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task<Student?> GetStudentByIdAsync(int id);
    Task<Student> CreateStudentAsync(Student student);
    Task UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(int id);
    Task<IEnumerable<Student>> GetStudentsByClassroomAsync(int classroomId);
    Task<IEnumerable<Student>> SearchStudentsAsync(string searchTerm);
    Task<decimal> GetStudentAverageGradeAsync(int studentId);
    Task<IEnumerable<Student>> GetTopPerformingStudentsAsync(int count);
}
