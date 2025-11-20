using InternshipNet.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace InternshipNet.ViewModels
{
    public class AddInternshipViewModel : ViewModelBase, IDataErrorInfo
    {
        private string _title;
        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        private bool _isRemote;
        public bool IsRemote
        {
            get => _isRemote;
            set { _isRemote = value; OnPropertyChanged(); }
        }

        // Список компаній
        public ObservableCollection<Company> Companies { get; set; }

        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get => _selectedCompany;
            set { _selectedCompany = value; OnPropertyChanged(); }
        }

        public AddInternshipViewModel(List<Company> companies)
        {
            Companies = new ObservableCollection<Company>(companies);
            SelectedCompany = Companies.FirstOrDefault();
        }

        // Метод для додавання нової компанії в список (викликається з View)
        public void AddNewCompanyToList(Company newCompany)
        {
            Companies.Add(newCompany);
            SelectedCompany = newCompany;
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Title) && string.IsNullOrWhiteSpace(Title))
                    return "Title is required.";
                if (columnName == nameof(SelectedCompany) && SelectedCompany == null)
                    return "Company is required.";
                return string.Empty;
            }
        }

        public string Error => null;
        public bool IsValid => !string.IsNullOrWhiteSpace(Title) && SelectedCompany != null;
    }
}