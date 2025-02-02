using Server.Controllers;

namespace Server.Identity.Controllers.V1.User;

public class UserInsertVerifyResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}