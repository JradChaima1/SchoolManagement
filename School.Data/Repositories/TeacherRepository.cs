namespace School.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using School.Core.Interfaces;
using School.Core.Models;

public class TeacherRepository : Repository<Teacher>, ITeacherRepository
{
    public TeacherRepository(SchoolContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Teacher>> GetTeachersWithCoursesAsync()
    {
        return await _context.Teachers
            .Include(t => t.Courses)
                .ThenInclude(c => c.Subject)
            .Include(t => t.Courses)
                .ThenInclude(c => c.Classroom)
            .ToListAsync();
    }

    public async Task<Teacher?> GetTeacherWithDetailsAsync(int id)
    {
        return await _context.Teachers
            .Include(t => t.Courses)
                .ThenInclude(c => c.Subject)
            .Include(t => t.Courses)
                .ThenInclude(c => c.Classroom)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}
