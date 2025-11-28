namespace School.Core.Interfaces;

using School.Core.Models;

public interface ITeacherRepository : IRepository<Teacher>
{
    Task<IEnumerable<Teacher>> GetTeachersWithCoursesAsync();
    Task<Teacher?> GetTeacherWithDetailsAsync(int id);
}
