namespace School.Services.Services;

using School.Core.Interfaces;
using School.Core.Models;

public class CourseService : ICourseService
{
    private readonly IRepository<Course> _courseRepository;

    public CourseService(IRepository<Course> courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        return await _courseRepository.GetAllAsync();
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        return await _courseRepository.GetByIdAsync(id);
    }

    public async Task<Course> CreateCourseAsync(Course course)
    {
        return await _courseRepository.AddAsync(course);
    }

    public async Task UpdateCourseAsync(Course course)
    {
        await _courseRepository.UpdateAsync(course);
    }

    public async Task DeleteCourseAsync(int id)
    {
        await _courseRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Course>> GetCoursesByClassroomAsync(int classroomId)
    {
        var courses = await _courseRepository.GetAllAsync();
        return courses.Where(c => c.ClassroomId == classroomId).ToList();
    }

    public async Task<IEnumerable<Course>> GetCoursesByTeacherAsync(int teacherId)
    {
        var courses = await _courseRepository.GetAllAsync();
        return courses.Where(c => c.TeacherId == teacherId).ToList();
    }
}
