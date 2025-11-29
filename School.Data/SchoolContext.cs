namespace School.Data;

using Microsoft.EntityFrameworkCore;
using School.Core.Models;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
    }


    public DbSet<Student> Students => Set<Student>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Classroom> Classrooms => Set<Classroom>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<Grade> Grades => Set<Grade>();
    public DbSet<Parent> Parents => Set<Parent>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

          

    modelBuilder.Entity<Student>()
        .HasOne(s => s.Parent)
        .WithMany(p => p.Students)
        .HasForeignKey(s => s.ParentId)
        .OnDelete(DeleteBehavior.SetNull);

    
    modelBuilder.Entity<Enrollment>()
        .HasOne(e => e.Student)
        .WithMany(s => s.Enrollments)
        .HasForeignKey(e => e.StudentId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Enrollment>()
        .HasOne(e => e.Classroom)
        .WithMany(c => c.Enrollments)
        .HasForeignKey(e => e.ClassroomId)
        .OnDelete(DeleteBehavior.Cascade);

    
    modelBuilder.Entity<Course>()
        .HasOne(c => c.Subject)
        .WithMany(s => s.Courses)
        .HasForeignKey(c => c.SubjectId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Course>()
        .HasOne(c => c.Classroom)
        .WithMany(cl => cl.Courses)
        .HasForeignKey(c => c.ClassroomId)
        .OnDelete(DeleteBehavior.SetNull);

    modelBuilder.Entity<Course>()
        .HasOne(c => c.Teacher)
        .WithMany(t => t.Courses)
        .HasForeignKey(c => c.TeacherId)
        .OnDelete(DeleteBehavior.SetNull);

    
    modelBuilder.Entity<Grade>()
        .HasOne(g => g.Student)
        .WithMany(s => s.Grades)
        .HasForeignKey(g => g.StudentId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Grade>()
        .HasOne(g => g.Subject)
        .WithMany(s => s.Grades)
        .HasForeignKey(g => g.SubjectId)
        .OnDelete(DeleteBehavior.Restrict);

    
    modelBuilder.Entity<User>()
        .HasOne(u => u.Role)
        .WithMany(r => r.Users)
        .HasForeignKey(u => u.RoleId)
        .OnDelete(DeleteBehavior.Restrict);

  
    modelBuilder.Entity<Student>()
        .Property(s => s.FirstName)
        .HasMaxLength(100)
        .IsRequired();

    modelBuilder.Entity<Student>()
        .Property(s => s.LastName)
        .HasMaxLength(100)
        .IsRequired();

    modelBuilder.Entity<Teacher>()
        .Property(t => t.FirstName)
        .HasMaxLength(100)
        .IsRequired();

    modelBuilder.Entity<Teacher>()
        .Property(t => t.LastName)
        .HasMaxLength(100)
        .IsRequired();

    modelBuilder.Entity<Classroom>()
        .Property(c => c.Name)
        .HasMaxLength(50)
        .IsRequired();

    modelBuilder.Entity<Subject>()
        .Property(s => s.Name)
        .HasMaxLength(200)
        .IsRequired();

    modelBuilder.Entity<Grade>()
        .Property(g => g.Score)
        .HasPrecision(5, 2); 




    }
}
