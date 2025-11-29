using School.Core.Models;

namespace School.Core.Interfaces
{
    public interface IGradeService
    {
        Task<IEnumerable<Grade>> GetAllGradesAsync();
        Task<Grade?> GetGradeByIdAsync(int id);
        Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(int studentId);
        Task<IEnumerable<Grade>> GetGradesByCourseIdAsync(int courseId);
        Task<double> GetStudentAverageAsync(int studentId);
        Task<Grade> CreateGradeAsync(Grade grade);
        Task UpdateGradeAsync(Grade grade);
        Task DeleteGradeAsync(int id);
    }
}
