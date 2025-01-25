using System.Security.Claims;

namespace Client.SystemClient;

public interface IIdentityApiClient
{
    public IdentityEntity GetIdentity(ClaimsPrincipal user);
}