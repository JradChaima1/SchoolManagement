namespace School.Services.Services;

using Microsoft.EntityFrameworkCore;
using School.Core.Interfaces;
using School.Core.Models;
using School.Data;

public class SubjectService : ISubjectService
{
    private readonly IRepository<Subject> _subjectRepository;
    private readonly SchoolContext _context;

    public SubjectService(IRepository<Subject> subjectRepository, SchoolContext context)
    {
        _subjectRepository = subjectRepository;
        _context = context;
    }

    public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
    {
        return await _subjectRepository.GetAllAsync();
    }

    public async Task<Subject?> GetSubjectByIdAsync(int id)
    {
        return await _subjectRepository.GetByIdAsync(id);
    }

    public async Task<Subject> CreateSubjectAsync(Subject subject)
    {
        return await _subjectRepository.AddAsync(subject);
    }

    public async Task UpdateSubjectAsync(Subject subject)
    {
        await _subjectRepository.UpdateAsync(subject);
    }

    public async Task DeleteSubjectAsync(int id)
    {
       
        var hasCourses = await _context.Courses.AnyAsync(c => c.SubjectId == id);
        if (hasCourses)
        {
            throw new InvalidOperationException("Cannot delete this subject because it is being used in one or more courses. Please remove or reassign the courses first.");
        }

   
        var hasGrades = await _context.Grades.AnyAsync(g => g.SubjectId == id);
        if (hasGrades)
        {
            throw new InvalidOperationException("Cannot delete this subject because it has student grades recorded. Please remove the grades first.");
        }

        await _subjectRepository.DeleteAsync(id);
    }
}
