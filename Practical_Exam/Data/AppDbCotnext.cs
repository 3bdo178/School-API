using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Practical_Exam.Models;

namespace Practical_Exam.Data
{
    public class AppDbCotnext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacherdto> Teachers { get; set; }
        public DbSet<ClassRoom> classRooms { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=true;Application Intent=ReadWrite;Multi Subnet Failover=False");
            base.OnConfiguring(optionsBuilder);
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacherdto>().HasOne(t => t.Department).WithMany(d => d.Teachers).HasForeignKey(t => t.DepartmentId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Teacherdto>().HasMany(t => t.Subjects).WithOne(s => s.Teacher).HasForeignKey(s => s.TeacherId);
            modelBuilder.Entity<Subject>().HasMany(s => s.Enrollments).WithOne(e => e.Subject).HasForeignKey(e => e.SubjectId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Enrollment>().HasOne(e=>e.Student).WithMany(s=>s.Enrollments).HasForeignKey(e => e.StudentId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Student>().HasOne(s => s.Classroom).WithMany(c => c.Students).HasForeignKey(s => s.ClassroomId);
            modelBuilder.Entity<Teacherdto>().HasIndex(t=>t.Email).IsUnique();
            modelBuilder.Entity<Student>().HasIndex(s => s.Email).IsUnique();
            modelBuilder.Entity<Enrollment>().Property(e => e.Grade).HasPrecision(18, 2);
            modelBuilder.Entity<Department>().HasIndex(d => d.Name).IsUnique();
            modelBuilder.Entity<Enrollment>().HasIndex(e=> new {e.StudentId, e.SubjectId}).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}
