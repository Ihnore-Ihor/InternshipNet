using System.ComponentModel.DataAnnotations; // Для валідації

namespace InternshipNet.API.DTOs
{
    public class CreateInternshipDto
    {
        [Required(ErrorMessage = "Назва стажування є обов'язковою")]
        [StringLength(100, ErrorMessage = "Назва не може бути довшою за 100 символів")]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsRemote { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}