using System.Collections.Generic;
using System.Linq;
using UniversityAdministrator.Models;
using UniversityAdministrator.Repositories;
using UniversityData.Models;

namespace UniversityAdministrator.Interactors
{
    public class StudentsInteractor
    {
        private IEnumerable<Institute> _dbInstitutes;
        private IEnumerable<Department> _dbDepartments;
        private IEnumerable<Group> _dbGroups;
        private IEnumerable<Student> _dbStudents;

        public StudentsInteractor()
        {
            UpdateData();
        }

        public void UpdateData()
        {
            _dbInstitutes = DataRepository.GetDbInstitutes();
            _dbDepartments = DataRepository.GetDbDepartments();
            _dbGroups = DataRepository.GetDbGroups();
            _dbStudents = DataRepository.GetDbStudents();
        }
        
        public void DeleteStudent(StudentModel student)
        {
            DataRepository.DeleteStudent(student.StudentNumber);
        }

        public void EditStudents(IEnumerable<StudentModel> students)
        {
            foreach (var student in students)
            {
                if (_dbStudents.FirstOrDefault(dbs => dbs.StudentNumber == student.StudentNumber) == null)
                {
                    DataRepository.CreateStudent(GetDbStudent(student));
                }
                else
                {
                    DataRepository.UpdateStudent(GetDbStudent(student));
                }
            }
        }

        public IEnumerable<StudentModel> GetStudents() => _dbStudents.Select(GetStudent);

        public IEnumerable<string> GetInstitutes() => _dbInstitutes.Select(i => i.Name);
        
        public IEnumerable<string> GetDepartments() => _dbDepartments.Select(d => d.Name);

        public IEnumerable<string> GetGroups() => _dbGroups.Select(g => g.Name);

        private Student GetDbStudent(StudentModel student)
        {
            var dbStudent = new Student
            {
                StudentNumber = student.StudentNumber,
                GroupId = _dbGroups.First(g => g.Name == student.Group).Id,
                Name = student.Name,
                Surname = student.Surname,
                Patronymic = student.Patronymic
            };

            return dbStudent;
        }
        
        private StudentModel GetStudent(Student dbStudent)
        {
            var student = new StudentModel
            {
                Institute = _dbInstitutes
                    .First(i => i.Id == _dbDepartments
                        .First(d => d.Id == _dbGroups
                            .First(g => g.Id == dbStudent.GroupId).DepartmentId).InstituteId)
                    .Name,
                Department = _dbDepartments
                    .First(d => d.Id == _dbGroups
                        .First(g => g.Id == dbStudent.GroupId).DepartmentId).Name,
                Group = _dbGroups
                    .First(g => g.Id == dbStudent.GroupId).Name,
                Name = dbStudent.Name,
                Surname = dbStudent.Surname,
                Patronymic = dbStudent.Patronymic,
                StudentNumber = dbStudent.StudentNumber
            };

            return student;
        }
    }
}