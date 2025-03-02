using Auth.Controllers;

namespace Auth.Identity.Controllers.V1.User;

public class UserInsertVerifyResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}