using System.ComponentModel.DataAnnotations;

namespace server.Identity.Controllers.V1.User;

public class UserInsertRequest
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}