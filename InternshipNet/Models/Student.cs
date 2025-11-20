using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace InternshipNet.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Зв'язок 1-1: У студента одне резюме
        public Resume Resume { get; set; }

        // Зв'язок N-M: Студент подає багато заявок
        public ICollection<StudentApplication> Applications { get; set; }
    }
}
