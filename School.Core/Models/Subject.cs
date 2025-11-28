namespace School.Core.Models;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int Credits { get; set; }

    public ICollection<Course> Courses { get; set; }
    public ICollection<Grade> Grades { get; set; }
}
