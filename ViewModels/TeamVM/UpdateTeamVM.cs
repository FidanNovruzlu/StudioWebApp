using StudioWebApp.Models;

namespace StudioWebApp.ViewModels.TeamVM;

public class UpdateTeamVM
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int JobId { get; set; }
    public IFormFile? ProfileImage { get; set; }
    public string? ProfileImageName { get; set; }
    public List<Job>? Jobs { get; set; }
}
