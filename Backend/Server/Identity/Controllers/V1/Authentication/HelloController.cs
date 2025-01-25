using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace server.Identity.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
public class HelloController : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public string Get()
    {
        return "Hello World!";
    }
}