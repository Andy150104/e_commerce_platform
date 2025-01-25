using System.Security.Claims;

namespace Client.SystemClient;

public class IdentityApiClient : IIdentityApiClient
{
    public IdentityEntity GetIdentity(ClaimsPrincipal user)
    {
        var identity = user.Identity as ClaimsIdentity;
        foreach (var claim in identity.Claims)
        {
            Console.WriteLine($"{claim.Type}: {claim.Value}");
        }
        var userNm = identity.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userNm)) 
            return null;

        var identityEntity = new IdentityEntity()
        {
            UserName = userNm,
        };
        return identityEntity;
    }
}