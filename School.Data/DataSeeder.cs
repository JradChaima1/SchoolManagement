using School.Core.Models;

namespace School.Data
{
    public static class DataSeeder
    {
        public static void SeedData(SchoolContext context)
        {
            
            if (context.Students.Any() || context.Teachers.Any())
            {
                return; 
            }

      
            var adminRole = new Role { Name = "Admin" };
            var teacherRole = new Role { Name = "Teacher" };
            var studentRole = new Role { Name = "Student" };
            context.Roles.AddRange(adminRole, teacherRole, studentRole);
            context.SaveChanges();

        
            var adminUser = new User
            {
                Username = "admin",
                PasswordHash = "admin123",
                Email = "admin@school.com",
                RoleId = adminRole.Id,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Users.Add(adminUser);
            context.SaveChanges();

      
            var parents = new List<Parent>
            {
                new Parent { FirstName = "Ahmed", LastName = "Ben Ali", Email = "ahmed.benali@email.com", PhoneNumber = "20123456", Relationship = "Father" },
                new Parent { FirstName = "Fatma", LastName = "Trabelsi", Email = "fatma.trabelsi@email.com", PhoneNumber = "20234567", Relationship = "Mother" },
                new Parent { FirstName = "Mohamed", LastName = "Gharbi", Email = "mohamed.gharbi@email.com", PhoneNumber = "20345678", Relationship = "Father" },
                new Parent { FirstName = "Leila", LastName = "Mansour", Email = "leila.mansour@email.com", PhoneNumber = "20456789", Relationship = "Mother" },
                new Parent { FirstName = "Karim", LastName = "Bouazizi", Email = "karim.bouazizi@email.com", PhoneNumber = "20567890", Relationship = "Father" }
            };
            context.Parents.AddRange(parents);
            context.SaveChanges();

          
            var classrooms = new List<Classroom>
            {
                new Classroom { Name = "1A", Capacity = 30, GradeLevel = 1 },
                new Classroom { Name = "1B", Capacity = 30, GradeLevel = 1 },
                new Classroom { Name = "2A", Capacity = 28, GradeLevel = 2 },
                new Classroom { Name = "2B", Capacity = 28, GradeLevel = 2 },
                new Classroom { Name = "3A", Capacity = 25, GradeLevel = 3 },
                new Classroom { Name = "3B", Capacity = 25, GradeLevel = 3 }
            };
            context.Classrooms.AddRange(classrooms);
            context.SaveChanges();

           
            var teachers = new List<Teacher>
            {
                new Teacher { FirstName = "Sami", LastName = "Khaled", Email = "sami.khaled@school.com", PhoneNumber = "21123456", HireDate = DateTime.Now.AddYears(-5), Specialization = "Mathematics" },
                new Teacher { FirstName = "Nadia", LastName = "Hamdi", Email = "nadia.hamdi@school.com", PhoneNumber = "21234567", HireDate = DateTime.Now.AddYears(-4), Specialization = "Physics" },
                new Teacher { FirstName = "Hichem", LastName = "Jebali", Email = "hichem.jebali@school.com", PhoneNumber = "21345678", HireDate = DateTime.Now.AddYears(-6), Specialization = "French" },
                new Teacher { FirstName = "Amira", LastName = "Sassi", Email = "amira.sassi@school.com", PhoneNumber = "21456789", HireDate = DateTime.Now.AddYears(-3), Specialization = "English" },
                new Teacher { FirstName = "Youssef", LastName = "Mejri", Email = "youssef.mejri@school.com", PhoneNumber = "21567890", HireDate = DateTime.Now.AddYears(-7), Specialization = "History" },
                new Teacher { FirstName = "Salma", LastName = "Dridi", Email = "salma.dridi@school.com", PhoneNumber = "21678901", HireDate = DateTime.Now.AddYears(-2), Specialization = "Biology" }
            };
            context.Teachers.AddRange(teachers);
            context.SaveChanges();

         
            var subjects = new List<Subject>
            {
                new Subject { Name = "Mathematics", Description = "Algebra, Geometry, Calculus", Credits = 4 },
                new Subject { Name = "Physics", Description = "Mechanics, Electricity, Optics", Credits = 4 },
                new Subject { Name = "French", Description = "Grammar, Literature, Writing", Credits = 3 },
                new Subject { Name = "English", Description = "Grammar, Conversation, Literature", Credits = 3 },
                new Subject { Name = "History", Description = "World History, National History", Credits = 2 },
                new Subject { Name = "Biology", Description = "Cell Biology, Genetics, Ecology", Credits = 3 },
                new Subject { Name = "Chemistry", Description = "Organic, Inorganic Chemistry", Credits = 3 },
                new Subject { Name = "Arabic", Description = "Grammar, Literature, Poetry", Credits = 3 }
            };
            context.Subjects.AddRange(subjects);
            context.SaveChanges();

          
            var students = new List<Student>();
            var random = new Random();
            var firstNames = new[] { "Ali", "Yasmine", "Omar", "Ines", "Mehdi", "Sarra", "Khalil", "Mariem", "Rami", "Nour", "Fares", "Hana", "Aymen", "Rim", "Bilel" };
            var lastNames = new[] { "Ben Ahmed", "Trabelsi", "Gharbi", "Mansour", "Bouazizi", "Khaled", "Hamdi", "Jebali", "Sassi", "Mejri" };

            for (int i = 0; i < 50; i++)
            {
                students.Add(new Student
                {
                    FirstName = firstNames[random.Next(firstNames.Length)],
                    LastName = lastNames[random.Next(lastNames.Length)],
                    DateOfBirth = DateTime.Now.AddYears(-random.Next(15, 19)),
                    Email = $"student{i + 1}@school.com",
                    PhoneNumber = $"2{random.Next(1000000, 9999999)}",
                    EnrollmentDate = DateTime.Now.AddMonths(-random.Next(1, 24)),
                    ParentId = parents[random.Next(parents.Count)].ParentId
                });
            }
            context.Students.AddRange(students);
            context.SaveChanges();

          
            var enrollments = new List<Enrollment>();
            foreach (var student in students)
            {
                enrollments.Add(new Enrollment
                {
                    StudentId = student.Id,
                    ClassroomId = classrooms[random.Next(classrooms.Count)].Id,
                    EnrollmentDate = student.EnrollmentDate,
                    AcademicYear = "2024-2025",
                    Semester = "Fall",
                    Status = "Active"
                });
            }
            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();

        
            var courses = new List<Course>();
            foreach (var classroom in classrooms)
            {
                foreach (var subject in subjects.Take(6))
                {
                    courses.Add(new Course
                    {
                        CourseName = $"{subject.Name} - {classroom.Name}",
                        SubjectId = subject.Id,
                        ClassroomId = classroom.Id,
                        TeacherId = teachers[random.Next(teachers.Count)].Id,
                        Schedule = $"Mon/Wed {random.Next(8, 16)}:00"
                    });
                }
            }
            context.Courses.AddRange(courses);
            context.SaveChanges();

         
            var grades = new List<Grade>();
            foreach (var student in students)
            {
                foreach (var subject in subjects.Take(6))
                {
                    grades.Add(new Grade
                    {
                        StudentId = student.Id,
                        SubjectId = subject.Id,
                        Score = (decimal)(random.Next(8, 20) + random.NextDouble()),
                        DateRecorded = DateTime.Now.AddDays(-random.Next(1, 90)),
                        Comments = random.Next(2) == 0 ? "Good progress" : "Needs improvement"
                    });
                }
            }
            context.Grades.AddRange(grades);
            context.SaveChanges();
        }
    }
}
