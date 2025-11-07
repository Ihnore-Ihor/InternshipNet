using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InternshipNet.ViewModels; // Додайте цей using

namespace InternshipNet.Views
{
    public partial class AddApplicationView : Window
    {
        public AddApplicationView()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddApplicationViewModel;

            // ПЕРЕВІРЯЄМО, ЧИ ДАНІ ВАЛІДНІ
            if (viewModel != null && !viewModel.IsValid)
            {
                MessageBox.Show("Please correct the errors before proceeding.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Не закриваємо вікно
            }

            if (viewModel != null)
            {
                viewModel.CreateApplication();
            }

            this.DialogResult = true;
        }
    }
}
