using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityData;
using UniversityData.DTO;
using UniversityData.Entities;

namespace UniversityWebApi.Controllers
{
    [Route("api/university")]
    [Authorize]
    public class UniversityDataController : ControllerBase
    {
        private readonly UniversityDbContext _context;

        public UniversityDataController(UniversityDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("student-requisites")]
        public IActionResult GetUserStudentRequisites(int id)
        {
            var studentRequisite = _context.StudentRequisites.FirstOrDefault(sr => sr.StudentId == id);
            if (studentRequisite == null)
            {
                return NotFound();
            }

            var group = _context.Groups.First(g => g.Id == studentRequisite.GroupId);
            var student = _context.Students.First(s => s.Id == id);

            return Ok(new StudentProfileData
            {
                StudentId = studentRequisite.StudentId,
                FullName = GetFullName(student.Name, student.Surname, student.Patronymic),
                GroupName = group.Name, Mail = studentRequisite.EMail,
                MobilePhone = studentRequisite.MobilePhoneNumber, HomePhone = studentRequisite.HomePhoneNumber
            });
        }

        [HttpGet]
        [Route("students")]
        public ActionResult<IEnumerable<StudentData>> GetStudents(int id)
        {
            var studentRequisite = _context.StudentRequisites.FirstOrDefault(sr => sr.StudentId == id);
            if (studentRequisite == null)
            {
                return NotFound();
            }

            var studentsRequisites = _context.StudentRequisites.Where(sr => sr.GroupId == studentRequisite.GroupId);
            var group = _context.Groups.First(g => g.Id == studentRequisite.GroupId);
            var studentsData = _context.Students
                .Where(s => studentsRequisites.FirstOrDefault(sr => sr.StudentId == s.Id) != null)
                .Select(s => new StudentData
                {
                    FullName = GetFullName(s.Name, s.Surname, s.Patronymic),
                    StudentId = s.Id,
                    IsHead = s.Id == group.HeadId
                });

            return Ok(studentsData);
        }

        [HttpGet]
        [Route("subjects")]
        public ActionResult<IEnumerable<SubjectData>> GetSubjects(int id)
        {
            var studentRequisite = _context.StudentRequisites.FirstOrDefault(sr => sr.StudentId == id);
            if (studentRequisite == null)
            {
                return NotFound();
            }

            var subjectsData = new List<SubjectData>();
            var subjectsIdsSemesters = _context.GroupsSubjects.Where(gs => gs.GroupId == studentRequisite.GroupId)
                .Select(gs => new {gs.SubjectId, gs.Semester});
            foreach (var subjectIdSemester in subjectsIdsSemesters)
            {
                var subSubjectsIds = _context.SubSubjects.Where(ss => ss.SubjectId == subjectIdSemester.SubjectId).Select(ss => ss.Id);
                var subSubjectsData = new List<SubSubjectData>();
                foreach (var subSubjectId in subSubjectsIds)
                {
                    var subSubject = _context.SubSubjects.First(ss => ss.Id == subSubjectId);
                    subSubjectsData.Add(new SubSubjectData
                    {
                        Id = subSubject.Id,
                        LecturerId = subSubject.SubLecturerId,
                        Name = subSubject.Name,
                        ShortName = subSubject.Shortname
                    });
                }
                var subject = _context.Subjects.First(s => s.Id == subjectIdSemester.SubjectId);
                var subjectInfo = _context.SubjectsInformation.First(si => si.Id == subject.InformationId);

                subjectsData.Add(new SubjectData
                {
                    Id = subjectIdSemester.SubjectId,
                    Name = subjectInfo.Name,
                    ShortName = subjectInfo.ShortName,
                    Description = subjectInfo.Description,
                    LecturerId = subject.LecturerId,
                    Semester = subjectIdSemester.Semester,
                    SubSubjectsData = subSubjectsData
                });
            }

            return Ok(subjectsData);
        }

        private static string GetFullName(string name, string surname, string patronymic)
        {
            return (surname + " " + name + " " + patronymic).TrimEnd();
        }

        #region TestData

