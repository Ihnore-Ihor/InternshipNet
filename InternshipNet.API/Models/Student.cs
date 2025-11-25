using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace InternshipNet.API.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // 1-to-1 relationship: A student has one resume
        public Resume Resume { get; set; }

        // Many-to-many relationship: A student can submit multiple applications
        public ICollection<StudentApplication> Applications { get; set; }
    }
}
