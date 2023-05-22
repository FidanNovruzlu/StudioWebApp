using StudioWebApp.Models;

namespace StudioWebApp.ViewModels.TeamVM;

public class CreateTeamVM
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public int JobId   { get; set; }
    public IFormFile ProfileImage { get; set; } = null!;
    public List<Job>? Jobs { get; set; } 
}
