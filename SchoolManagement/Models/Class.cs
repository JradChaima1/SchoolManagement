using System.Collections.Generic;

namespace SchoolManagement.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Subject { get; set; }

        
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}