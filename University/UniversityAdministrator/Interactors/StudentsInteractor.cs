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

        public IEnumerable<StudentModel> GetStudents() => _dbStudents.Select(GetStudent);

        public IEnumerable<string> GetInstitutes() => _dbInstitutes.Select(i => i.Name);
        
        public IEnumerable<string> GetDepartments() => _dbDepartments.Select(d => d.Name);

        public IEnumerable<string> GetGroups() => _dbGroups.Select(g => g.Name);
        
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