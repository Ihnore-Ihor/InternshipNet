using InternshipNet.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace InternshipNet.ViewModels
{
    public class InternshipViewModel : ViewModelBase
    {
        private readonly Internship _model;

        public InternshipViewModel(Internship model)
        {
            _model = model;
            // Завантажуємо існуючі заявки у ViewModel
            if (model.Applications != null)
            {
                Applications = new ObservableCollection<StudentApplication>(model.Applications);
            }
        }

        // Повертаємо реальну модель для збереження
        public Internship GetModel() => _model;

        public int Id => _model.Id;

        public string Title
        {
            get => _model.Title;
            set { _model.Title = value; OnPropertyChanged(); }
        }

        // Беремо назву компанії з об'єкта Company
        public string CompanyName
        {
            get => _model.Company?.Name ?? "Unknown";
        }

        public string Description
        {
            get => _model.Description;
            set { _model.Description = value; OnPropertyChanged(); }
        }

        public bool IsRemote
        {
            get => _model.IsRemote;
            set
            {
                _model.IsRemote = value;
                OnPropertyChanged();
            }
        }

        public string Industry => _model.Company?.Industry ?? "General";

        public ObservableCollection<StudentApplication> Applications { get; set; } = new ObservableCollection<StudentApplication>();
    }
}