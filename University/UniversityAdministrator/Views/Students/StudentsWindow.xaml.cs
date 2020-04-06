using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UniversityAdministrator.Models;
using UniversityAdministrator.Presenters;

namespace UniversityAdministrator.Views.Students
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IStudentsWindow
    {
        private readonly StudentsPresenter _presenter;

        public MainWindow()
        {
            InitializeComponent();
            _presenter = new StudentsPresenter(this);
        }

        public void UpdateUi(ObservableCollection<StudentModel> students, ObservableCollection<string> institutes,
            ObservableCollection<string> departments, ObservableCollection<string> groups)
        {
            StudentsGrid.ItemsSource = null;
            InstituteColumn.ItemsSource = null;
            DepartmentColumn.ItemsSource = null;
            GroupColumn.ItemsSource = null;

            StudentsGrid.ItemsSource = students;
            InstituteColumn.ItemsSource = institutes;
            DepartmentColumn.ItemsSource = departments;
            GroupColumn.ItemsSource = groups;
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            _presenter.UpdateData();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var items = StudentsGrid.SelectedItems;
            foreach (var item in items)
            {
                _presenter.DeleteStudent((StudentModel) item);
            }
            _presenter.UpdateData();
        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            SetEditCreateMode();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            SetNormalMode();
            _presenter.UpdateData();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            SetNormalMode();
            _presenter.EditStudents(StudentsGrid.Items.Cast<StudentModel>());
            _presenter.UpdateData();
        }

        private void SetEditCreateMode()
        {
            StudentsGrid.CanUserAddRows = true;
            StudentsGrid.IsReadOnly = false;
            
            UpdateButton.Visibility = Visibility.Collapsed;
            DeleteButton.Visibility = Visibility.Collapsed;
            EditButton.Visibility = Visibility.Collapsed;

            CancelButton.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Visible;
        }

        private void SetNormalMode()
        {
            StudentsGrid.CanUserAddRows = false;
            StudentsGrid.IsReadOnly = true;
            
            UpdateButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
            EditButton.Visibility = Visibility.Visible;

            CancelButton.Visibility = Visibility.Collapsed;
            SaveButton.Visibility = Visibility.Collapsed;
        }
    }
}