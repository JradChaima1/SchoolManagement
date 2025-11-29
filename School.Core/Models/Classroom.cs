namespace School.Core.Models;

public class Classroom
{
    public int Id { get; set; }
   public string Name { get; set; } = string.Empty;
    public int GradeLevel { get; set; } 
    public int Capacity { get; set; }
    
    
  public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
public ICollection<Course> Courses { get; set; } = new List<Course>();
}
