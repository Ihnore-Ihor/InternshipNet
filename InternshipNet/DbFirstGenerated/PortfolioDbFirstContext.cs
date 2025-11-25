using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InternshipNet.DbFirstGenerated;

public partial class PortfolioDbFirstContext : DbContext
{
    public PortfolioDbFirstContext()
    {
    }

    public PortfolioDbFirstContext(DbContextOptions<PortfolioDbFirstContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Internship> Internships { get; set; }

    public virtual DbSet<Resume> Resumes { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentApplication> StudentApplications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=InternshipNetDb;Username=postgres;Password=p");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Internship>(entity =>
        {
            entity.HasIndex(e => e.CompanyId, "IX_Internships_CompanyId");

            entity.Property(e => e.IsRemote).HasDefaultValue(false);

            entity.HasOne(d => d.Company).WithMany(p => p.Internships).HasForeignKey(d => d.CompanyId);
        });

        modelBuilder.Entity<Resume>(entity =>
        {
            entity.HasIndex(e => e.StudentId, "IX_Resumes_StudentId").IsUnique();

            entity.HasOne(d => d.Student).WithOne(p => p.Resume).HasForeignKey<Resume>(d => d.StudentId);
        });

        modelBuilder.Entity<StudentApplication>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.InternshipId });

            entity.HasIndex(e => e.InternshipId, "IX_StudentApplications_InternshipId");

            entity.HasOne(d => d.Internship).WithMany(p => p.StudentApplications).HasForeignKey(d => d.InternshipId);

            entity.HasOne(d => d.Student).WithMany(p => p.StudentApplications).HasForeignKey(d => d.StudentId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
