using GestionFlotte.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotte.Data
{
    public class FlotteContext : DbContext
    {
        public FlotteContext(DbContextOptions<FlotteContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Maitrise> Maitrises { get; set; }
        public DbSet<Marin> Marins { get; set; }
        public DbSet<TypeBateau> TypesBateaux { get; set; }
        public DbSet<Poste> Postes { get; set; }
        public DbSet<Bateau> Bateaux { get; set; }
        public DbSet<RoleAssignment> RoleAssignments { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Maitrise>().ToTable("Maitrise");
            modelBuilder.Entity<Marin>().ToTable("Marin");
            modelBuilder.Entity<TypeBateau>().ToTable("TypeBateau");
            modelBuilder.Entity<Poste>().ToTable("Poste");
            modelBuilder.Entity<Bateau>().ToTable("Bateau");

            modelBuilder.Entity<RoleAssignment>().ToTable("RoleAssignment");
            modelBuilder.Entity<RoleAssignment>()
                .HasKey(c => new { c.RoleID, c.MarinID });

            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");

            modelBuilder.Entity<CourseAssignment>()
                .HasKey(c => new { c.CourseID, c.InstructorID });
        }

    }
}
