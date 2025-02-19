using Client.Controllers;

namespace client.Identity.Controllers.V1.User.UserRole;

public class UpdateRoleRequest : AbstractApiRequest
{
    public string UserName { get; set; }
    
    public Guid? PlanId { get; set; }
}