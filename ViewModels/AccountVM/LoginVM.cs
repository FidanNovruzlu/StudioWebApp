using System.ComponentModel.DataAnnotations;

namespace StudioWebApp.ViewModels.AccountVM
{
    public class LoginVM
    {
        [EmailAddress,Required]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
