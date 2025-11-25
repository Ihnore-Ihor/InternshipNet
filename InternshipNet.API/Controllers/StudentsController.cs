using InternshipNet.API.Data;
using InternshipNet.API.DTOs;
using InternshipNet.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternshipNet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отримати всіх студентів
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            var students = await _context.Students
                .Include(s => s.Resume)
                .ToListAsync();

            var dtos = students.Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                HasResume = s.Resume != null
            });

            return Ok(dtos);
        }

        /// <summary>
        /// Створити нового студента
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent(CreateStudentDto createDto)
        {
            var student = new Student
            {
                Name = createDto.Name,
                Email = createDto.Email
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var dto = new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                HasResume = false
            };

            return CreatedAtAction(nameof(GetStudents), new { id = student.Id }, dto);
        }

        /// <summary>
        /// Видалити студента
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}