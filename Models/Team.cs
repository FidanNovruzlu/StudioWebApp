namespace StudioWebApp.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string ProfileImageName { get; set; } = null!;
        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}
