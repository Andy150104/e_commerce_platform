using Client.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Server.Controllers.V1.ForgetPasswordScreen;

    public class FPSUpdatePasswordRequest : AbstractApiRequest
    {
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string? Email { get; set; } = string.Empty;

        public string? UserName { get; set; } = string.Empty;
    }

