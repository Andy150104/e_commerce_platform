using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Server.Identity.Models;

[Table(nameof(Role))]
public class Role : IdentityRole<long>
{
    public List<UserRole> UserRoles { get; set; }
}