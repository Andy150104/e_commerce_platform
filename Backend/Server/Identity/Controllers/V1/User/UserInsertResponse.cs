
using Server.Controllers;

namespace server.Identity.Controllers.V1.User;

public class UserInsertResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}