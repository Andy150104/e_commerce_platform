using System.Security.Claims;
using Server.Identity;
using Server.Systemserver;

namespace Server.SystemClient;

public class IdentityApiClient : IIdentityApiClient
{
    public IdentityEntity GetIdentity(ClaimsPrincipal user)
    {
        var identity = user.Identity as ClaimsIdentity;
        foreach (var claim in identity.Claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
        }
        
        var userId = identity.FindFirst("UserId")?.Value;
        if (String.IsNullOrEmpty(userId)) return null;


        var identityEntity = new IdentityEntity()
        {
            UserName = userId,
        };
        return identityEntity;
    }
}