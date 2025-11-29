namespace School.Services.Services;

using School.Core.Interfaces;
using School.Core.Models;

public class EnrollmentService : IEnrollmentService
{
    private readonly IRepository<Enrollment> _enrollmentRepository;

    public EnrollmentService(IRepository<Enrollment> enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
    {
        return await _enrollmentRepository.GetAllAsync();
    }

    public async Task<Enrollment?> GetEnrollmentByIdAsync(int id)
    {
        return await _enrollmentRepository.GetByIdAsync(id);
    }

    public async Task<Enrollment> CreateEnrollmentAsync(Enrollment enrollment)
    {
        enrollment.EnrollmentDate = DateTime.Now;
        enrollment.Status = "Active";
        return await _enrollmentRepository.AddAsync(enrollment);
    }

    public async Task UpdateEnrollmentAsync(Enrollment enrollment)
    {
        await _enrollmentRepository.UpdateAsync(enrollment);
    }

    public async Task DeleteEnrollmentAsync(int id)
    {
        await _enrollmentRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentAsync(int studentId)
    {
        var enrollments = await _enrollmentRepository.GetAllAsync();
        return enrollments.Where(e => e.StudentId == studentId).ToList();
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByClassroomAsync(int classroomId)
    {
        var enrollments = await _enrollmentRepository.GetAllAsync();
        return enrollments.Where(e => e.ClassroomId == classroomId).ToList();
    }

    public async Task<bool> IsStudentEnrolledAsync(int studentId, int classroomId)
    {
        var enrollments = await _enrollmentRepository.GetAllAsync();
        return enrollments.Any(e => e.StudentId == studentId && 
                                   e.ClassroomId == classroomId && 
                                   e.Status == "Active");
    }
}
