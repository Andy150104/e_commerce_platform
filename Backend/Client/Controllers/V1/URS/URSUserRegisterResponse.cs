using Client.Controllers;

namespace server.Controllers.V1.UserRegisterScreen;

public class URSUserRegisterResponse : AbstractApiResponse<object>
{
    public override object Response { get; set; }
}