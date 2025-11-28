namespace School.Core.Models;

public class Course
{
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public int ClassroomId { get; set; }
    public int TeacherId { get; set; }
    public string? Schedule { get; set; } 
    public string Semester { get; set; } 
    
    
    public Subject Subject { get; set; }
    public Classroom Classroom { get; set; }
    public Teacher Teacher { get; set; }
    public ICollection<Attendance> Attendances { get; set; }
}
