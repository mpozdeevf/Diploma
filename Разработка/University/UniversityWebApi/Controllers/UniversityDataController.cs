using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityData;
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
        public ActionResult<StudentRequisites> GetUserStudentRequisites(int id)
        {
            var studentRequisite = _context.StudentRequisites.FirstOrDefault(sr => sr.StudentId == id);
            if (studentRequisite == null)
            {
                return NotFound();
            }

            return Ok(studentRequisite);
        }

        [HttpGet("groupId")]
        [Route("student-group")]
        public IEnumerable<Group> GetStudentGroup(int groupId) =>
            _context.Groups.Where(gs => gs.Id == groupId);

        [HttpGet("groupId")]
        [Route("group-subjects")]
        public IEnumerable<GroupSubject> GetGroupSubjects(int groupId) =>
            _context.GroupsSubjects.Where(gs => gs.GroupId == groupId);

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
            }
            catch (Exception e)
            {
                BadRequest();
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

        #endregion
    }
}