        [HttpGet]
        [Route("generate-test-data")]
        [AllowAnonymous]
        public IActionResult GenerateTestData()
        {
            try
            {
                GenerateStaff();
                GenerateStaffRequisites();
                GenerateInstitutes();
                GenerateDepartments();
                GenerateStaffDepartments();
                GenerateStudents();
                GenerateGroups();
                GenerateStudentsRequisites();
                GenerateClasses();
                GenerateSubjectsInformation();
                GenerateSubjects();
                GenerateSubSubjects();
                GenerateGroupsSubjects();
            }
            catch (Exception e)
            {
                BadRequest(new ErrorData {ErrorText = e.Message});
            }

            return Ok();
        }

        // private void RemoveData()
        // {
        //     _context.Database.ExecuteSqlRaw(
        //         "TRUNCATE TABLE staff_auth_data;" +
        //         "TRUNCATE TABLE students_auth_data;" +
        //         "TRUNCATE TABLE schedule;" +
        //         "TRUNCATE TABLE classes;" +
        //         "TRUNCATE TABLE groups_subjects;" +
        //         "TRUNCATE TABLE sub_subjects;" +
        //         "TRUNCATE TABLE subjects;" +
        //         "TRUNCATE TABLE subjects_information;" +
        //         "TRUNCATE TABLE news_receivers;" +
        //         "TRUNCATE TABLE news;" +
        //         "TRUNCATE TABLE student_requisites;" +
        //         "TRUNCATE TABLE groups;" +
        //         "TRUNCATE TABLE students;" +
        //         "TRUNCATE TABLE staff_departments;" +
        //         "TRUNCATE TABLE departments;" +
        //         "TRUNCATE TABLE staff_requisites;" +
        //         "TRUNCATE TABLE institutes;" +
        //         "TRUNCATE TABLE staff;"
        //         );
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE students_auth_data");
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE schedule");
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE classes");
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE groups_subjects");
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE sub_subjects");
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE subjects");
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE subjects_information");
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE news_receivers");
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE news");
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE student_requisites");
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE groups");
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE students");
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE staff_departments");
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE departments");
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE staff_requisites");
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE institutes");
        //     // _context.Database.ExecuteSqlRaw("TRUNCATE TABLE staff");
        //
        //     _context.SaveChanges();
        // }

        private void GenerateStaff()
        {
            _context.Staff.Add(new Staff
            {
                Name = "Игорь",
                Surname = "Архипов",
                Patronymic = "Олегович"
            });
            _context.Staff.Add(new Staff
            {
                Name = "Валентина",
                Surname = "Соболева",
                Patronymic = "Павловна"
            });
            _context.Staff.Add(new Staff
            {
                Name = "Александр",
                Surname = "Коробейников",
                Patronymic = "Васильевич"
            });
            _context.Staff.Add(new Staff
            {
                Name = "Владимир",
                Surname = "Тарасов",
                Patronymic = "Георгиевич"
            });
            _context.Staff.Add(new Staff
            {
                Name = "Артем",
                Surname = "Юминов"
            });
            _context.SaveChanges();
        }

        private void GenerateStaffRequisites()
        {
            _context.StaffRequisites.Add(new StaffRequisite
            {
                StaffId = _context.Staff.First(s => s.Surname == "Архипов").Id,
                EMail = "a@istu.ru"
            });
            _context.StaffRequisites.Add(new StaffRequisite
            {
                StaffId = _context.Staff.First(s => s.Surname == "Тарасов").Id,
                EMail = "t@istu.ru"
            });
            _context.StaffRequisites.Add(new StaffRequisite
            {
                StaffId = _context.Staff.First(s => s.Surname == "Соболева").Id,
                EMail = "s@istu.ru"
            });
            _context.StaffRequisites.Add(new StaffRequisite
            {
                StaffId = _context.Staff.First(s => s.Surname == "Коробейников").Id,
                EMail = "k@istu.ru"
            });
            _context.StaffRequisites.Add(new StaffRequisite
            {
                StaffId = _context.Staff.First(s => s.Surname == "Юминов").Id,
                EMail = "u@istu.ru"
            });
            _context.SaveChanges();
        }

        private void GenerateInstitutes()
        {
            _context.Institutes.Add(new Institute
            {
                Name = "Институт \"Информатика и вычислительная техника\"",
                ShortName = "ИВТ",
                DirectorId = _context.Staff.First(s => s.Surname == "Архипов").Id,
                DirectorDeputyId = _context.Staff.First(s => s.Surname == "Соболева").Id
            });
            _context.SaveChanges();
        }

