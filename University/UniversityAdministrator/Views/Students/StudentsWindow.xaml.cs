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
            
        }
        
        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void CreateButton_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}