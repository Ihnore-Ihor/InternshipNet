using System.ComponentModel.DataAnnotations;

namespace InternshipNet.API.DTOs
{
    public class CreateStudentDto
    {
        [Required(ErrorMessage = "Ім'я студента обов'язкове")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Некоректний формат пошти")]
        public string Email { get; set; }
    }
}