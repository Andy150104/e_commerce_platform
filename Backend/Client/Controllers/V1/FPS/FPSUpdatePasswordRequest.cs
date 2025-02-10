using Client.Controllers;
using System.ComponentModel.DataAnnotations;

namespace server.Controllers.V1.ForgetPasswordScreen;

    public class FPSUpdatePasswordRequest : AbstractApiRequest
    {
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }

        public string? UserName { get; set; }
    }

