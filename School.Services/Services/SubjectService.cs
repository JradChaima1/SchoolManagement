namespace School.Services.Services;

using School.Core.Interfaces;
using School.Core.Models;

public class SubjectService : ISubjectService
{
    private readonly IRepository<Subject> _subjectRepository;

    public SubjectService(IRepository<Subject> subjectRepository)
    {
        _subjectRepository = subjectRepository;
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
        await _subjectRepository.DeleteAsync(id);
    }
}
