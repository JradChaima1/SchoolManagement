namespace School.Core.Models;

public class Teacher
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public DateTime HireDate { get; set; }
    public string? Specialization { get; set; }
    
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
