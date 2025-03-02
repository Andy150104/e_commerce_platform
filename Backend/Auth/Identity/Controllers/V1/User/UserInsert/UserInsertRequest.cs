using System.ComponentModel.DataAnnotations;
using Client.Controllers;

namespace Auth.Identity.Controllers.V1.User;

public class UserInsertRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "First name is required")]
    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "First name contains invalid characters")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required")]
    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "First name contains invalid characters")]
    public string LastName { get; set; }
}