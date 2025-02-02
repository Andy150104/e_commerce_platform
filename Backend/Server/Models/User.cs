using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Server.Models;

public class User : IdentityUser
{
    public bool IsActive { get; set; }

    public string RoleId { get; set; }
    
    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; } = null!;
}