namespace StudioWebApp.Models;
public class Job
{
    public Job()
    {
        Teams = new List<Team>();
    }
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Team> Teams { get; set; }
}