        private void GenerateDepartments()
        {
            _context.Departments.Add(new Department
            {
                InstituteId = _context.Institutes.First(i => i.ShortName == "ИВТ").Id,
                Name = "Кафедра \"Программное обеспечение\"",
                ShortName = "Кафедра ПО",
                DirectorId = _context.Staff.First(s => s.Surname == "Коробейников").Id
            });
            _context.SaveChanges();
        }

        private void GenerateStaffDepartments()
        {
            _context.StaffDepartments.Add(new StaffDepartment
            {
                DepartmentId = _context.Departments.First(d => d.ShortName == "Кафедра ПО").Id,
                StaffId = _context.Staff.First(s => s.Surname == "Соболева").Id
            });
            _context.StaffDepartments.Add(new StaffDepartment
            {
                DepartmentId = _context.Departments.First(d => d.ShortName == "Кафедра ПО").Id,
                StaffId = _context.Staff.First(s => s.Surname == "Архипов").Id
            });
            _context.StaffDepartments.Add(new StaffDepartment
            {
                DepartmentId = _context.Departments.First(d => d.ShortName == "Кафедра ПО").Id,
                StaffId = _context.Staff.First(s => s.Surname == "Коробейников").Id
            });
            _context.StaffDepartments.Add(new StaffDepartment
            {
                DepartmentId = _context.Departments.First(d => d.ShortName == "Кафедра ПО").Id,
                StaffId = _context.Staff.First(s => s.Surname == "Тарасов").Id
            });
            _context.StaffDepartments.Add(new StaffDepartment
            {
                DepartmentId = _context.Departments.First(d => d.ShortName == "Кафедра ПО").Id,
                StaffId = _context.Staff.First(s => s.Surname == "Юминов").Id
            });
            _context.SaveChanges();
        }

        private void GenerateStudents()
        {
            _context.Students.Add(new Student
            {
                StudentNumber = "12345678",
                Name = "Никита",
                Surname = "Вахрушев"
            });
            _context.Students.Add(new Student
            {
                StudentNumber = "02345678",
                Name = "Никита",
                Surname = "Штек"
            });
            _context.Students.Add(new Student
            {
                StudentNumber = "00345678",
                Name = "Максим",
                Surname = "Поздеев",
                Patronymic = "Львович"
            });
            _context.Students.Add(new Student
            {
                StudentNumber = "00045678",
                Name = "Иван",
                Surname = "Зырянов",
                Patronymic = "Олегович"
            });
            _context.Students.Add(new Student
            {
                StudentNumber = "00005678",
                Name = "Михаил",
                Surname = "Мокрушин"
            });
            _context.Students.Add(new Student
            {
                StudentNumber = "00000678",
                Name = "Михаил",
                Surname = "Ивченко"
            });
            _context.SaveChanges();
        }

        private void GenerateGroups()
        {
            _context.Groups.Add(new Group
            {
                Name = "Б08-191-1",
                DepartmentId = _context.Departments.First(d => d.ShortName == "Кафедра ПО").Id,
                HeadId = _context.Students.First(s => s.Surname == "Штек").Id
            });
            _context.Groups.Add(new Group
            {
                Name = "Б08-191-2",
                DepartmentId = _context.Departments.First(d => d.ShortName == "Кафедра ПО").Id,
                HeadId = _context.Students.First(s => s.Surname == "Вахрушев").Id
            });
            _context.SaveChanges();
        }

        private void GenerateStudentsRequisites()
        {
            _context.StudentRequisites.Add(new StudentRequisites
            {
                StudentId = _context.Students.First(s => s.Surname == "Штек").Id,
                GroupId = _context.Groups.First(g => g.Name == "Б08-191-1").Id
            });
            _context.StudentRequisites.Add(new StudentRequisites
            {
                StudentId = _context.Students.First(s => s.Surname == "Ивченко").Id,
                GroupId = _context.Groups.First(g => g.Name == "Б08-191-1").Id
            });
            _context.StudentRequisites.Add(new StudentRequisites
            {
                StudentId = _context.Students.First(s => s.Surname == "Мокрушин").Id,
                GroupId = _context.Groups.First(g => g.Name == "Б08-191-1").Id
            });
            _context.StudentRequisites.Add(new StudentRequisites
            {
                StudentId = _context.Students.First(s => s.Surname == "Вахрушев").Id,
                GroupId = _context.Groups.First(g => g.Name == "Б08-191-2").Id
            });
            _context.StudentRequisites.Add(new StudentRequisites
            {
                StudentId = _context.Students.First(s => s.Surname == "Поздеев").Id,
                GroupId = _context.Groups.First(g => g.Name == "Б08-191-2").Id
            });
            _context.StudentRequisites.Add(new StudentRequisites
            {
                StudentId = _context.Students.First(s => s.Surname == "Зырянов").Id,
                GroupId = _context.Groups.First(g => g.Name == "Б08-191-2").Id
            });
            _context.SaveChanges();
        }

