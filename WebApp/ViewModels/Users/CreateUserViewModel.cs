using System.ComponentModel.DataAnnotations;


namespace WebApp.ViewModels.Users;

public class CreateUserViewModel
{
    [Required(ErrorMessage = "Enter email")]
    [EmailAddress(ErrorMessage = "Incorrect email")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Enter password")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
}
