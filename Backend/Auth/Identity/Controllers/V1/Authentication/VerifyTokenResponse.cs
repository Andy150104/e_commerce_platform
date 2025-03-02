using Auth.Controllers;

namespace Auth.Identity.Controllers;

public class VerifyTokenResponse : AbstractApiResponse<VerifyTokenEntity>
{
    public override VerifyTokenEntity Response { get; set; }
}

public class VerifyTokenEntity
{
    public string RoleName { get; set; }
}