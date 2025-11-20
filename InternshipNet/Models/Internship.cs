using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipNet.Models
{
    public class Internship
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Зв'язок 1-N: Стажування належить одній компанії
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public bool IsRemote { get; set; }

        // Зв'язок N-M: На стажування подано багато заявок
        public ICollection<StudentApplication> Applications { get; set; }
    }
}
