namespace School.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using School.Core.Interfaces;
using School.Core.Models;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(SchoolContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Student>> GetStudentsByClassroomAsync(int classroomId)
    {
        return await _context.Students
            .Where(s => s.Enrollments.Any(e => e.ClassroomId == classroomId))
            .Include(s => s.Parent)
            .ToListAsync();
    }

    public async Task<IEnumerable<Student>> GetStudentsWithEnrollmentsAsync()
    {
        return await _context.Students
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Classroom)
            .Include(s => s.Parent)
            .ToListAsync();
    }

    public async Task<Student?> GetStudentWithDetailsAsync(int id)
    {
        return await _context.Students
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Classroom)
            .Include(s => s.Grades)
                .ThenInclude(g => g.Subject)
            .Include(s => s.Attendances)
            .Include(s => s.Parent)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}
