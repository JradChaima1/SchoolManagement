namespace School.Core.Models;

public class Parent
{
    public int ParentId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
    
    
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
