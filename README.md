# 🎓 School Management System

A comprehensive web-based School Management System built with ASP.NET Core MVC, Entity Framework Core, and DevExpress components. This system provides complete management of students, teachers, classrooms, courses, grades, and enrollments with an interactive analytics dashboard.

## 📋 Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Architecture](#architecture)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Database Setup](#database-setup)
- [Running the Application](#running-the-application)
- [Default Credentials](#default-credentials)
- [Project Structure](#project-structure)
- [Key Features Explained](#key-features-explained)
- [Screenshots](#screenshots)
- [Deployment](#deployment)
- [Contributing](#contributing)
- [License](#license)

## ✨ Features

### Core Modules
- **👥 Student Management**: Complete CRUD operations for student records with parent associations
- **👨‍🏫 Teacher Management**: Manage teacher profiles with specializations and hire dates
- **🏫 Classroom Management**: Organize classrooms with capacity and grade levels
- **📚 Subject Management**: Define subjects with credits and descriptions
- **📅 Course Management**: Create and manage courses (timetables) linking subjects, teachers, and classrooms
- **📝 Enrollment Management**: Track student enrollments in classrooms with academic year and semester
- **📊 Grade Management**: Record and manage student grades (scored out of 20)
- **👪 Parent Management**: Maintain parent information and link to students

### Analytics Dashboard
- **📈 Interactive Charts**: Powered by DevExpress for professional data visualization
- **Classroom Average Grades**: Bar chart showing performance by classroom
- **Yearly Enrollment Trends**: Track enrollment patterns over the last 5 years
- **Teacher Workload Analysis**: Visualize course and student distribution per teacher
- **Statistics Cards**: Real-time counts of students, teachers, classrooms, subjects, courses, parents, and average grades
- **Classroom Statistics Grid**: Sortable, filterable data grid with export functionality

### Additional Features
- **🔐 Authentication & Authorization**: Session-based authentication with role management
- **🔍 Search Functionality**: Search bars on Students, Enrollments, and Teachers lists
- **📱 Responsive Design**: Mobile-friendly interface with collapsible sidebar
- **🎨 Modern UI**: Clean, professional interface with Bootstrap and custom CSS
- **🗄️ Data Seeding**: Automatic population of sample data for testing

## 🛠️ Technologies Used

### Backend
- **ASP.NET Core 8.0 MVC**: Web framework
- **Entity Framework Core 9.0**: ORM for database operations
- **SQL Server**: Database management system
- **C# 12**: Programming language
- **LINQ**: Data querying

### Frontend
- **Razor Views**: Server-side rendering
- **Bootstrap 5**: CSS framework
- **Bootstrap Icons**: Icon library
- **jQuery**: JavaScript library
- **DevExpress 20.1.3**: Professional charts and data grids
- **Custom CSS**: Tailored styling

### Architecture Pattern
- **Layered Architecture**: Clean separation of concerns across multiple assemblies
- **Repository Pattern**: Data access abstraction
- **Service Layer**: Business logic separation
- **Dependency Injection**: Loose coupling and testability
- **MVC Pattern**: Presentation layer organization
- **Multi-Project Solution**: Modular assembly structure for maintainability

## 📦 Prerequisites

Before running this project, ensure you have:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (Express or Developer Edition)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms) (optional)



### Seed Sample Data

The application automatically seeds sample data on first run, including:
- 50 students with realistic names
- 6 teachers with specializations
- 8 subjects (Math, Physics, French, English, History, Biology, Chemistry, Arabic)
- 6 classrooms (1A, 1B, 2A, 2B, 3A, 3B)
- 5 parents
- 50 enrollments
- 36 courses
- 300 grades

After seeding, use these credentials to log in:

- **Username**: `chaima`
- **Password**: `password123`
- **Role**: Admin

## 🏗️ Layered Architecture

This project follows a **clean layered architecture** with **4 separate assemblies** for better separation of concerns, maintainability, and testability:

### Assembly Structure

```
Solution: SchoolManagement.sln
│
├── School.Core (Class Library)           # Domain Layer
├── School.Data (Class Library)           # Data Access Layer
├── School.Services (Class Library)       # Business Logic Layer
└── SchoolManagement (Web Application)    # Presentation Layer
```

### Layer Dependencies

```
SchoolManagement (Web)
    ↓ depends on
School.Services (Business Logic)
    ↓ depends on
School.Data (Data Access)
    ↓ depends on
School.Core (Domain Models)
```

**Benefits of this architecture:**
- ✅ **Separation of Concerns**: Each layer has a specific responsibility
- ✅ **Testability**: Layers can be tested independently
- ✅ **Maintainability**: Changes in one layer don't affect others
- ✅ **Reusability**: Core and Service layers can be reused in other projects
- ✅ **Scalability**: Easy to add new features without breaking existing code

## 📁 Project Structure

```
SchoolManagement/
├── School.Core/                    # 🎯 Domain Layer (Assembly 1)
│   ├── Models/                     # Entity models
│   │   ├── Student.cs
│   │   ├── Teacher.cs
│   │   ├── Classroom.cs
│   │   ├── Subject.cs
│   │   ├── Course.cs
│   │   ├── Enrollment.cs
│   │   ├── Grade.cs
│   │   ├── Parent.cs
│   │   ├── User.cs
│   │   └── Role.cs
│   └── Interfaces/                 # Service and repository interfaces
│       ├── IStudentService.cs
│       ├── ITeacherService.cs
│       ├── IAnalyticsService.cs
│       ├── IRepository.cs
│       └── ...
│
├── School.Data/                    # 💾 Data Access Layer (Assembly 2)
│   ├── SchoolContext.cs           # EF Core DbContext
│   ├── DataSeeder.cs              # Sample data seeder
│   └── Repositories/              # Repository implementations
│       ├── StudentRepository.cs
│       ├── TeacherRepository.cs
│       ├── ClassroomRepository.cs
│       └── Repository.cs          # Generic repository
│
├── School.Services/                # 🔧 Business Logic Layer (Assembly 3)
│   └── Services/
│       ├── StudentService.cs
│       ├── TeacherService.cs
│       ├── AnalyticsService.cs
│       ├── GradeService.cs
│       ├── EnrollmentService.cs
│       ├── ParentService.cs
│       └── ...
│
└── SchoolManagement/               # 🌐 Presentation Layer (Assembly 4 - Web MVC)
    ├── Controllers/               # MVC Controllers
    │   ├── DashboardController.cs
    │   ├── StudentController.cs
    │   ├── TeacherController.cs
    │   └── ...
    ├── Views/                     # Razor views
    │   ├── Dashboard/
    │   ├── Students/
    │   ├── Teachers/
    │   ├── Shared/
    │   └── ...
    ├── wwwroot/                   # Static files
    │   ├── css/
    │   ├── js/
    │   └── lib/
    ├── Filters/                   # Custom filters
    │   └── AuthorizeSessionAttribute.cs
    ├── Migrations/                # EF Core migrations
    ├── appsettings.json          # Configuration
    └── Program.cs                # Application entry point
```

## �️ Layered rArchitecture Details

### Layer 1: Domain Layer (School.Core)
**Purpose**: Contains domain models and interfaces (contracts)

**Responsibilities:**
- Define entity models (Student, Teacher, Course, etc.)
- Define service interfaces (IStudentService, ITeacherService, etc.)
- Define repository interfaces (IRepository<T>, IStudentRepository, etc.)
- No dependencies on other layers

**Key Principle**: This layer is the foundation and has **zero dependencies** on other projects.

### Layer 2: Data Access Layer (School.Data)
**Purpose**: Handles all database operations using Entity Framework Core

**Responsibilities:**
- DbContext configuration (SchoolContext)
- Repository implementations
- Database migrations
- Data seeding
- Entity configurations and relationships

**Dependencies**: References `School.Core` only

### Layer 3: Business Logic Layer (School.Services)
**Purpose**: Contains all business logic and rules

**Responsibilities:**
- Service implementations (StudentService, GradeService, etc.)
- Business rule validation
- Data transformation
- Complex queries and calculations
- Analytics and reporting logic

**Dependencies**: References `School.Core` and `School.Data`

### Layer 4: Presentation Layer (SchoolManagement)
**Purpose**: ASP.NET Core MVC web application

**Responsibilities:**
- Controllers (handle HTTP requests)
- Views (Razor pages for UI)
- ViewModels and DTOs
- Authentication and authorization
- Static files (CSS, JS, images)
- Dependency injection configuration

**Dependencies**: References all other layers (`School.Core`, `School.Data`, `School.Services`)

## 🎯 Key Features Explained

### 1. Analytics Dashboard with DevExpress

The dashboard uses DevExpress components for professional data visualization:

```javascript
// Example: Classroom Average Grades Chart
$("#subjectPerformanceChart").dxChart({
    dataSource: data,
    size: { height: 350 },
    commonSeriesSettings: {
        argumentField: "classroomName",
        type: "bar"
    },
    series: [
        { valueField: "averageGrade", name: "Average Grade" }
    ]
});
```

**Features:**
- Interactive bar and line charts
- Responsive data grids with filtering, sorting, and export
- Real-time statistics cards
- Customizable tooltips and legends

### 2. Layered Architecture Implementation

**Domain Layer (School.Core):**
```csharp
// Interface definition
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
```

**Data Access Layer (School.Data):**
```csharp
// Repository implementation
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly SchoolContext _context;
    
    public Repository(SchoolContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
}
```

**Business Logic Layer (School.Services):**
```csharp
// Service implementation
public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;
    
    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _repository.GetAllAsync();
    }
}
```

**Presentation Layer (SchoolManagement):**
```csharp
// Controller using service
public class StudentController : Controller
{
    private readonly IStudentService _studentService;
    
    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    public async Task<IActionResult> Index()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return View(students);
    }
}
```

### 3. Assembly References and Dependencies

**Dependency Flow:**
```
SchoolManagement.csproj
    → School.Services.csproj
        → School.Data.csproj
            → School.Core.csproj (No dependencies)
```

**Benefits:**
- **Loose Coupling**: Layers communicate through interfaces
- **Testability**: Each layer can be unit tested independently
- **Maintainability**: Changes in one layer don't cascade to others
- **Reusability**: Core and Services can be used in other applications (e.g., API, Console app)

### 4. Grade System

Grades are scored out of 20 (French education system):
- Pass mark: 10/20
- Excellent: 16+/20
- Good: 14-16/20
- Average: 10-14/20
- Below average: <10/20

### 5. Relationship Management

- **Students ↔ Parents**: One-to-Many (nullable)
- **Students ↔ Enrollments**: One-to-Many (cascade delete)
- **Students ↔ Grades**: One-to-Many (cascade delete)
- **Classrooms ↔ Enrollments**: One-to-Many (cascade delete)
- **Courses ↔ Teachers**: Many-to-One (set null on delete)
- **Courses ↔ Classrooms**: Many-to-One (set null on delete)
- **Grades ↔ Subjects**: Many-to-One (restrict delete)

