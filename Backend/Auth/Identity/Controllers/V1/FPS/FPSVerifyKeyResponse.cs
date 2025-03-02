using Auth.Controllers;

namespace client.Identity.Controllers.V1.FPS;

public class FPSVerifyKeyResponse: AbstractApiResponse<string>
{
    public override string Response { get; set; }
}