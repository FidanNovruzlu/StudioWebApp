using System.ComponentModel.DataAnnotations;

namespace StudioWebApp.ViewModels.AccountVM
{
    public class RegisterVM
    {
        public string Name { get; set; } = null!;
        [EmailAddress,Required]
        public string Email { get; set; }= null!;
        [Required,DataType(DataType.Password),MinLength(8)]
        public string Password { get; set; } = null!;    
        public string Surname { get; set; } = null!;
        [Required,MaxLength(15)]
        public string UserName { get; set; } = null!;
        [Required,DataType(DataType.Password),Compare(nameof(Password)),MinLength(8)]
        public string ConfrimPassword { get; set; } = null!;
    }
}
