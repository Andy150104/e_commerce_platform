using System.Security.Claims;
using Auth.Systemserver;

namespace Auth.Identity;

public interface IIdentityApiClient
{
    public IdentityEntity GetIdentity(ClaimsPrincipal user);
}