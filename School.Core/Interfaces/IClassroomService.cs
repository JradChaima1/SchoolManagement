namespace School.Core.Interfaces;

using School.Core.Models;

public interface IClassroomService
{
    Task<IEnumerable<Classroom>> GetAllClassroomsAsync();
    Task<Classroom?> GetClassroomByIdAsync(int id);
    Task<Classroom> CreateClassroomAsync(Classroom classroom);
    Task UpdateClassroomAsync(Classroom classroom);
    Task DeleteClassroomAsync(int id);
    Task<int> GetClassroomStudentCountAsync(int classroomId);
    Task<bool> IsClassroomFullAsync(int classroomId);
}
