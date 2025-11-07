using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternshipNet.Models;
using System.Collections.ObjectModel;

namespace InternshipNet.ViewModels
{
    // Цей клас буде використовуватися для відображення в UI
    public class InternshipViewModel : ViewModelBase
    {
        // Ми можемо додати сюди логіку, специфічну для UI, якщо потрібно
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public ObservableCollection<StudentApplication> Applications { get; set; } = new ObservableCollection<StudentApplication>();
    }
}