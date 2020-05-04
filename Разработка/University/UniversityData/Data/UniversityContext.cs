using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using UniversityData.Models;

namespace UniversityData
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

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<GroupStudentV> GroupStudents { get; set; }
        public virtual DbSet<GroupSubjectLink> GroupSubjectLink { get; set; }
        public virtual DbSet<Institute> Institute { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffDepartmentLink> StaffDepartmentLink { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentGroup> StudentGroup { get; set; }
        public virtual DbSet<StudentRequisite> StudentRequisite { get; set; }
        public virtual DbSet<SubSubject> SubSubject { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<GroupSubjectV> GroupSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.HasIndex(e => e.DirectorId)
                    .HasName("department_director_id_key")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("department_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DirectorId).HasColumnName("director_id");

                entity.Property(e => e.InstituteId).HasColumnName("institute_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Shortname)
                    .HasColumnName("shortname")
                    .HasMaxLength(25);

                entity.HasOne(d => d.Director)
                    .WithOne(p => p.Department)
                    .HasForeignKey<Department>(d => d.DirectorId)
                    .HasConstraintName("department_director_id_fkey");

                entity.HasOne(d => d.Institute)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.InstituteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("department_institute_id_fkey");
            });

            modelBuilder.Entity<GroupStudentV>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("group_student_v");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.IsHeadOfGroup).HasColumnName("is_head_of_group");

                entity.Property(e => e.SerialNumber).HasColumnName("serial_number");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.StudentName).HasColumnName("student_name");
            });

            modelBuilder.Entity<GroupSubjectLink>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.SubjectId })
                    .HasName("gs_pk");

                entity.ToTable("group_subject_link");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.Property(e => e.SemesterNumber)
                    .HasColumnName("semester_number")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupSubjectLink)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("group_subject_link_group_id_fkey");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.GroupSubjectLink)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("group_subject_link_subject_id_fkey");
            });

            modelBuilder.Entity<Institute>(entity =>
            {
                entity.ToTable("institute");

                entity.HasIndex(e => e.DirectorDeputyId)
                    .HasName("institute_director_deputy_id_key")
                    .IsUnique();

                entity.HasIndex(e => e.DirectorId)
                    .HasName("institute_director_id_key")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("institute_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DirectorDeputyId).HasColumnName("director_deputy_id");

                entity.Property(e => e.DirectorId).HasColumnName("director_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Shortname)
                    .HasColumnName("shortname")
                    .HasMaxLength(25);

                entity.HasOne(d => d.DirectorDeputy)
                    .WithOne(p => p.InstituteDirectorDeputy)
                    .HasForeignKey<Institute>(d => d.DirectorDeputyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("institute_director_deputy_id_fkey");

                entity.HasOne(d => d.Director)
                    .WithOne(p => p.InstituteDirector)
                    .HasForeignKey<Institute>(d => d.DirectorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("institute_director_id_fkey");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("staff");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Patronymic)
                    .HasColumnName("patronymic")
                    .HasMaxLength(50);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StaffDepartmentLink>(entity =>
            {
                entity.HasKey(e => new { e.DepartmentId, e.StaffId })
                    .HasName("sd_pk");

                entity.ToTable("staff_department_link");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.StaffDepartmentLink)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("staff_department_link_department_id_fkey");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffDepartmentLink)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("staff_department_link_staff_id_fkey");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.HasIndex(e => e.StudentNumber)
                    .HasName("student_student_number_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Patronymic)
                    .HasColumnName("patronymic")
                    .HasMaxLength(50);

                entity.Property(e => e.StudentNumber)
                    .IsRequired()
                    .HasColumnName("student_number")
                    .HasMaxLength(50);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StudentGroup>(entity =>
            {
                entity.ToTable("student_group");

                entity.HasIndex(e => e.Name)
                    .HasName("student_group_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.HeadId).HasColumnName("head_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(25);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.StudentGroup)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_group_department_id_fkey");

                entity.HasOne(d => d.Head)
                    .WithMany(p => p.StudentGroup)
                    .HasForeignKey(d => d.HeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_group_head_id_fkey");
            });

            modelBuilder.Entity<StudentRequisite>(entity =>
            {
                entity.HasKey(e => e.StudentId)
                    .HasName("student_requisite_pkey");

                entity.ToTable("student_requisite");

                entity.Property(e => e.StudentId)
                    .HasColumnName("student_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EMail)
                    .HasColumnName("e_mail")
                    .HasMaxLength(50);

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.HomePhoneNumber)
                    .HasColumnName("home_phone_number")
                    .HasMaxLength(50);

                entity.Property(e => e.MobilePhoneNumber)
                    .HasColumnName("mobile_phone_number")
                    .HasMaxLength(50);

                entity.Property(e => e.PhotoLink).HasColumnName("photo_link");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.StudentRequisite)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_requisite_group_id_fkey");

                entity.HasOne(d => d.Student)
                    .WithOne(p => p.StudentRequisite)
                    .HasForeignKey<StudentRequisite>(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_requisite_student_id_fkey");
            });

            modelBuilder.Entity<SubSubject>(entity =>
            {
                entity.ToTable("sub_subject");

                entity.HasIndex(e => e.Name)
                    .HasName("sub_subject_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Shortname)
                    .HasColumnName("shortname")
                    .HasMaxLength(25);

                entity.Property(e => e.SubLecturerId).HasColumnName("sub_lecturer_id");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.HasOne(d => d.SubLecturer)
                    .WithMany(p => p.SubSubject)
                    .HasForeignKey(d => d.SubLecturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sub_subject_sub_lecturer_id_fkey");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubSubject)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sub_subject_subject_id_fkey");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subject");

                entity.HasIndex(e => e.Name)
                    .HasName("subject_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LecturerId).HasColumnName("lecturer_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Shortname)
                    .HasColumnName("shortname")
                    .HasMaxLength(25);

                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.Subject)
                    .HasForeignKey(d => d.LecturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subject_lecturer_id_fkey");
            });
            
            modelBuilder.Entity<GroupSubjectV>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("group_subject_v");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.Semester).HasColumnName("semester");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.Property(e => e.SubjectName)
                    .HasColumnName("subject_name")
                    .HasMaxLength(25);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
