using Client.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Server.Controllers.V1.ForgetPasswordScreen;

public class FPSUpdatePasswordRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
}

