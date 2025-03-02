using Client.Controllers;
using Auth.Controllers;

namespace Auth.Controllers.V1.UserRegisterScreen;

public class UserVerifyKeyResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}