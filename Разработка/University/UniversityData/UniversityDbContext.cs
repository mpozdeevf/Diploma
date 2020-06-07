using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using UniversityData.Entities;

namespace UniversityData
{
    public partial class UniversityDbContext : DbContext
    {
        public UniversityDbContext()
        {
        }

        public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupSubject> GroupsSubjects { get; set; }
        public virtual DbSet<Institute> Institutes { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsReceiver> NewsReceivers { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffAuthData> StaffAuthData { get; set; }
        public virtual DbSet<StaffDepartment> StaffDepartments { get; set; }
        public virtual DbSet<StaffRequisite> StaffRequisites { get; set; }
        public virtual DbSet<StudentRequisites> StudentRequisites { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentAuthData> StudentsAuthData { get; set; }
        public virtual DbSet<SubSubject> SubSubjects { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectInformation> SubjectsInformation { get; set; }

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                 optionsBuilder.UseNpgsql("Host=192.168.56.101;Port=5432;Username=postgres;Password=qweASD#21;Database=university1;");
//             }
//         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("classes");

                entity.HasIndex(e => e.EndTime)
                    .HasName("classes_end_time_key")
                    .IsUnique();

                entity.HasIndex(e => e.Number)
                    .HasName("classes_number_key")
                    .IsUnique();

                entity.HasIndex(e => e.StartTime)
                    .HasName("classes_start_time_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndTime)
                    .IsRequired()
                    .HasColumnName("end_time")
                    .HasMaxLength(25);

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.StartTime)
                    .IsRequired()
                    .HasColumnName("start_time")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("departments");

                entity.HasIndex(e => e.DirectorId)
                    .HasName("departments_director_id_key")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("departments_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DirectorId).HasColumnName("director_id");

                entity.Property(e => e.InstituteId).HasColumnName("institute_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.ShortName)
                    .HasColumnName("short_name")
                    .HasMaxLength(25);

                entity.HasOne(d => d.Director)
                    .WithOne(p => p.Departments)
                    .HasForeignKey<Department>(d => d.DirectorId)
                    .HasConstraintName("departments_director_id_fkey");

                entity.HasOne(d => d.Institute)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.InstituteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("departments_institute_id_fkey");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("groups");

                entity.HasIndex(e => e.Name)
                    .HasName("groups_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.HeadId).HasColumnName("head_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(25);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("groups_department_id_fkey");

                entity.HasOne(d => d.Head)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.HeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("groups_head_id_fkey");
            });

            modelBuilder.Entity<GroupSubject>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.SubjectId })
                    .HasName("gs_pk");

                entity.ToTable("groups_subjects");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.Property(e => e.Semester)
                    .HasColumnName("semester")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupsSubjects)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("groups_subjects_group_id_fkey");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.GroupsSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("groups_subjects_subject_id_fkey");
            });

