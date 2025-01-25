using System.Security.Claims;
using Server.Systemserver;

namespace Server.Identity;

public interface IIdentityApiClient
{
    public IdentityEntity GetIdentity(ClaimsPrincipal user);
}