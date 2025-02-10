using Client.Controllers;
using System.ComponentModel.DataAnnotations;

namespace server.Controllers.V1.ForgetPasswordScreen
{
    public class FPSVerifyTokenRequest : AbstractApiRequest
    {
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }

        public string? UserName { get; set; }
        [Required(ErrorMessage = "OTP is required")]
        public string OTP { get; set; }

        [Required(ErrorMessage = "NewPassword is required")]
        public string NewPassWord { get; set; }
    }
}
