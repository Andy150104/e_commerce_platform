using System.Security.Claims;
using Server.Models.Helper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Server.Helpers;

namespace server.Identity.Controllers;

/// <summary>
/// 
/// </summary>
public class AuthenticationController : ControllerBase
{
    private readonly IOpenIddictScopeManager _scopeManager;
    private readonly AppDbContext _context;

    /// <summary>
    ///  Constructor
    /// </summary>
    /// <param name="scopeManager"></param>
    /// <param name="context"></param>
    public AuthenticationController(IOpenIddictScopeManager scopeManager, AppDbContext context)
    {
        _scopeManager = scopeManager;
        _context = context;
    }

    /// <summary>
    ///  Exchange token
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("~/connect/token")]
    [Consumes("application/x-www-form-urlencoded")]
    [Produces("application/json")]
    public async Task<IActionResult> Exchange(AuthenticationRequest request)
    {
        var openIdRequest = HttpContext.GetOpenIddictServerRequest();

        // Password
        if (openIdRequest.IsPasswordGrantType())
        {
            return await TokensForPasswordGrantType(request);
        }

        // Refresh token
        if (openIdRequest.IsRefreshTokenGrantType())
        {
            var claimsPrincipal = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal;
            
            return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
        
        // Unsupported grant type
        return BadRequest(new OpenIddictResponse
        {
            Error = OpenIddictConstants.Errors.UnsupportedGrantType
        });
    }

    /// <summary>
    ///  Tokens for password grant type
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private async Task<IActionResult> TokensForPasswordGrantType(AuthenticationRequest request)
    {
        var user = await _context.Users
            .Include(x => x.Role)
            .Where(x => x.UserName == request.UserName)
            .Where(x => x.IsEnabled == true)
            .FirstOrDefaultAsync(); 

        if (user == null)
        {
            return Unauthorized();
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            if (user.AuthFailedCount >= 5)
            {
                user.IsEnabled = false;
                await _context.SaveChangesAsync();
                return Unauthorized();
            }
            else
            {
                user.AuthFailedCount += 1;
                user.LockDate = System.DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return this.StatusCode(423);
            }
        } 
        
        if (user.LockDate.HasValue && (DateTime.UtcNow - user.LockDate.Value).TotalMinutes > 30)
        {
            user.LockDate = null;
            user.AuthFailedCount = 0;
            await _context.SaveChangesAsync();
        }
        
        user.AuthFailedCount = 0;
        user.LockDate = null;
        await _context.SaveChangesAsync();
        
        var identity = new ClaimsIdentity(
            TokenValidationParameters.DefaultAuthenticationType,
            OpenIddictConstants.Claims.Name,
            OpenIddictConstants.Claims.Role
        );
        
        identity.SetClaim(OpenIddictConstants.Claims.Subject, user.Id.ToString(), OpenIddictConstants.Destinations.AccessToken);
        identity.SetClaim(OpenIddictConstants.Claims.Name, user.UserName, OpenIddictConstants.Destinations.AccessToken);
        identity.SetClaim("UserId", request.UserName, OpenIddictConstants.Destinations.AccessToken);
        identity.SetClaim(OpenIddictConstants.Claims.Role, user.Role.Name, OpenIddictConstants.Destinations.AccessToken);        
        identity.SetClaim(OpenIddictConstants.Claims.Audience, "service_client", OpenIddictConstants.Destinations.AccessToken);        
        
        identity.SetDestinations(claim =>
        {
            return claim.Type switch
            {
                OpenIddictConstants.Claims.Subject => new[] { OpenIddictConstants.Destinations.AccessToken },
                OpenIddictConstants.Claims.Name => new[] { OpenIddictConstants.Destinations.AccessToken },
                "UserId" => new[] { OpenIddictConstants.Destinations.AccessToken },
                OpenIddictConstants.Claims.Role => new[] { OpenIddictConstants.Destinations.AccessToken },
                OpenIddictConstants.Claims.Audience => new[] { OpenIddictConstants.Destinations.AccessToken },
                _ => new[] { OpenIddictConstants.Destinations.AccessToken }
            };
        });

        var claimsPrincipal = new ClaimsPrincipal(identity);
        claimsPrincipal.SetScopes(new string[]
        {
            OpenIddictConstants.Scopes.Roles,
            OpenIddictConstants.Scopes.OfflineAccess, 
            OpenIddictConstants.Scopes.Profile,
        });
        foreach (var claim in identity.Claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
        }
        claimsPrincipal.SetResources(await _scopeManager.ListResourcesAsync(claimsPrincipal.GetScopes()).ToListAsync());

        // Set refresh token and access token
        claimsPrincipal.SetAccessTokenLifetime(TimeSpan.FromMinutes(60));
        claimsPrincipal.SetRefreshTokenLifetime(TimeSpan.FromMinutes(120));

        return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }
}