        private void GenerateClasses()
        {
            _context.Classes.Add(new Class
            {
                StartTime = "8:30",
                EndTime = "10:00",
                Number = 1
            });
            _context.Classes.Add(new Class
            {
                StartTime = "10:10",
                EndTime = "11:40",
                Number = 2
            });
            _context.Classes.Add(new Class
            {
                StartTime = "12:20",
                EndTime = "13:50",
                Number = 3
            });
            _context.Classes.Add(new Class
            {
                StartTime = "14:00",
                EndTime = "15:30",
                Number = 4
            });
            _context.Classes.Add(new Class
            {
                StartTime = "15:40",
                EndTime = "17:10",
                Number = 5
            });
            _context.Classes.Add(new Class
            {
                StartTime = "17:20",
                EndTime = "18:50",
                Number = 6
            });
            _context.Classes.Add(new Class
            {
                StartTime = "19:00",
                EndTime = "20:30",
                Number = 7
            });
            _context.SaveChanges();
        }

        private void GenerateSubjectsInformation()
        {
            _context.SubjectsInformation.Add(new SubjectInformation
            {
                Name = "Операционные системы",
                ShortName = "ОС",
                Description = "Учи ОС!"
            });
            _context.SubjectsInformation.Add(new SubjectInformation
            {
                Name = "Математические основы искусственного интеллекта",
                ShortName = "МОИИ",
                Description = "Учи МОИИ!"
            });
            _context.SubjectsInformation.Add(new SubjectInformation
            {
                Name = "Теория цифровой обработки сигналов",
                ShortName = "ТЦОС",
                Description = "Учи ТЦОС!"
            });
            _context.SubjectsInformation.Add(new SubjectInformation
            {
                Name = "Информатика",
                Description = "Учи Информатику!"
            });
            _context.SaveChanges();
        }

        private void GenerateSubjects()
        {
            _context.Subjects.Add(new Subject
            {
                LecturerId = _context.Staff.First(s => s.Surname == "Архипов").Id,
                InformationId = _context.SubjectsInformation.First(si => si.ShortName == "ТЦОС").Id
            });
            _context.Subjects.Add(new Subject
            {
                LecturerId = _context.Staff.First(s => s.Surname == "Тарасов").Id,
                InformationId = _context.SubjectsInformation.First(si => si.ShortName == "ОС").Id
            });
            _context.Subjects.Add(new Subject
            {
                LecturerId = _context.Staff.First(s => s.Surname == "Коробейников").Id,
                InformationId = _context.SubjectsInformation.First(si => si.ShortName == "МОИИ").Id
            });
            _context.Subjects.Add(new Subject
            {
                LecturerId = _context.Staff.First(s => s.Surname == "Соболева").Id,
                InformationId = _context.SubjectsInformation.First(si => si.Name == "Информатика").Id
            });
            _context.SaveChanges();
        }

