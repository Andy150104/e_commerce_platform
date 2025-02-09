using System.Security.Claims;
using Server.Identity;
using Server.Systemserver;

namespace Server.SystemClient;

public class IdentityApiClient : IIdentityApiClient
{
    /// <summary>
    /// Get the identity
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public IdentityEntity GetIdentity(ClaimsPrincipal user)
    {
        // Get the identity
        var identity = user.Identity as ClaimsIdentity;
        // Get the user id
        var userId = identity.FindFirst("UserId")?.Value;
        if (String.IsNullOrEmpty(userId)) return null;
        // Create the identity entity
        var identityEntity = new IdentityEntity()
        {
            UserName = userId,
        };
        return identityEntity;
    }
}