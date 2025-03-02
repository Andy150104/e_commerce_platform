
using Auth.Controllers;

namespace Auth.Identity.Controllers.V1.User;

public class UserInsertResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}