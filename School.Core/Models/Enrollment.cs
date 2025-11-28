namespace School.Core.Models;
public class Enrollment
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int ClassroomId { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public string AcademicYear { get; set; }
    public string Semester { get; set; } 
    public string Status { get; set; } 
    
    public Student Student { get; set; }
    public Classroom Classroom { get; set; }
}
