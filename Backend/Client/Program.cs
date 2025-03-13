using System.Net;
using Client;
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
using Client.Logics.Commons.GHNLogics;
using Client.Repositories;
using Client.Services;
using Client.Settings;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container (Repository)
builder.Services.AddScoped(typeof(IBaseRepository<,,>), typeof(BaseRepository<,,>));
builder.Services.AddScoped(typeof(IBaseService<, ,>), typeof(BaseService<, ,>));

builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();
builder.Services.AddScoped<ILogicCommonRepository, LogicCommonRepository>();

// Add services to the container (Service)
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IBaseService<Image, Guid, VwImageAccessory>, BaseService<Image, Guid, VwImageAccessory>>();
builder.Services.AddScoped<IBaseService<ImagesBlindBox, Guid, VwImageBlindBox>, BaseService<ImagesBlindBox, Guid, VwImageBlindBox>>();
builder.Services.AddScoped<IBaseService<WishlistItem, Guid, object>, BaseService<WishlistItem, Guid, object>>();
builder.Services.AddScoped<IBaseService<Voucher, Guid, object>, BaseService<Voucher, Guid, object>>();
builder.Services.AddScoped<IBaseService<RefundPlanRequest, Guid, object>, BaseService<RefundPlanRequest, Guid, object>>();
builder.Services.AddScoped<IBaseService<OrderPlan, Guid, object>, BaseService<OrderPlan, Guid, object>>();
builder.Services.AddScoped<IBaseService<Cart, Guid, VwCartDisplay>, BaseService<Cart, Guid, VwCartDisplay>>();


builder.Services.AddScoped<IWishListService, WishListService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IQueueService, QueueService>();
builder.Services.AddScoped<IExchangeService, ExchangeService>();
builder.Services.AddScoped<IBlindBoxService, BlindBoxService>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<IAccessoryService, AccessoryService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IRefundRequestOrderService, RefundRequestOrderServiceService>();

//Connect MOMO API (NOT CHANGE)
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoService>();
builder.Services.AddScoped<IGHNLogic, GHNLogics>();

builder.Services.AddScoped<IIdentityApiClient, IdentityApiClient>();

// Connect Cloudinary API
builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("Cloudinary"));
builder.Services.AddSingleton<CloudinaryLogic>();


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
    config.DocumentProcessors.Add(new OrderOperationsProcessor());
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