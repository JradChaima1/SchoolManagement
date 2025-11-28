namespace School.Core.Models;

public class Classroom
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public int GradeLevel { get; set; } 
    public int Capacity { get; set; }
    
    
    public ICollection<Enrollment> Enrollments { get; set; }
    public ICollection<Course> Courses { get; set; }
}
