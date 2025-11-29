namespace School.Core.Models;

public class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public int? SubjectId { get; set; }
    public int? ClassroomId { get; set; }
    public int? TeacherId { get; set; }  
    public string? Schedule { get; set; } 
    
    
    public Subject? Subject { get; set; }
    public Classroom? Classroom { get; set; }
    public Teacher? Teacher { get; set; }
}
