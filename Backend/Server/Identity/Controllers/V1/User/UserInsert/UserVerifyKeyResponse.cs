using Client.Controllers;
using Server.Controllers;

namespace server.Controllers.V1.UserRegisterScreen;

public class UserVerifyKeyResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}