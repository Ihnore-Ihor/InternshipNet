using InternshipNet.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace InternshipNet.ViewModels
{
    public class AddApplicationViewModel : ViewModelBase, IDataErrorInfo
    {
        public ObservableCollection<Student> Students { get; set; }

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set { _selectedStudent = value; OnPropertyChanged(); }
        }

        public AddApplicationViewModel(List<Student> existingStudents)
        {
            Students = new ObservableCollection<Student>(existingStudents);
        }

        public void AddNewStudentToList(Student newStudent)
        {
            Students.Add(newStudent);
            SelectedStudent = newStudent;
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(SelectedStudent) && SelectedStudent == null)
                    return "Please select a student.";
                return string.Empty;
            }
        }

        public string Error => null;
        public bool IsValid => SelectedStudent != null;
    }
}