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
        // Конструктор (залишаємо порожнім або для ін'єкцій)
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Таблиці бази даних
        public DbSet<Company> Companies { get; set; }
        public DbSet<Internship> Internships { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<StudentApplication> StudentApplications { get; set; }

        // Налаштування підключення
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseNpgsql("Host=localhost;Port=5432;Database=InternshipNetDb;Username=postgres;Password=p")
                    // ЗМІНИЛИ ТУТ: Використовуємо Debug.WriteLine замість Console.WriteLine
                    .LogTo(message => System.Diagnostics.Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information)
                    .EnableSensitiveDataLogging();
            }
        }

        // Налаштування моделі (Fluent API)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- 1. Composite Key (Вимога лабораторної) ---
            // Ключ таблиці заявок складається з ID студента та ID стажування
            modelBuilder.Entity<StudentApplication>()
                .HasKey(sa => new { sa.StudentId, sa.InternshipId });

            // --- 2. Налаштування зв'язків ---

            // 1-to-N: Компанія має багато стажувань
            modelBuilder.Entity<Internship>()
                .HasOne(i => i.Company)
                .WithMany(c => c.Internships)
                .HasForeignKey(i => i.CompanyId);

            // 1-to-1: Студент має одне резюме
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Resume)
                .WithOne(r => r.Student)
                .HasForeignKey<Resume>(r => r.StudentId);

            // N-to-M: Зв'язок через проміжну таблицю StudentApplication
            modelBuilder.Entity<StudentApplication>()
                .HasOne(sa => sa.Student)
                .WithMany(s => s.Applications)
                .HasForeignKey(sa => sa.StudentId);

            modelBuilder.Entity<StudentApplication>()
                .HasOne(sa => sa.Internship)
                .WithMany(i => i.Applications)
                .HasForeignKey(sa => sa.InternshipId);

            // --- 3. Seed Data (Вимога: використання enum у seed) ---

            // Додаємо компанії
            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Microsoft", Industry = "Technology" },
                new Company { Id = 2, Name = "Google", Industry = "Search & AI" }
            );

            // Додаємо стажування
            modelBuilder.Entity<Internship>().HasData(
                new Internship { Id = 1, Title = ".NET Developer Intern", Description = "C# and WPF tasks", CompanyId = 1 },
                new Internship { Id = 2, Title = "Android Intern", Description = "Kotlin and Java", CompanyId = 2 }
            );

            // Додаємо студента
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Ihor Braichenko", Email = "ihor@test.com" }
            );

            // Додаємо резюме
            modelBuilder.Entity<Resume>().HasData(
                new Resume { Id = 1, Content = "My Resume Content...", StudentId = 1 }
            );

            // Додаємо заявку (тут використовуємо Enum ApplicationStatus)
            modelBuilder.Entity<StudentApplication>().HasData(
                new StudentApplication
                {
                    StudentId = 1,
                    InternshipId = 1,
                    Status = ApplicationStatus.Pending, // Використання Enum
                    AppliedDate = DateTime.UtcNow
                }
            );
        }
    }
}
