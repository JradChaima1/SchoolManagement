namespace School.Core.Models;

public class Grade
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public decimal Score { get; set; }
    public DateTime ExamDate { get; set; }
    public string? ExamType { get; set; } 
    public string? Comments { get; set; }
    
   
    public Student Student { get; set; }
    public Subject Subject { get; set; }
}
