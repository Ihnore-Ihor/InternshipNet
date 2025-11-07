using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternshipNet.Models;
using System.Collections.ObjectModel;
using System.ComponentModel; // <-- Додайте цей using

namespace InternshipNet.ViewModels
{
    // Реалізуємо інтерфейс IDataErrorInfo
    public class AddApplicationViewModel : ViewModelBase, IDataErrorInfo
    {
        private string _studentName;
        public string StudentName
        {
            get => _studentName;
            set { _studentName = value; OnPropertyChanged(); }
        }

        private string _status;
        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> Statuses { get; }
        public StudentApplication Application { get; private set; }

        public AddApplicationViewModel()
        {
            Statuses = new ObservableCollection<string> { "Pending", "Accepted", "Rejected" };
            Status = "Pending";
        }

        public void CreateApplication()
        {
            Application = new StudentApplication { StudentName = this.StudentName, Status = this.Status };
        }

        // --- РЕАЛІЗАЦІЯ ВАЛІДАЦІЇ ---

        // Ця властивість буде містити текст помилки для конкретної властивості (напр., StudentName)
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                if (columnName == nameof(StudentName) && string.IsNullOrWhiteSpace(StudentName))
                {
                    error = "Student name cannot be empty.";
                }
                return error;
            }
        }

        // Ця властивість може повертати загальну помилку для всього об'єкта. Нам це не потрібно.
        public string Error => null;

        // Допоміжна властивість, щоб перевірити, чи всі дані валідні
        public bool IsValid => string.IsNullOrWhiteSpace(this[nameof(StudentName)]);
    }
}