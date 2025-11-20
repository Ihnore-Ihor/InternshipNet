using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipNet.Models
{
    public class StudentApplication
    {
        // Складений ключ (Composite Key) буде налаштовано в Context пізніше:
        // Він складатиметься з StudentId + InternshipId

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int InternshipId { get; set; }
        public Internship Internship { get; set; }

        public ApplicationStatus Status { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}