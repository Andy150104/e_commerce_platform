using System.ComponentModel.DataAnnotations;
using Client.Controllers;

namespace client.Identity.Controllers.V1.FPS;

public class FPSVerifyKeyRequest: AbstractApiRequest
{
    [Required(ErrorMessage = "Key is required")]
    public string Key { get; set; }
        
    [Required(ErrorMessage = "NewPassword is required")]
    public string NewPassWord { get; set; }
}