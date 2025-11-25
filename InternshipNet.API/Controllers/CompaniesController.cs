using InternshipNet.API.Data;
using InternshipNet.API.DTOs;
using InternshipNet.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternshipNet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отримати список усіх компаній
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
        {
            var companies = await _context.Companies.ToListAsync();

            var dtos = companies.Select(c => new CompanyDto
            {
                Id = c.Id,
                Name = c.Name,
                Industry = c.Industry
            });

            return Ok(dtos);
        }

        /// <summary>
        /// Створити нову компанію
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> CreateCompany(CreateCompanyDto createDto)
        {
            // Mapping DTO -> Entity
            var company = new Company
            {
                Name = createDto.Name,
                Industry = createDto.Industry
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            // Mapping Entity -> DTO (answer)
            var dto = new CompanyDto
            {
                Id = company.Id,
                Name = company.Name,
                Industry = company.Industry
            };

            // Return status 201 Created
            return CreatedAtAction(nameof(GetCompanies), new { id = company.Id }, dto);
        }

        /// <summary>
        /// Оновити дані компанії
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CreateCompanyDto updateDto)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound($"Company with ID {id} not found.");
            }

            company.Name = updateDto.Name;
            company.Industry = updateDto.Industry;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Видалити компанію
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}