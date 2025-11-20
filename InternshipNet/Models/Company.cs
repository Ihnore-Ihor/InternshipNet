using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace InternshipNet.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public ICollection<Internship> Internships { get; set; }
    }
}
