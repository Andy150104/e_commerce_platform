using Client.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Server.Controllers.V1.FPS;

public class FPSForgotPasswordRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; }
}