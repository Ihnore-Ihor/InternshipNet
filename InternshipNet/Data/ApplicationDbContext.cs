using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using InternshipNet.Models;
using System;

namespace InternshipNet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Database tables
        public DbSet<Company> Companies { get; set; }
        public DbSet<Internship> Internships { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<StudentApplication> StudentApplications { get; set; }

        // Connection configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseNpgsql("Host=localhost;Port=5432;Database=InternshipNetDb;Username=postgres;Password=p")
                    .LogTo(message => System.Diagnostics.Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information)
                    .EnableSensitiveDataLogging();
            }
        }

        // Model configuration (Fluent API)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- 1. Composite Key (Lab requirement) ---
            // The primary key for StudentApplication consists of StudentId and InternshipId
            modelBuilder.Entity<StudentApplication>()
                .HasKey(sa => new { sa.StudentId, sa.InternshipId });

            // --- 2. Relationship configuration ---

            // 1-to-Many: A company has many internships
            modelBuilder.Entity<Internship>()
                .HasOne(i => i.Company)
                .WithMany(c => c.Internships)
                .HasForeignKey(i => i.CompanyId);

            // 1-to-1: A student has one resume
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Resume)
                .WithOne(r => r.Student)
                .HasForeignKey<Resume>(r => r.StudentId);

            // Many-to-Many: Relationship via the StudentApplication join table
            modelBuilder.Entity<StudentApplication>()
                .HasOne(sa => sa.Student)
                .WithMany(s => s.Applications)
                .HasForeignKey(sa => sa.StudentId);

            modelBuilder.Entity<StudentApplication>()
                .HasOne(sa => sa.Internship)
                .WithMany(i => i.Applications)
                .HasForeignKey(sa => sa.InternshipId);

            // --- 3. Seed Data (Requirement: use enum in seed) ---

            // Add companies
            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Microsoft", Industry = "Technology" },
                new Company { Id = 2, Name = "Google", Industry = "Search & AI" }
            );

            // Add internships
            modelBuilder.Entity<Internship>().HasData(
                new Internship { Id = 1, Title = ".NET Developer Intern", Description = "C# and WPF tasks", CompanyId = 1 },
                new Internship { Id = 2, Title = "Android Intern", Description = "Kotlin and Java", CompanyId = 2 }
            );

            // Add student
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Ihor Braichenko", Email = "ihor@test.com" }
            );

            // Add resume
            modelBuilder.Entity<Resume>().HasData(
                new Resume { Id = 1, Content = "My Resume Content...", StudentId = 1 }
            );

            // Add application (using ApplicationStatus enum)
            modelBuilder.Entity<StudentApplication>().HasData(
                new StudentApplication
                {
                    StudentId = 1,
                    InternshipId = 1,
                    Status = ApplicationStatus.Pending, 
                    AppliedDate = DateTime.UtcNow
                }
            );
        }
    }
}
