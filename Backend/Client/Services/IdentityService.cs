using System.Security.Principal;
using Client.Models;
using Client.Repositories;
using Client.SystemClient;

namespace Client.Services;

public class IdentityService : BaseService<User, string, VwUserAuthentication>, IIdentityService
{
    private readonly IIdentityRepository _identityRepository;

    public IdentityEntity IdentityEntity { get; set; }
    
    public IdentityService(IBaseRepository<User, string, VwUserAuthentication> repository, IIdentityRepository identityRepository) : base(repository)
    {
        _identityRepository = identityRepository;
    }

    public VwUserAuthentication? GetVwUserAuthenticationsAsync(string username, string email)
    {
        return _identityRepository.GetVwUserAuthentication(username, email);
    }

    public string GetUserName(string username)
    {
        return _identityRepository.GetUserName(username).UserName;
    }

    public bool AddUser(User user)
    {
        return Repository.Add(user);
    }
    
}