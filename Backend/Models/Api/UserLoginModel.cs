using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Api;

public class UserLoginModel
{
    [Required(ErrorMessage = "Login is required")]
    [StringLength(30, ErrorMessage = "Login must be between 5 and 20 characters", MinimumLength = 5)]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, ErrorMessage = "Login must be between 3 and 100 characters", MinimumLength = 3)]
    public string Password { get; set; }
}