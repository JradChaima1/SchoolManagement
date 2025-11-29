using School.Core.Interfaces;
using School.Core.Models;
using School.Data;
using Microsoft.EntityFrameworkCore;

namespace School.Services.Services
{
    public class GradeService : IGradeService
    {
        private readonly SchoolContext _context;

        public GradeService(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Grade>> GetAllGradesAsync()
        {
            return await _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .OrderByDescending(g => g.DateRecorded)
                .ToListAsync();
        }

        public async Task<Grade?> GetGradeByIdAsync(int id)
        {
            return await _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .FirstOrDefaultAsync(g => g.GradeId == id);
        }

        public async Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(int studentId)
        {
            return await _context.Grades
                .Include(g => g.Subject)
                .Where(g => g.StudentId == studentId)
                .OrderByDescending(g => g.DateRecorded)
                .ToListAsync();
        }

        public async Task<IEnumerable<Grade>> GetGradesByCourseIdAsync(int courseId)
        {
            // This method is now for getting grades by subject
            return await _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .Where(g => g.SubjectId == courseId) // courseId parameter is actually subjectId
                .OrderByDescending(g => g.DateRecorded)
                .ToListAsync();
        }

        public async Task<double> GetStudentAverageAsync(int studentId)
        {
            var grades = await _context.Grades
                .Where(g => g.StudentId == studentId)
                .ToListAsync();

            if (!grades.Any())
                return 0;

            return (double)grades.Average(g => g.Score);
        }

        public async Task<Grade> CreateGradeAsync(Grade grade)
        {
            grade.DateRecorded = DateTime.Now;
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
            return grade;
        }

        public async Task UpdateGradeAsync(Grade grade)
        {
            _context.Grades.Update(grade);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGradeAsync(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();
            }
        }
    }
}
