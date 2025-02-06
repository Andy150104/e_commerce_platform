using Client.Controllers;

namespace server.Controllers.V1.UserRegisterScreen;

public class URSUserRegisterResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}