using client.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Server.Models.Helper;

public class AppDbContext : AuthenticationContext
{
    public NLog.Logger _Logger;

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) 
        : base(new DbContextOptions<AuthenticationContext>(), configuration)
    {
    }
}