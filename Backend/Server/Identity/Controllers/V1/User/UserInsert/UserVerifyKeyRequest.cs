using Client.Controllers;

namespace server.Controllers.V1.UserRegisterScreen;

public class UserVerifyKeyRequest : AbstractApiRequest
{
    public string Key { get; set; }
}