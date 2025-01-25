using Microsoft.EntityFrameworkCore;
using Server.Systemserver;

namespace Server.Models.Helper;

public class AppDbContext : PersonalStoreContext
{
    public NLog.Logger _Logger;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(new DbContextOptions<PersonalStoreContext>())
    {
    }
    
    public IdentityEntity IdentityEntity { get; set; }

    public User User { get; set; }
}