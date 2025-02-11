using Client.Controllers;

namespace server.Controllers.V1.UserRegisterScreen;

public class URSUserVerifyRequest : AbstractApiRequest
{
    public string Key { get; set; }
}