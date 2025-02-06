using Client.Controllers;

namespace Server.Identity.Controllers.V1.User;

public class UserInsertVerifyRequest : AbstractApiRequest
{
    public string Key { get; set; }
}