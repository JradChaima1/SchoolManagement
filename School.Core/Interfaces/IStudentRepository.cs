namespace School.Core.Interfaces;

using School.Core.Models;

public interface IStudentRepository : IRepository<Student>
{
    Task<IEnumerable<Student>> GetStudentsByClassroomAsync(int classroomId);
    Task<IEnumerable<Student>> GetStudentsWithEnrollmentsAsync();
    Task<Student?> GetStudentWithDetailsAsync(int id);
}
