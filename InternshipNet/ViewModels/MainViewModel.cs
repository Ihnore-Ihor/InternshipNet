using InternshipNet.Data;
using InternshipNet.Models;
using InternshipNet.Services;
using InternshipNet.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace InternshipNet.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AdoNetService _adoService;

        public ObservableCollection<InternshipViewModel> Internships { get; set; }

        private InternshipViewModel _selectedInternship;
        public InternshipViewModel SelectedInternship
        {
            get => _selectedInternship;
            set
            {
                _selectedInternship = value;
                OnPropertyChanged();
                LoadApplications();
            }
        }

        public ObservableCollection<StudentApplication> SelectedApplications { get; set; }

        public ICommand LoadCommand { get; }
        public ICommand AddInternshipCommand { get; }
        public ICommand AddApplicationCommand { get; }
        public ICommand CheckStatsCommand { get; }
        public ICommand AcceptCommand { get; }
        public ICommand RejectCommand { get; }

        public MainViewModel()
        {
            _context = new ApplicationDbContext();
            _adoService = new AdoNetService();

            LoadCommand = new RelayCommand(LoadData);
            AddInternshipCommand = new RelayCommand(AddInternship);
            AddApplicationCommand = new RelayCommand(AddApplication);
            CheckStatsCommand = new RelayCommand(CheckStats);

            AcceptCommand = new RelayCommand(param => UpdateAppStatus(param, ApplicationStatus.Accepted));
            RejectCommand = new RelayCommand(param => UpdateAppStatus(param, ApplicationStatus.Rejected));

            try { LoadData(null); } catch { }
        }

        private void LoadData(object parameter)
        {
            var list = _context.Internships
                .Include(i => i.Company)
                .OrderByDescending(i => i.Id)
                .ToList();

            Internships = new ObservableCollection<InternshipViewModel>(
                list.Select(i => new InternshipViewModel(i))
            );
            OnPropertyChanged(nameof(Internships));
        }

        private void LoadApplications()
        {
            if (SelectedInternship == null) return;

            var apps = _context.StudentApplications
                .Where(sa => sa.InternshipId == SelectedInternship.Id)
                .Include(sa => sa.Student)
                .OrderByDescending(sa => sa.AppliedDate)
                .ToList();

            SelectedApplications = new ObservableCollection<StudentApplication>(apps);
            OnPropertyChanged(nameof(SelectedApplications));
        }

        private void AddInternship(object parameter)
        {
            var companies = _context.Companies.ToList();
            var vm = new AddInternshipViewModel(companies);
            var view = new Views.AddInternshipView
            {
                DataContext = vm,
                Owner = Application.Current.MainWindow
            };

            if (view.ShowDialog() == true)
            {
                var newInternship = new Internship
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    IsRemote = vm.IsRemote,
                    CompanyId = vm.SelectedCompany.Id
                };

                _context.Internships.Add(newInternship);
                _context.SaveChanges();
                LoadData(null);
            }
        }

        private void AddApplication(object parameter)
        {
            if (SelectedInternship == null) return;

            var allStudents = _context.Students.ToList();
            var vm = new AddApplicationViewModel(allStudents);
            var view = new Views.AddApplicationView
            {
                DataContext = vm,
                Owner = Application.Current.MainWindow
            };

            if (view.ShowDialog() == true)
            {
                bool exists = _context.StudentApplications.Any(sa =>
                    sa.StudentId == vm.SelectedStudent.Id &&
                    sa.InternshipId == SelectedInternship.Id);

                if (exists)
                {
                    MessageBox.Show("Student already applied!");
                    return;
                }

                var newApp = new StudentApplication
                {
                    StudentId = vm.SelectedStudent.Id,
                    InternshipId = SelectedInternship.Id,
                    Status = ApplicationStatus.Pending, // Тільки Pending за замовчуванням
                    AppliedDate = DateTime.UtcNow
                };

                _context.StudentApplications.Add(newApp);
                _context.SaveChanges();
                LoadApplications();
            }
        }

        private void UpdateAppStatus(object param, ApplicationStatus newStatus)
        {
            if (param is StudentApplication app)
            {
                try
                {
                    // Виклик ADO.NET процедури
                    _adoService.UpdateStatus(app.StudentId, app.InternshipId, (int)newStatus);
                    app.Status = newStatus;
                    LoadApplications();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void CheckStats(object parameter)
        {
            try
            {
                int count = _adoService.GetTotalApplicationsCount();
                MessageBox.Show($"Total Applications (ADO.NET): {count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}