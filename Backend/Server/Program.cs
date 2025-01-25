using System.Net;
using Server.Models.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSwag;
using NSwag.Generation.Processors.Security;
using OpenIddict.Abstractions;
using Server;
using Server.Identity;
using Server.Identity.Models;
using Server.SystemClient;
using Role = Server.Identity.Models.Role;

var builder = WebApplication.CreateBuilder(args);

// 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDataProtection();

builder.Services.AddDbContext<AppDbContext>(options =>
{ 
    options.UseSqlServer(connectionString);
    options.UseOpenIddict();
});

builder.Services.AddScoped<IIdentityApiClient, IdentityApiClient>();
// Add services to the container.
builder.Services.AddControllers();
// Swagger configuration to output API type definitions
builder.Services.AddSwaggerDocument(config =>
{
    config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
    config.AddSecurity("JWT Token", Enumerable.Empty<string>(),
        new OpenApiSecurityScheme()
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = nameof(Authorization),
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Copy this into the value field: Bearer {token}"
        }
    );
});

// Allow API to be read from outside
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
});

// Use AppDbContext that inherits DB context
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseOpenIddict();
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
    options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
    options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
});

// Configure the OpenIddict server
builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
            .UseDbContext<AppDbContext>();
    })
    .AddServer(options =>
    {
        // Enable the required endpoints
        options.SetTokenEndpointUris("/connect/token");
        options.SetIntrospectionEndpointUris("/connect/introspect");
        options.SetUserInfoEndpointUris("/connect/userinfo");
        options.SetEndSessionEndpointUris("/connect/logout");
        // Enable the client credentials flow
        options.AllowPasswordFlow();
        options.AllowRefreshTokenFlow();
        options.AllowClientCredentialsFlow();
        options.AllowCustomFlow("logout");
        // Register the signing and encryption credentials
        options.UseReferenceAccessTokens();
        options.UseReferenceRefreshTokens();
        options.DisableAccessTokenEncryption();
        // Register your scopes
        options.RegisterScopes(
                OpenIddictConstants.Permissions.Scopes.Email,
                OpenIddictConstants.Permissions.Scopes.Profile,
                OpenIddictConstants.Permissions.Scopes.Roles);
        // Register the encryption credentials
        options.AddDevelopmentEncryptionCertificate()
               .AddDevelopmentSigningCertificate();      
        // Set the lifetime of the tokens
        options.SetAccessTokenLifetime(TimeSpan.FromMinutes(60));
        options.SetRefreshTokenLifetime(TimeSpan.FromMinutes(120));
        // Register ASP.NET Core host and configure options
        options.UseAspNetCore()
            .EnableTokenEndpointPassthrough()
            .EnableEndSessionEndpointPassthrough()
            .DisableTransportSecurityRequirement();
    })
    .AddValidation(options =>
    {
        options.UseLocalServer();
        options.UseAspNetCore();
    });
// Add authentication services
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = OpenIddictConstants.Schemes.Bearer;
    options.DefaultChallengeScheme = OpenIddictConstants.Schemes.Bearer;
});
// Add the OpenIddict validation component
builder.Services.AddIdentity<Users, Role>()
    .AddSignInManager()
    .AddUserStore<UserStore>()
    .AddRoleStore<RoleStore>()
    .AddUserManager<UserManager<Users>>();
// DB context that inherits AppDbContext
builder.Services.AddHttpContextAccessor();
// ConfigureServices
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// Add the worker service
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.UseOpenApi();
app.UseSwaggerUi();
app.Run();