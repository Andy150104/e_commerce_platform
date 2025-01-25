using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Identity.Models;

public class UserRole
{
    [ForeignKey(nameof(Identity.Models.Users.Id))]
    public string UserId { get; set; }
    public Users User { get; set; }

    [ForeignKey(nameof(Identity.Models.Role.Id ))]
    public long RoleId { get; set; }
    public Role Role { get; set; }
}