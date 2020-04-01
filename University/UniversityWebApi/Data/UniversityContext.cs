﻿using Microsoft.EntityFrameworkCore;
using UniversityWebApi.Models;

namespace UniversityWebApi.Data
{
    public partial class UniversityContext : DbContext
    {
        public UniversityContext()
        {
        }

        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Institutes> Institutes { get; set; }
        public virtual DbSet<LecturersDepartments> LecturersDepartments { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Students> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=192.168.56.101;Port=5432;Username=postgres;Password=qweASD#21;Database=university;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departments>(entity =>
            {
                entity.ToTable("departments");

                entity.HasIndex(e => e.Name)
                    .HasName("departments_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HeadId).HasColumnName("head_id");

                entity.Property(e => e.InstituteId).HasColumnName("institute_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.HasOne(d => d.Head)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.HeadId)
                    .HasConstraintName("departments_head_id_fkey");

                entity.HasOne(d => d.Institute)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.InstituteId)
                    .HasConstraintName("departments_institute_id_fkey");
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.ToTable("groups");

                entity.HasIndex(e => e.Name)
                    .HasName("groups_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("groups_department_id_fkey");
            });

            modelBuilder.Entity<Institutes>(entity =>
            {
                entity.ToTable("institutes");

                entity.HasIndex(e => e.Name)
                    .HasName("institutes_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DirectorDeputyId).HasColumnName("director_deputy_id");

                entity.Property(e => e.DirectorId).HasColumnName("director_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.HasOne(d => d.DirectorDeputy)
                    .WithMany(p => p.InstitutesDirectorDeputy)
                    .HasForeignKey(d => d.DirectorDeputyId)
                    .HasConstraintName("institutes_director_deputy_id_fkey");

                entity.HasOne(d => d.Director)
                    .WithMany(p => p.InstitutesDirector)
                    .HasForeignKey(d => d.DirectorId)
                    .HasConstraintName("institutes_director_id_fkey");
            });

            modelBuilder.Entity<LecturersDepartments>(entity =>
            {
                entity.HasKey(e => new { e.DepartmentId, e.LecturerId })
                    .HasName("pk");

                entity.ToTable("lecturers_departments");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.LecturerId).HasColumnName("lecturer_id");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.LecturersDepartments)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lecturers_departments_department_id_fkey");

                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.LecturersDepartments)
                    .HasForeignKey(d => d.LecturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lecturers_departments_lecturer_id_fkey");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("staff");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Patronymic).HasColumnName("patronymic");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.StudentNumber)
                    .HasName("students_pkey");

                entity.ToTable("students");

                entity.Property(e => e.StudentNumber).HasColumnName("student_number");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Patronymic).HasColumnName("patronymic");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("students_group_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
