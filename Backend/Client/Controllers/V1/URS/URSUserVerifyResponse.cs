using Client.Controllers;

namespace server.Controllers.V1.UserRegisterScreen;

public class URSUserVerifyResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}