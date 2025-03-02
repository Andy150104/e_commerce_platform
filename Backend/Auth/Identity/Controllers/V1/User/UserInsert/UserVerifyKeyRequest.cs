using Client.Controllers;

namespace Auth.Controllers.V1.UserRegisterScreen;

public class UserVerifyKeyRequest : AbstractApiRequest
{
    public string Key { get; set; }
}