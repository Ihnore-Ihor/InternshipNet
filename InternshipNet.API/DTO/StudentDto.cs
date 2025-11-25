namespace InternshipNet.API.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool HasResume { get; set; } // Just a flag indicating if a resume exists
    }
}