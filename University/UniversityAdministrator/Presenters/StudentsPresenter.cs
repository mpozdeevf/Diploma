using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniversityAdministrator.Interactors;
using UniversityAdministrator.Models;
using UniversityAdministrator.Views.Students;

namespace UniversityAdministrator.Presenters
{
    public class StudentsPresenter
    {
        private readonly StudentsInteractor _interactor;
        private readonly IStudentsWindow _window;

        public StudentsPresenter(IStudentsWindow window)
        {
            _window = window;
            _interactor = new StudentsInteractor();
            UpdateData();
        }

        public void DeleteStudent(StudentModel student)
        {
            _interactor.DeleteStudent(student);
        }

        public void EditStudents(IEnumerable<StudentModel> students)
        {
            _interactor.EditStudents(students);
        }

        public void UpdateData()
        {
            _interactor.UpdateData();
            UpdateUi();
        }

        private void UpdateUi()
        {
            var students = new ObservableCollection<StudentModel>(_interactor.GetStudents());
            var institutes = new ObservableCollection<string>(_interactor.GetInstitutes());
            var departments = new ObservableCollection<string>(_interactor.GetDepartments());
            var groups = new ObservableCollection<string>(_interactor.GetGroups());

            _window.UpdateUi(students, institutes, departments, groups);
        }
    }
}