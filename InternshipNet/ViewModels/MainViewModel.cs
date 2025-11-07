using InternshipNet.Models;
using InternshipNet.Services; // Додаємо using для сервісу
using InternshipNet.Mappers; // <-- Додаємо using для маппера
using InternshipNet.ViewModels; // <-- Додаємо using для ViewModel
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; // Додаємо using для MessageBoxusing System.Windows.Input;
using System.Windows.Input;

namespace InternshipNet.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly JsonDataService _dataService;
        private readonly InternshipMapper _mapper; // <-- Створюємо екземпляр маппера
        private const string FilePath = "internships.json"; // Назва файлу для збереження

        private InternshipViewModel _selectedInternship;
        public InternshipViewModel SelectedInternship
        {
            get => _selectedInternship;
            set { _selectedInternship = value; OnPropertyChanged(); }
        }

        public ObservableCollection<InternshipViewModel> Internships { get; set; }

        public ICommand LoadCommand { get; }
        public ICommand SaveCommand { get; }

        public ICommand AddApplicationCommand { get; }


        public MainViewModel()
        {
            _dataService = new JsonDataService(); // Створюємо екземпляр сервісу
            _mapper = new InternshipMapper(); // <-- Ініціалізуємо маппер

            LoadCommand = new RelayCommand(LoadData);
            SaveCommand = new RelayCommand(SaveData);

            AddApplicationCommand = new RelayCommand(AddApplication, CanAddApplication);

            // Завантажуємо дані при старті
            LoadData(null);
        }

        private bool CanAddApplication(object parameter)
        {
            // Команду можна виконати тільки якщо обрано стажування
            return SelectedInternship != null;
        }

        private void AddApplication(object parameter)
        {
            var viewModel = new AddApplicationViewModel();
            var view = new Views.AddApplicationView
            {
                DataContext = viewModel,
                Owner = Application.Current.MainWindow // Робимо вікно модальним відносно головного
            };

            // Показуємо вікно і чекаємо, поки його закриють
            if (view.ShowDialog() == true)
            {
                // Якщо користувач натиснув "OK", додаємо нову заявку до списку
                SelectedInternship.Applications.Add(viewModel.Application);
            }
        }

        private void LoadData(object parameter)
        {
            var internshipModels = _dataService.Load(FilePath);

            if (!internshipModels.Any()) // Якщо файл порожній
            {
                // Створюємо моделі, як і раніше
                internshipModels = new ObservableCollection<Internship> { /* ... ваші тестові дані ... */ };
            }

            // ПЕРЕТВОРЮЄМО моделі у ViewModel-і за допомогою маппера
            Internships = new ObservableCollection<InternshipViewModel>(
                internshipModels.Select(model => _mapper.Map(model))
            );

            OnPropertyChanged(nameof(Internships));
        }

        private void SaveData(object parameter)
        {
            // ПЕРЕТВОРЮЄМО ViewModel-і назад у моделі перед збереженням
            var internshipModels = new ObservableCollection<Internship>(
                Internships.Select(vm => _mapper.Map(vm))
            );

            _dataService.Save(FilePath, internshipModels);
            MessageBox.Show("Data saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}
