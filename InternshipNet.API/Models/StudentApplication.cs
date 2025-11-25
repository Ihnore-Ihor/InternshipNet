using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipNet.API.Models
{
    public class StudentApplication
    {
        // The composite key will be configured in the DbContext later:
        // It will consist of StudentId + InternshipId

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int InternshipId { get; set; }
        public Internship Internship { get; set; }

        public ApplicationStatus Status { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}