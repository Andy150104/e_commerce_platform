using Client.Controllers;

namespace Auth.Identity.Controllers.V1.User;

public class UserInsertVerifyRequest : AbstractApiRequest
{
    public string Key { get; set; }
}