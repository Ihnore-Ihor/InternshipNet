namespace InternshipNet.API.DTOs
{
    public class InternshipDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRemote { get; set; }
        public string CompanyName { get; set; } // Instead of a Company object, we just return the name
    }
}