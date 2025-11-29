using School.Core.Interfaces;
using School.Core.Models;
using School.Data;
using Microsoft.EntityFrameworkCore;

namespace School.Services.Services
{
    public class ParentService : IParentService
    {
        private readonly SchoolContext _context;

        public ParentService(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Parent>> GetAllParentsAsync()
        {
            return await _context.Parents
                .Include(p => p.Students)
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync();
        }

        public async Task<Parent?> GetParentByIdAsync(int id)
        {
            return await _context.Parents
                .Include(p => p.Students)
                .FirstOrDefaultAsync(p => p.ParentId == id);
        }

        public async Task<IEnumerable<Parent>> GetParentsByStudentIdAsync(int studentId)
        {
            return await _context.Parents
                .Include(p => p.Students)
                .Where(p => p.Students.Any(s => s.Id == studentId))
                .ToListAsync();
        }

        public async Task<Parent> CreateParentAsync(Parent parent)
        {
            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();
            return parent;
        }

        public async Task UpdateParentAsync(Parent parent)
        {
            _context.Parents.Update(parent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteParentAsync(int id)
        {
            var parent = await _context.Parents.FindAsync(id);
            if (parent != null)
            {
                _context.Parents.Remove(parent);
                await _context.SaveChangesAsync();
            }
        }
    }
}
