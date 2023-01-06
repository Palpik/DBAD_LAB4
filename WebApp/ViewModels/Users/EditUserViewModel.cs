using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Users;

public class EditUserViewModel
{
    public string Id { get; set; }

    [Required(ErrorMessage = "Enter email")]
    [EmailAddress(ErrorMessage = "Incorrect email")]
    [Display(Name = "Email")]
    public string Email { get; set; }


    [Required(ErrorMessage = "Enter new password")]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    public string NewPassword { get; set; }


    [Required(ErrorMessage = "Enter old password")]
    [Display(Name = "Old password")]
    public string OldPassword { get; set; }

}