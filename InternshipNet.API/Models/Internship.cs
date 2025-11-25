using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipNet.API.Models
{
    public class Internship
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // 1-N relationship: Internship belongs to one company
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public bool IsRemote { get; set; }

        // N-M relationship: Many student applications can be submitted for an internship
        public ICollection<StudentApplication> Applications { get; set; }
    }
}