        private void GenerateSubSubjects()
        {
            _context.SubSubjects.Add(new SubSubject
            {
                SubjectId = _context.Subjects.First(s =>
                    s.InformationId == _context.SubjectsInformation.First(si => si.ShortName == "ТЦОС").Id).Id,
                Name = "Теория цифровой обработки сигналов. Практика",
                Shortname = "ТЦОС (Практика)",
                SubLecturerId = _context.Staff.First(s => s.Surname == "Архипов").Id
            });
            _context.SubSubjects.Add(new SubSubject
            {
                SubjectId = _context.Subjects.First(s =>
                    s.InformationId == _context.SubjectsInformation.First(si => si.ShortName == "ТЦОС").Id).Id,
                Name = "Теория цифровой обработки сигналов. Лабораторная",
                Shortname = "ТЦОС (Лаб)",
                SubLecturerId = _context.Staff.First(s => s.Surname == "Архипов").Id
            });
            _context.SubSubjects.Add(new SubSubject
            {
                SubjectId = _context.Subjects.First(s =>
                    s.InformationId == _context.SubjectsInformation.First(si => si.ShortName == "ОС").Id).Id,
                Name = "Операционные системы. Лабораторная",
                Shortname = "ОС (Лаб)",
                SubLecturerId = _context.Staff.First(s => s.Surname == "Юминов").Id
            });
            _context.SubSubjects.Add(new SubSubject
            {
                SubjectId = _context.Subjects.First(s =>
                    s.InformationId == _context.SubjectsInformation.First(si => si.Name == "Информатика").Id).Id,
                Name = "Информатика. Лабораторная",
                Shortname = "Информатика (Лаб)",
                SubLecturerId = _context.Staff.First(s => s.Surname == "Соболева").Id
            });
            _context.SaveChanges();
        }

        private void GenerateGroupsSubjects()
        {
            _context.GroupsSubjects.Add(new GroupSubject
            {
                GroupId = _context.Groups.First(g => g.Name == "Б08-191-1").Id,
                SubjectId = _context.Subjects.First(s =>
                    s.InformationId == _context.SubjectsInformation.First(si => si.ShortName == "ОС").Id).Id,
                Semester = 6
            });
            _context.GroupsSubjects.Add(new GroupSubject
            {
                GroupId = _context.Groups.First(g => g.Name == "Б08-191-1").Id,
                SubjectId = _context.Subjects.First(s =>
                    s.InformationId == _context.SubjectsInformation.First(si => si.ShortName == "МОИИ").Id).Id,
                Semester = 6
            });
            _context.GroupsSubjects.Add(new GroupSubject
            {
                GroupId = _context.Groups.First(g => g.Name == "Б08-191-1").Id,
                SubjectId = _context.Subjects.First(s =>
                    s.InformationId == _context.SubjectsInformation.First(si => si.ShortName == "ТЦОС").Id).Id,
                Semester = 5
            });
            _context.GroupsSubjects.Add(new GroupSubject
            {
                GroupId = _context.Groups.First(g => g.Name == "Б08-191-1").Id,
                SubjectId = _context.Subjects.First(s =>
                    s.InformationId == _context.SubjectsInformation.First(si => si.Name == "Информатика").Id).Id,
                Semester = 1
            });

            _context.GroupsSubjects.Add(new GroupSubject
            {
                GroupId = _context.Groups.First(g => g.Name == "Б08-191-2").Id,
                SubjectId = _context.Subjects.First(s =>
                    s.InformationId == _context.SubjectsInformation.First(si => si.ShortName == "ОС").Id).Id,
                Semester = 6
            });
            _context.GroupsSubjects.Add(new GroupSubject
            {
                GroupId = _context.Groups.First(g => g.Name == "Б08-191-2").Id,
                SubjectId = _context.Subjects.First(s =>
                    s.InformationId == _context.SubjectsInformation.First(si => si.ShortName == "МОИИ").Id).Id,
                Semester = 6
            });
            _context.GroupsSubjects.Add(new GroupSubject
            {
                GroupId = _context.Groups.First(g => g.Name == "Б08-191-2").Id,
                SubjectId = _context.Subjects.First(s =>
                    s.InformationId == _context.SubjectsInformation.First(si => si.ShortName == "ТЦОС").Id).Id,
                Semester = 5
            });
            _context.GroupsSubjects.Add(new GroupSubject
            {
                GroupId = _context.Groups.First(g => g.Name == "Б08-191-2").Id,
                SubjectId = _context.Subjects.First(s =>
                    s.InformationId == _context.SubjectsInformation.First(si => si.Name == "Информатика").Id).Id,
                Semester = 1
            });
            _context.SaveChanges();
        }

        // private void GenerateSchedule()
        // {
        //     _context.Schedule.Add(new Schedule
        //     {
        //         ClassId = _context.Classes.First(c => c.Number == 1).Id,
        //         Date = DateTime.
        //     });
        // }

        #endregion
    }
}