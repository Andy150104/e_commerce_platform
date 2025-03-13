using Client.Models;
using Client.SystemClient;

namespace Client.Services;

public interface IIdentityService : IBaseService<User, string, VwUserAuthentication>
{ 
    public IdentityEntity IdentityEntity { get; set; }
    
    VwUserAuthentication? GetVwUserAuthenticationsAsync(string username, string email);
    
    string GetUserName(string username);
}