            modelBuilder.Entity<Institute>(entity =>
            {
                entity.ToTable("institutes");

                entity.HasIndex(e => e.DirectorDeputyId)
                    .HasName("institutes_director_deputy_id_key")
                    .IsUnique();

                entity.HasIndex(e => e.DirectorId)
                    .HasName("institutes_director_id_key")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("institutes_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DirectorDeputyId).HasColumnName("director_deputy_id");

                entity.Property(e => e.DirectorId).HasColumnName("director_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.ShortName)
                    .HasColumnName("short_name")
                    .HasMaxLength(25);

                entity.HasOne(d => d.DirectorDeputy)
                    .WithOne(p => p.InstitutesDirectorDeputy)
                    .HasForeignKey<Institute>(d => d.DirectorDeputyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("institutes_director_deputy_id_fkey");

                entity.HasOne(d => d.Director)
                    .WithOne(p => p.InstitutesDirector)
                    .HasForeignKey<Institute>(d => d.DirectorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("institutes_director_id_fkey");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.ToTable("news");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthorStaffId).HasColumnName("author_staff_id");

                entity.Property(e => e.AuthorStudentId).HasColumnName("author_student_id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(300);

                entity.HasOne(d => d.AuthorStaff)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.AuthorStaffId)
                    .HasConstraintName("news_author_staff_id_fkey");

                entity.HasOne(d => d.AuthorStudent)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.AuthorStudentId)
                    .HasConstraintName("news_author_student_id_fkey");
            });

            modelBuilder.Entity<NewsReceiver>(entity =>
            {
                entity.ToTable("news_receivers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.InstituteId).HasColumnName("institute_id");

                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.NewsReceivers)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("news_receivers_department_id_fkey");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.NewsReceivers)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("news_receivers_group_id_fkey");

                entity.HasOne(d => d.Institute)
                    .WithMany(p => p.NewsReceivers)
                    .HasForeignKey(d => d.InstituteId)
                    .HasConstraintName("news_receivers_institute_id_fkey");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsReceivers)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_receivers_news_id_fkey");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.NewsReceivers)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("news_receivers_staff_id_fkey");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("schedule");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasMaxLength(25);

                entity.Property(e => e.SubSubjectId).HasColumnName("sub_subject_id");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.Property(e => e.WeekLine).HasColumnName("week_line");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("schedule_class_id_fkey");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("schedule_group_id_fkey");

                entity.HasOne(d => d.SubSubject)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.SubSubjectId)
                    .HasConstraintName("schedule_sub_subject_id_fkey");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("schedule_subject_id_fkey");
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

            modelBuilder.Entity<StaffAuthData>(entity =>
            {
                entity.HasKey(e => e.StaffId)
                    .HasName("staff_auth_data_pkey");

                entity.ToTable("staff_auth_data");

                entity.HasIndex(e => e.Username)
                    .HasName("staff_auth_data_username_key")
                    .IsUnique();

                entity.Property(e => e.StaffId)
                    .HasColumnName("staff_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100);

                entity.Property(e => e.RefreshToken)
                    .HasColumnName("refresh_token")
                    .HasMaxLength(100);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("salt")
                    .HasMaxLength(100);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(25);

                entity.HasOne(d => d.Staff)
                    .WithOne(p => p.StaffAuthData)
                    .HasForeignKey<StaffAuthData>(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("staff_auth_data_staff_id_fkey");
            });

            modelBuilder.Entity<StaffDepartment>(entity =>
            {
                entity.HasKey(e => new { e.DepartmentId, e.StaffId })
                    .HasName("sd_pk");

                entity.ToTable("staff_departments");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.StaffDepartments)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("staff_departments_department_id_fkey");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffDepartments)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("staff_departments_staff_id_fkey");
            });

            modelBuilder.Entity<StaffRequisite>(entity =>
            {
                entity.HasKey(e => e.StaffId)
                    .HasName("staff_requisites_pkey");

                entity.ToTable("staff_requisites");

                entity.Property(e => e.StaffId)
                    .HasColumnName("staff_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EMail)
                    .HasColumnName("e_mail")
                    .HasMaxLength(50);

                entity.Property(e => e.HomePhone)
                    .HasColumnName("home_phone")
                    .HasMaxLength(50);

                entity.Property(e => e.MobilePhone)
                    .HasColumnName("mobile_phone")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Staff)
                    .WithOne(p => p.StaffRequisites)
                    .HasForeignKey<StaffRequisite>(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("staff_requisites_staff_id_fkey");
            });

            modelBuilder.Entity<StudentRequisites>(entity =>
            {
                entity.HasKey(e => e.StudentId)
                    .HasName("student_requisites_pkey");

                entity.ToTable("student_requisites");

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

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.StudentRequisites)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_requisites_group_id_fkey");

                entity.HasOne(d => d.Student)
                    .WithOne(p => p.StudentRequisites)
                    .HasForeignKey<StudentRequisites>(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_requisites_student_id_fkey");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("students");

                entity.HasIndex(e => e.StudentNumber)
                    .HasName("students_student_number_key")
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

            modelBuilder.Entity<StudentAuthData>(entity =>
            {
                entity.HasKey(e => e.StudentId)
                    .HasName("students_auth_data_pkey");

                entity.ToTable("students_auth_data");

                entity.HasIndex(e => e.Username)
                    .HasName("students_auth_data_username_key")
                    .IsUnique();

                entity.Property(e => e.StudentId)
                    .HasColumnName("student_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100);

                entity.Property(e => e.RefreshToken)
                    .HasColumnName("refresh_token")
                    .HasMaxLength(100);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("salt")
                    .HasMaxLength(100);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(25);

                entity.HasOne(d => d.Student)
                    .WithOne(p => p.StudentsAuthData)
                    .HasForeignKey<StudentAuthData>(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("students_auth_data_student_id_fkey");
            });

            modelBuilder.Entity<SubSubject>(entity =>
            {
                entity.ToTable("sub_subjects");

                entity.HasIndex(e => e.Name)
                    .HasName("sub_subjects_name_key")
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
                    .WithMany(p => p.SubSubjects)
                    .HasForeignKey(d => d.SubLecturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sub_subjects_sub_lecturer_id_fkey");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sub_subjects_subject_id_fkey");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subjects");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.InformationId).HasColumnName("information_id");

                entity.Property(e => e.LecturerId).HasColumnName("lecturer_id");

                entity.HasOne(d => d.Information)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.InformationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subjects_information_id_fkey");

                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.LecturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subjects_lecturer_id_fkey");
            });

            modelBuilder.Entity<SubjectInformation>(entity =>
            {
                entity.ToTable("subjects_information");

                entity.HasIndex(e => e.Name)
                    .HasName("subjects_information_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.ShortName)
                    .HasColumnName("short_name")
                    .HasMaxLength(25);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
