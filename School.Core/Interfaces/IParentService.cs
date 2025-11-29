using School.Core.Models;

namespace School.Core.Interfaces
{
    public interface IParentService
    {
        Task<IEnumerable<Parent>> GetAllParentsAsync();
        Task<Parent?> GetParentByIdAsync(int id);
        Task<IEnumerable<Parent>> GetParentsByStudentIdAsync(int studentId);
        Task<Parent> CreateParentAsync(Parent parent);
        Task UpdateParentAsync(Parent parent);
        Task DeleteParentAsync(int id);
    }
}
