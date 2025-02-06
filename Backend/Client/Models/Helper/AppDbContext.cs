using Microsoft.EntityFrameworkCore;
using Client.SystemClient;
using server.Models;

namespace Client.Models.Helper;

public class AppDbContext : BBExTradingFloorContext
{
    public NLog.Logger _Logger;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(new DbContextOptions<BBExTradingFloorContext>())
    {
    }
    
     public User User { get; set; }
    
    public IdentityEntity IdentityEntity { get; set; }
}