namespace School.Services.Services;

using School.Core.Interfaces;
using School.Core.Models;

public class ClassroomService : IClassroomService
{
    private readonly IClassroomRepository _classroomRepository;
    private readonly IRepository<Enrollment> _enrollmentRepository;

    public ClassroomService(IClassroomRepository classroomRepository, IRepository<Enrollment> enrollmentRepository)
    {
        _classroomRepository = classroomRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<IEnumerable<Classroom>> GetAllClassroomsAsync()
    {
            return await _classroomRepository.GetClassroomsWithStudentsAsync(); 
    }

    public async Task<Classroom?> GetClassroomByIdAsync(int id)
    {
        return await _classroomRepository.GetClassroomWithDetailsAsync(id);
    }

    public async Task<Classroom> CreateClassroomAsync(Classroom classroom)
    {
        return await _classroomRepository.AddAsync(classroom);
    }

    public async Task UpdateClassroomAsync(Classroom classroom)
    {
        await _classroomRepository.UpdateAsync(classroom);
    }

    public async Task DeleteClassroomAsync(int id)
    {
        await _classroomRepository.DeleteAsync(id);
    }

  
    public async Task<int> GetClassroomStudentCountAsync(int classroomId)
    {
        var enrollments = await _enrollmentRepository.GetAllAsync();
        
        return enrollments
            .Count(e => e.ClassroomId == classroomId && e.Status == "Active");
    }

  
    public async Task<bool> IsClassroomFullAsync(int classroomId)
    {
        var classroom = await _classroomRepository.GetByIdAsync(classroomId);
        if (classroom == null) return false;

        var currentCount = await GetClassroomStudentCountAsync(classroomId);
        
        return currentCount >= classroom.Capacity;
    }
}
