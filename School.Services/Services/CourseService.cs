namespace School.Services.Services;

using Microsoft.EntityFrameworkCore;
using School.Core.Interfaces;
using School.Core.Models;
using School.Data;

public class CourseService : ICourseService
{
    private readonly SchoolContext _context;

    public CourseService(SchoolContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        return await _context.Courses
            .Include(c => c.Subject)
            .Include(c => c.Teacher)
            .Include(c => c.Classroom)
            .ToListAsync();
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.Subject)
            .Include(c => c.Teacher)
            .Include(c => c.Classroom)
            .FirstOrDefaultAsync(c => c.CourseId == id);
    }

    public async Task<Course> CreateCourseAsync(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task UpdateCourseAsync(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCourseAsync(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Course>> GetCoursesByClassroomAsync(int classroomId)
    {
        return await _context.Courses
            .Include(c => c.Subject)
            .Include(c => c.Teacher)
            .Include(c => c.Classroom)
            .Where(c => c.ClassroomId == classroomId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetCoursesByTeacherAsync(int teacherId)
    {
        return await _context.Courses
            .Include(c => c.Subject)
            .Include(c => c.Teacher)
            .Include(c => c.Classroom)
            .Where(c => c.TeacherId == teacherId)
            .ToListAsync();
    }
}
