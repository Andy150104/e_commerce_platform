using Client.Controllers;

namespace Client.Controllers.V1.UserRegisterScreen;

public class URSUserRegisterResponse : AbstractApiResponse<object>
{
    public override object Response { get; set; }
}