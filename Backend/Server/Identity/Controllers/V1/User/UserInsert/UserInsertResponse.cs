
using Server.Controllers;

namespace Server.Identity.Controllers.V1.User;

public class UserInsertResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}