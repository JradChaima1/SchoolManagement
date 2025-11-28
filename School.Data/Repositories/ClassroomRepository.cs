namespace School.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using School.Core.Interfaces;
using School.Core.Models;

public class ClassroomRepository : Repository<Classroom>, IClassroomRepository
{
    public ClassroomRepository(SchoolContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Classroom>> GetClassroomsWithStudentsAsync()
    {
        return await _context.Classrooms
            .Include(c => c.Enrollments)
                .ThenInclude(e => e.Student)
            .Include(c => c.Courses)
                .ThenInclude(co => co.Subject)
            .ToListAsync();
    }

    public async Task<Classroom?> GetClassroomWithDetailsAsync(int id)
    {
        return await _context.Classrooms
            .Include(c => c.Enrollments)
                .ThenInclude(e => e.Student)
            .Include(c => c.Courses)
                .ThenInclude(co => co.Subject)
            .Include(c => c.Courses)
                .ThenInclude(co => co.Teacher)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
