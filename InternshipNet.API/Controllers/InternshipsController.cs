using InternshipNet.API.Data;
using InternshipNet.API.DTOs;
using InternshipNet.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternshipNet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternshipsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Dependency Injection
        public InternshipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отримати список усіх стажувань
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InternshipDto>>> GetInternships()
        {
            var internships = await _context.Internships
                .Include(i => i.Company)
                .ToListAsync();

            // Mapping Entity -> DTO
            var dtos = internships.Select(i => new InternshipDto
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                IsRemote = i.IsRemote,
                CompanyName = i.Company?.Name ?? "Unknown"
            }).ToList();

            return Ok(dtos);
        }

        /// <summary>
        /// Отримати стажування по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<InternshipDto>> GetInternship(int id)
        {
            var internship = await _context.Internships
                .Include(i => i.Company)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (internship == null)
            {
                return NotFound($"Internship with ID {id} not found.");
            }

            // Mapping
            var dto = new InternshipDto
            {
                Id = internship.Id,
                Title = internship.Title,
                Description = internship.Description,
                IsRemote = internship.IsRemote,
                CompanyName = internship.Company?.Name
            };

            return Ok(dto);
        }

        /// <summary>
        /// Створити нове стажування
        /// </summary>
        /// <remarks>
        /// Приклад запиту:
        /// 
        ///     POST /api/internships
        ///     {
        ///        "title": ".NET Junior",
        ///        "description": "Coding C#",
        ///        "isRemote": true,
        ///        "companyId": 1
        ///     }
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<InternshipDto>> CreateInternship([FromBody] CreateInternshipDto createDto)
        {
            // Autovalidation of model because of [ApiController]

            var company = await _context.Companies.FindAsync(createDto.CompanyId);
            if (company == null)
            {
                return BadRequest("Invalid Company ID.");
            }

            // Mapping DTO -> Entity
            var internship = new Internship
            {
                Title = createDto.Title,
                Description = createDto.Description,
                IsRemote = createDto.IsRemote,
                CompanyId = createDto.CompanyId
            };

            _context.Internships.Add(internship);
            await _context.SaveChangesAsync();

            var resultDto = new InternshipDto
            {
                Id = internship.Id,
                Title = internship.Title,
                Description = internship.Description,
                IsRemote = internship.IsRemote,
                CompanyName = company.Name
            };

            return CreatedAtAction(nameof(GetInternship), new { id = internship.Id }, resultDto);
        }

        /// <summary>
        /// Оновити існуюче стажування
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInternship(int id, [FromBody] CreateInternshipDto updateDto)
        {
            var internship = await _context.Internships.FindAsync(id);
            if (internship == null)
            {
                return NotFound();
            }

            internship.Title = updateDto.Title;
            internship.Description = updateDto.Description;
            internship.IsRemote = updateDto.IsRemote;
            internship.CompanyId = updateDto.CompanyId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Видалити стажування
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInternship(int id)
        {
            var internship = await _context.Internships.FindAsync(id);
            if (internship == null)
            {
                return NotFound();
            }

            _context.Internships.Remove(internship);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}