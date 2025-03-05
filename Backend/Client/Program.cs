using System.Net;
using Client.Controllers.V1.MomoPayment;
using Client.Models;
using Client.Models.Helper;
using Microsoft.EntityFrameworkCore;
using NSwag;
using NSwag.Generation.Processors.Security;
using OpenIddict.Validation.AspNetCore;
using OpenApiSecurityScheme = NSwag.OpenApiSecurityScheme;
using Client.SystemClient;
using Client.Controllers.V1.MomoPayment.MomoServices;
using Client.Logics.Commons;
using Client.Settings;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Connect MOMO API (NOT CHANGE)
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoService>();
builder.Services.AddScoped<IIdentityApiClient, IdentityApiClient>();

// Connect Cloudinary API
builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("Cloudinary"));
builder.Services.AddSingleton<CommonLogic.CloudinaryService>();


// Add services to the container.
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
builder.Services.AddOpenApiDocument(config =>
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