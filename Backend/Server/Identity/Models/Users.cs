using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Identity.Models;

public class Users
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string PasswordHash { get; set; }
    
    public DateTime? LockDate { get; set; }
    
    public decimal AuthFailedCount { get; set; }

    public List<UserRole> UserRoles { get; set; }
}