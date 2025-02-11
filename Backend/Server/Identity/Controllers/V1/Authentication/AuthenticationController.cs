using System.Security.Claims;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Server.Controllers;
using Server.Helpers;
using Server.Models;
using Server.Models.Helper;
using Server.Utils.Consts;

namespace Server.Identity.Controllers;

/// <summary>
/// Authentication controller - Exchange token
/// </summary>
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManage;
    private readonly IOpenIddictScopeManager _scopeManager;
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="scopeManager"></param>
    /// <param name="context"></param>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    public AuthenticationController(IOpenIddictScopeManager scopeManager, AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _scopeManager = scopeManager;
        _context = context;
        _userManager = userManager;
        _signInManage = signInManager;
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
        // Check user exists
        var userExist = _context.VwUserLogins.AsNoTracking().FirstOrDefault(x => x.UserName == request.UserNameOrEmail || x.Email == request.UserNameOrEmail);
        if (userExist == null)
        {
            return BadRequest(new OpenIddictResponse
            {
                Error = OpenIddictConstants.Errors.InvalidGrant,
                ErrorDescription = "The username or password is incorrect."
            });
        }
        // If user exists
        var user = await _userManager.FindByIdAsync(userExist.UserId);
        // Check password
        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            user.AccessFailedCount++;
            if (user.AccessFailedCount >= 5)
            {
                user.LockoutEnd = DateTime.UtcNow.AddMinutes(30);
            }
            await _userManager.UpdateAsync(user);
            return BadRequest(new OpenIddictResponse
            {
                Error = OpenIddictConstants.Errors.InvalidGrant,
                ErrorDescription = "The username or password is incorrect."
            });
        }
        // Check lockout
        if (user.LockoutEnd != null && user.LockoutEnd > DateTime.UtcNow)
        {
            return Unauthorized(new OpenIddictResponse
            {
                ErrorDescription = "Your account has been locked."
            });
        }
        // Check user is active
        if (user.LockoutEnabled)
        {
            return Unauthorized();
        }
        
        user.AccessFailedCount = 0;
        user.LockoutEnd = null;
        await _context.SaveChangesAsync();
        
        var identity = new ClaimsIdentity(
            TokenValidationParameters.DefaultAuthenticationType,
            OpenIddictConstants.Claims.Name,
            OpenIddictConstants.Claims.Role
        );
        
        identity.SetClaim(OpenIddictConstants.Claims.Subject, userExist.UserId.ToString(), OpenIddictConstants.Destinations.AccessToken);
        identity.SetClaim(OpenIddictConstants.Claims.Name, userExist.UserName, OpenIddictConstants.Destinations.AccessToken);
        identity.SetClaim("UserId", request.UserNameOrEmail, OpenIddictConstants.Destinations.AccessToken);
        identity.SetClaim(OpenIddictConstants.Claims.Email, userExist.Email, OpenIddictConstants.Destinations.AccessToken);
        identity.SetClaim(OpenIddictConstants.Claims.Role, userExist.RoleName, OpenIddictConstants.Destinations.AccessToken);        
        identity.SetClaim(OpenIddictConstants.Claims.Audience, "service_client", OpenIddictConstants.Destinations.AccessToken);        
        
        identity.SetDestinations(claim =>
        {
            return claim.Type switch
            {
                OpenIddictConstants.Claims.Subject => new[] { OpenIddictConstants.Destinations.AccessToken },
                OpenIddictConstants.Claims.Name => new[] { OpenIddictConstants.Destinations.AccessToken },
                "UserId" => new[] { OpenIddictConstants.Destinations.AccessToken },
                OpenIddictConstants.Claims.Email => new[] { OpenIddictConstants.Destinations.AccessToken },
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
        
        claimsPrincipal.SetResources(await _scopeManager.ListResourcesAsync(claimsPrincipal.GetScopes()).ToListAsync());

        // Set refresh token and access token
        claimsPrincipal.SetAccessTokenLifetime(TimeSpan.FromMinutes(60));
        claimsPrincipal.SetRefreshTokenLifetime(TimeSpan.FromMinutes(120));

        return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }
}