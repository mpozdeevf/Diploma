using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniversityAdministrator.Models;

namespace UniversityAdministrator.Views.Students
{
    public interface IStudentsWindow
    {
        public void UpdateUi(ObservableCollection<StudentModel> students, ObservableCollection<string> institutes,
            ObservableCollection<string> departments, ObservableCollection<string> groups);
    }
}