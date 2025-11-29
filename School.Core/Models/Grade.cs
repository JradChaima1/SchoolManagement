namespace School.Core.Models;

public class Grade
{
    public int GradeId { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public decimal Score { get; set; }
    public DateTime DateRecorded { get; set; }
    public string? Comments { get; set; }
    
   
    public Student? Student { get; set; }
    public Subject? Subject { get; set; }
}
