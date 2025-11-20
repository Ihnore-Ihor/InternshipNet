using System.Windows;
using InternshipNet.Data; // Для доступу до БД
using InternshipNet.Models;
using InternshipNet.ViewModels;

namespace InternshipNet.Views
{
    public partial class AddInternshipView : Window
    {
        public AddInternshipView()
        {
            InitializeComponent();
        }

        private void AddCompany_Click(object sender, RoutedEventArgs e)
        {
            // Відкриваємо діалог
            var dialog = new SimpleInputDialog("New Company", "Enter Company Name:");
            if (dialog.ShowDialog() == true)
            {
                string companyName = dialog.ResultText;

                // Зберігаємо в БД
                using (var context = new ApplicationDbContext())
                {
                    var newComp = new Company { Name = companyName, Industry = "New Industry" };
                    context.Companies.Add(newComp);
                    context.SaveChanges();

                    // Оновлюємо ViewModel
                    if (DataContext is AddInternshipViewModel vm)
                    {
                        vm.AddNewCompanyToList(newComp);
                    }
                }
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as AddInternshipViewModel;
            if (vm != null && vm.IsValid)
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please fill in all required fields.");
            }
        }
    }
}