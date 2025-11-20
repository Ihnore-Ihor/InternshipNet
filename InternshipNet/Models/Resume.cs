using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipNet.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public string Content { get; set; } // Текст резюме або посилання

        // Зовнішній ключ (Foreign Key) на студента
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
