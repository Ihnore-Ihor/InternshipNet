using System.Windows;
using InternshipNet.Data;
using InternshipNet.Models;
using InternshipNet.ViewModels;

namespace InternshipNet.Views
{
    public partial class AddApplicationView : Window
    {
        public AddApplicationView()
        {
            InitializeComponent();
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SimpleInputDialog("New Student", "Enter Student Name:");
            if (dialog.ShowDialog() == true)
            {
                string name = dialog.ResultText;

                using (var context = new ApplicationDbContext())
                {
                    var newStudent = new Student { Name = name, Email = "new@student.com" };
                    context.Students.Add(newStudent);
                    context.SaveChanges();

                    if (DataContext is AddApplicationViewModel vm)
                    {
                        vm.AddNewStudentToList(newStudent);
                    }
                }
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as AddApplicationViewModel;
            if (vm != null && vm.IsValid)
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please select a student.");
            }
        }
    }
}