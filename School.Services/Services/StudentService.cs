namespace School.Services.Services;

using School.Core.Interfaces;
using School.Core.Models;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IRepository<Grade> _gradeRepository;

    public StudentService(IStudentRepository studentRepository, IRepository<Grade> gradeRepository)
    {
        _studentRepository = studentRepository;
        _gradeRepository = gradeRepository;
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _studentRepository.GetAllAsync();
    }

    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        return await _studentRepository.GetStudentWithDetailsAsync(id);
    }

    public async Task<Student> CreateStudentAsync(Student student)
    {
        return await _studentRepository.AddAsync(student);
    }

    public async Task UpdateStudentAsync(Student student)
    {
        await _studentRepository.UpdateAsync(student);
    }

    public async Task DeleteStudentAsync(int id)
    {
        await _studentRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Student>> GetStudentsByClassroomAsync(int classroomId)
    {
        return await _studentRepository.GetStudentsByClassroomAsync(classroomId);
    }


    public async Task<IEnumerable<Student>> SearchStudentsAsync(string searchTerm)
    {
        var students = await _studentRepository.GetAllAsync();
        
        return students
            .Where(s => s.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                       s.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .OrderBy(s => s.LastName)
            .ThenBy(s => s.FirstName)
            .ToList();
    }

  
    public async Task<decimal> GetStudentAverageGradeAsync(int studentId)
    {
        var grades = await _gradeRepository.GetAllAsync();
        
        var studentGrades = grades
            .Where(g => g.StudentId == studentId)
            .Select(g => g.Score)
            .ToList();

        return studentGrades.Any() ? studentGrades.Average() : 0;
    }

   
    public async Task<IEnumerable<Student>> GetTopPerformingStudentsAsync(int count)
    {
        var students = await _studentRepository.GetAllAsync();
        var grades = await _gradeRepository.GetAllAsync();

        var studentAverages = students
            .Select(s => new
            {
                Student = s,
                Average = grades
                    .Where(g => g.StudentId == s.Id)
                    .Select(g => g.Score)
                    .DefaultIfEmpty(0)
                    .Average()
            })
            .OrderByDescending(x => x.Average)
            .Take(count)
            .Select(x => x.Student)
            .ToList();

        return studentAverages;
    }
}
