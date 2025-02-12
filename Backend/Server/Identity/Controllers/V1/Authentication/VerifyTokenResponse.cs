using Server.Controllers;

namespace Server.Identity.Controllers;

public class VerifyTokenResponse : AbstractApiResponse<VerifyTokenEntity>
{
    public override VerifyTokenEntity Response { get; set; }
}

public class VerifyTokenEntity
{
    public string RoleName { get; set; }
}