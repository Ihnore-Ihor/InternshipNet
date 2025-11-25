using System.ComponentModel.DataAnnotations;

namespace InternshipNet.API.DTOs
{
    public class CreateCompanyDto
    {
        [Required(ErrorMessage = "Назва компанії обов'язкова")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Вкажіть індустрію (IT, Finance, etc.)")]
        public string Industry { get; set; }
    }
}