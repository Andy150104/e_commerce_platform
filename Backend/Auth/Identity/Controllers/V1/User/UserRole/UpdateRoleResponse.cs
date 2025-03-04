using Auth.Controllers;

namespace client.Identity.Controllers.V1.User.UserRole;

public class UpdateRoleResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}