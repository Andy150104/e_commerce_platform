using Client.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Server.Controllers.V1.ForgetPasswordScreen
{
    public class FPSVerifyTokenRequest : AbstractApiRequest
    {
        [Required(ErrorMessage = "OTP is required")]
        public string OTP { get; set; } = string.Empty;

        [Required(ErrorMessage = "NewPassword is required")]
        public string NewPassWord { get; set; } = string.Empty;
    }
}
