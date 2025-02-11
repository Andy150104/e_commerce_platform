using System.Net;
using Client.Models.Helper;
using Microsoft.EntityFrameworkCore;
using NSwag;
using NSwag.Generation.Processors.Security;
using OpenIddict.Validation.AspNetCore;
using OpenApiSecurityScheme = NSwag.OpenApiSecurityScheme;
using Client.SystemClient;
using server.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IIdentityApiClient, IdentityApiClient>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseOpenIddict();
});

builder.Services.AddDbContext<BBExTradingFloorContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseOpenIddict();
});

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
    options.AddDefaultPolicy(
        builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
    );
});

// Use AppDbContext that inherits DB context
builder.Services.AddDbContext<AppDbContext>();

// Configure the authentication server for the API
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
});

// Configure the OpenIddict server
builder.Services.AddOpenIddict()
    .AddValidation(options =>
    {       
        options.SetIssuer("https://localhost:5090/");
        options.AddAudiences("service_client");
        
        options.UseIntrospection()
            .AddAudiences("service_client")
            .SetClientId("service_client")
            .SetClientSecret("SWD392-LamNN15-GROUP3-SPRING2025");
        
        options.UseSystemNetHttp();
        options.UseAspNetCore();
    });

// DB context that inherits AppDbContext
builder.Services.AddHttpContextAccessor();

// ConfigureServices
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

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