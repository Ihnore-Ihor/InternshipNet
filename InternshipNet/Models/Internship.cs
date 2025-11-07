using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipNet.Models
{
    // Додайте ключове слово 'public'
    public class Internship
    {
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public ObservableCollection<StudentApplication> Applications { get; set; } = new ObservableCollection<StudentApplication>();
    }
}
