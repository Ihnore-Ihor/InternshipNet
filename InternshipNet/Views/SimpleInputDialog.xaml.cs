using System.Windows;

namespace InternshipNet.Views
{
    public partial class SimpleInputDialog : Window
    {
        public string ResultText { get; private set; }

        public SimpleInputDialog(string title, string prompt)
        {
            InitializeComponent();
            this.Title = title;
            MessageText.Text = prompt;
            InputTextBox.Focus();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(InputTextBox.Text))
            {
                MessageBox.Show("Value cannot be empty.");
                return;
            }
            ResultText = InputTextBox.Text;
            DialogResult = true;
        }
    }
}