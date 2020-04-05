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