using Client.Models;

namespace Client.Repositories;

public interface IIdentityRepository : IBaseRepository<User, string, VwUserAuthentication>
{
    VwUserAuthentication? GetVwUserAuthentication(string username, string email);
    
    VwUserAuthentication? GetUserName(string username);
}