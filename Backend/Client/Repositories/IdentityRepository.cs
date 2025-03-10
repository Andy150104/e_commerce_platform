using System.Security.Cryptography;
using System.Text;
using Client.Models;
using Client.Models.Helper;
using Microsoft.EntityFrameworkCore;
using SystemConfig = Client.Utils.Consts.SystemConfig;

namespace Client.Repositories;

public class IdentityRepository : BaseRepository<User, string, VwUserAuthentication>, IIdentityRepository
{
    public IdentityRepository(AppDbContext context) : base(context)
    {
    }

   /// <summary>
   /// Get user authentication details
   /// </summary>
   /// <param name="username"></param>
   /// <param name="email"></param>
   /// <returns></returns>
    public VwUserAuthentication? GetVwUserAuthentication(string username, string email)
    {
        return Context.VwUserAuthentications
            .AsNoTracking()
            .FirstOrDefault(auth => 
                auth.UserName == username || auth.Email == email);
    }

    public VwUserAuthentication? GetUserName(string username)
    {
        return Context.VwUserAuthentications
            .AsNoTracking()
            .FirstOrDefault(auth => 
                auth.UserName == username);
    }

    
}