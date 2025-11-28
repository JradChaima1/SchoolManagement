namespace School.Services.Services;

using School.Core.Interfaces;
using School.Core.Models;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IRepository<Course> _courseRepository;

    public TeacherService(ITeacherRepository teacherRepository, IRepository<Course> courseRepository)
    {
        _teacherRepository = teacherRepository;
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
    {
        return await _teacherRepository.GetAllAsync();
    }

    public async Task<Teacher?> GetTeacherByIdAsync(int id)
    {
        return await _teacherRepository.GetTeacherWithDetailsAsync(id);
    }

    public async Task<Teacher> CreateTeacherAsync(Teacher teacher)
    {
        return await _teacherRepository.AddAsync(teacher);
    }

    public async Task UpdateTeacherAsync(Teacher teacher)
    {
        await _teacherRepository.UpdateAsync(teacher);
    }

    public async Task DeleteTeacherAsync(int id)
    {
        await _teacherRepository.DeleteAsync(id);
    }


    public async Task<IEnumerable<Teacher>> GetTeachersBySubjectAsync(string subjectName)
    {
        var teachers = await _teacherRepository.GetTeachersWithCoursesAsync();
        
        return teachers
            .Where(t => t.Courses.Any(c => c.Subject.Name.Contains(subjectName, StringComparison.OrdinalIgnoreCase)))
            .Distinct()
            .ToList();
    }

    
    public async Task<int> GetTeacherCourseCountAsync(int teacherId)
    {
        var courses = await _courseRepository.GetAllAsync();
        
        return courses
            .Count(c => c.TeacherId == teacherId);
    }
}
