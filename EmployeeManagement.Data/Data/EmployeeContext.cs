using System;
using System.Collections.Generic;
using EmployeeManagement.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace EmployeeManagement.Data.Data;

public partial class EmployeeContext : DbContext
{
    public EmployeeContext()
    {
    }

    public EmployeeContext(DbContextOptions<EmployeeContext> options)
        : base(options)
    {
    }

    //public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
=> optionsBuilder.UseSqlServer("Server=Kratim-win11;Database=Company;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Department>(entity =>
        //{
        //    entity.ToTable("Department");

        //    entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
        //    entity.Property(e => e.DepartmentName)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //});

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            //entity.Property(e => e.Department)
            //    .HasMaxLength(100)
            //    .IsUnicode(false);
			entity.Property(e => e.Doj)
                .HasColumnType("date")
                .HasColumnName("DOJ");
            entity.Property(e => e.EmailId).HasMaxLength(500);
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(500)
                .IsUnicode(false);
			entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
		});

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
