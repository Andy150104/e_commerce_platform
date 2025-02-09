using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Server.Helpers;
using Server.Models;
using Server.Utils.Consts;

namespace Server.Identity.Controllers;

/// <summary>
/// GoogleLoginControlller - Login with Google
/// </summary>
public class GoogleLoginControlller : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IOpenIddictScopeManager _scopeManager;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    /// <param name="roleManager"></param>
    /// <param name="scopeManager"></param>
    public GoogleLoginControlller(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, IOpenIddictScopeManager scopeManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _scopeManager = scopeManager;
    }
    
    /// <summary>
    /// Login with Google
    /// </summary>
    /// <returns></returns>
    [HttpGet("LoginWithGoogle")]
    public IActionResult LoginWithGoogle()
    {
        var redirectUrl = Url.Action("GoogleResponse", "GoogleLoginControlller", null, Request.Scheme);
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(GoogleDefaults.AuthenticationScheme, redirectUrl);
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }
    
    /// <summary>
    /// GoogleResponse
    /// </summary>
    /// <returns></returns>
    [HttpGet("GoogleResponse")]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!result.Succeeded)
            return BadRequest("Lỗi đăng nhập Google");

        var userData = new
        {
            Name = result.Principal.FindFirstValue(ClaimTypes.Name),
            Email = result.Principal.FindFirstValue(ClaimTypes.Email),
            GoogleId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier),
            Avatar = result.Principal.FindFirstValue("urn:google:picture"),
            RawClaims = result.Principal.Claims.Select(c => new { c.Type, c.Value }) // Toàn bộ thông tin
        };

        return Ok(userData);
        // var info = await _signInManager.GetExternalLoginInfoAsync();
        // if (info == null)
        //     return Unauthorized();
        //
        // var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        // foreach (var claim in info.Principal.Claims)
        // {
        //     Console.WriteLine(claim.Type + " : " + claim.Value);
        // }
        // var user = await _userManager.FindByEmailAsync(email);
        // if (user == null)
        // {
        //     // Create new user
        //     var role = await _roleManager.FindByNameAsync(ConstantEnum.UserRole.CUSTOMER.ToString());
        //
        //     user = new User
        //     {
        //         UserName = email,
        //         Email = email,
        //         RoleId = role.Id,
        //         IsActive = true,
        //         LockoutEnd = null,
        //         EmailConfirmed = true,
        //         LockoutEnabled = false,
        //     };
        //
        //     var result = await _userManager.CreateAsync(user);
        //     if (!result.Succeeded)
        //         return BadRequest(result.Errors);
        //
        //     await _userManager.AddLoginAsync(user, info);
        // }
        //
        // // Create JWT token
        // var identity = new ClaimsIdentity(
        //     TokenValidationParameters.DefaultAuthenticationType,
        //     OpenIddictConstants.Claims.Name,
        //     OpenIddictConstants.Claims.Role
        // );
        //
        // identity.SetClaim(OpenIddictConstants.Claims.Subject, user.Id, OpenIddictConstants.Destinations.AccessToken);
        // identity.SetClaim(OpenIddictConstants.Claims.Name, user.UserName, OpenIddictConstants.Destinations.AccessToken);
        // identity.SetClaim("UserId", user.UserName, OpenIddictConstants.Destinations.AccessToken);
        // identity.SetClaim(OpenIddictConstants.Claims.Email, user.Email, OpenIddictConstants.Destinations.AccessToken);
        // identity.SetClaim(OpenIddictConstants.Claims.Role, user.RoleId, OpenIddictConstants.Destinations.AccessToken);        
        // identity.SetClaim(OpenIddictConstants.Claims.Audience, "service_client", OpenIddictConstants.Destinations.AccessToken);        
        //
        // identity.SetDestinations(claim =>
        // {
        //     return claim.Type switch
        //     {
        //         OpenIddictConstants.Claims.Subject => new[] { OpenIddictConstants.Destinations.AccessToken },
        //         OpenIddictConstants.Claims.Name => new[] { OpenIddictConstants.Destinations.AccessToken },
        //         "UserId" => new[] { OpenIddictConstants.Destinations.AccessToken },
        //         OpenIddictConstants.Claims.Email => new[] { OpenIddictConstants.Destinations.AccessToken },
        //         OpenIddictConstants.Claims.Role => new[] { OpenIddictConstants.Destinations.AccessToken },
        //         OpenIddictConstants.Claims.Audience => new[] { OpenIddictConstants.Destinations.AccessToken },
        //         _ => new[] { OpenIddictConstants.Destinations.AccessToken }
        //     };
        // });
        //
        // var claimsPrincipal = new ClaimsPrincipal(identity);
        // claimsPrincipal.SetScopes(new string[]
        // {
        //     OpenIddictConstants.Scopes.Roles,
        //     OpenIddictConstants.Scopes.OfflineAccess, 
        //     OpenIddictConstants.Scopes.Profile,
        // });
        //
        // claimsPrincipal.SetResources(await _scopeManager.ListResourcesAsync(claimsPrincipal.GetScopes()).ToListAsync());
        //
        // // Set refresh token and access token
        // claimsPrincipal.SetAccessTokenLifetime(TimeSpan.FromMinutes(60));
        // claimsPrincipal.SetRefreshTokenLifetime(TimeSpan.FromMinutes(120));
        //
        // return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

}