namespace School.Core.Interfaces;

using School.Core.Models;

public interface IClassroomRepository : IRepository<Classroom>
{
    Task<IEnumerable<Classroom>> GetClassroomsWithStudentsAsync();
    Task<Classroom?> GetClassroomWithDetailsAsync(int id);
}
