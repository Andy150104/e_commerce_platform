using Client.Models;
using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Client.Logics.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using server.Logics.Commons;

namespace Client.Controllers.V1.MPS;

/// <summary>
/// MPSInsertAccessoryController - Insert Accessory
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class MPSInsertAccessoryController : AbstractApiAsyncController<MPSInsertAccessoryRequest, MPSInsertAccessoryResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context; 
    private readonly CommonLogic.CloudinaryService _cloudinaryService;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public MPSInsertAccessoryController(AppDbContext context, IIdentityApiClient identityApiClient, CommonLogic.CloudinaryService cloudinaryService)
    {
        _context = context;
        _context._Logger = logger;
        _identityApiClient = identityApiClient;
        _cloudinaryService = cloudinaryService;
    }
    
    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = ConstRole.SaleEmployee + "," + ConstRole.Owner, 
        AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override async Task<MPSInsertAccessoryResponse> Post(MPSInsertAccessoryRequest request)
    {
        return await Post(request, _context, logger, new MPSInsertAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override async Task<MPSInsertAccessoryResponse> Exec(MPSInsertAccessoryRequest request, IDbContextTransaction transaction)
    {
        var response = new MPSInsertAccessoryResponse() { Success = false };
        
        // Get userName
        var userName = _context.IdentityEntity.UserName;
        
        // Insert Accessory
        var product = new Accessory
        {
            AccessoryId = GenerateAccessoryCode(),
            Name = request.Name,
            Price = request.Price,
            Description = request.Description,
            ShortDescription = request.ShortDescription,
            Quantity = request.Quantity,
            Discount = request.Discount,
            CategoryId = request.CategoryId,
        };
        _context.Accessories.Add(product);
        await _context.SaveChangesAsync(userName);
        
        // Insert Image
        var imageTasks = request.Images.Select(async image =>
        {
            var imageUrl = await _cloudinaryService.UploadImageAsync(image);
            return new Image
            {
                AccessoryId = product.AccessoryId,
                ImageUrl = imageUrl
            };
        });

        // Wait for all images to be uploaded
        var images = await Task.WhenAll(imageTasks);
        _context.Images.AddRange(images);

        // Save changes
        await _context.SaveChangesAsync(userName);
        transaction.CommitAsync();
        
        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override MPSInsertAccessoryResponse ErrorCheck(MPSInsertAccessoryRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new MPSInsertAccessoryResponse() { Success = false };
        
        if (detailErrorList.Count > 0)
        {
            // Error
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }
        // True
        response.Success = true;
        return response;
    }
    
    /// <summary>
    /// Generate Accessory Code
    /// </summary>
    /// <returns></returns>
    private string GenerateAccessoryCode()
    {
        return "P00" + Guid.NewGuid().ToString("D").Substring(26).ToUpper();
    }
}