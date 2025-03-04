using Client.Logics.Commons;
using Client.Models;
using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.MPS;

/// <summary>
/// MPSInsertImageAccessoryController - Insert Image Accessory
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class MSPInsertImageAccessoryController : AbstractApiAsyncController<MSPInsertImageAccessoryRequest, MSPInsertImageAccessoryResponse, string>
{
    private readonly AppDbContext _context;
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly CommonLogic.CloudinaryService _cloudinaryService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public MSPInsertImageAccessoryController(AppDbContext context, IIdentityApiClient identityApiClient, CommonLogic.CloudinaryService cloudinaryService)
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
    public override async Task<MSPInsertImageAccessoryResponse> Post(MSPInsertImageAccessoryRequest request)
    {
        return await Post(request, _context, logger, new MSPInsertImageAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override async Task<MSPInsertImageAccessoryResponse> Exec(MSPInsertImageAccessoryRequest request, IDbContextTransaction transaction)
    {
       var response = new MSPInsertImageAccessoryResponse() { Success = false };
       
       // Get userName
       var userName = _context.IdentityEntity.UserName;
       
       // Get Accessory
       var accessorySelect = _context.Accessories.FirstOrDefault(x => x.AccessoryId == request.CodeAccessory && x.IsActive == true);
       
       // Add ImageList
       var imageTasks = request.Images.Select(async image =>
       {
           var imageUrl = await _cloudinaryService.UploadImageAsync(image);
           return new Image
           {
               AccessoryId = accessorySelect.AccessoryId,
               ImageUrl = imageUrl
           };
       });
       
       // Wait for all photos to finish uploading
       var newImages = await Task.WhenAll(imageTasks);

       // Add new images to the database
       await _context.Images.AddRangeAsync(newImages);

       await _context.SaveChangesAsync(userName);
       await transaction.CommitAsync();
       
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
    protected internal override MSPInsertImageAccessoryResponse ErrorCheck(MSPInsertImageAccessoryRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new MSPInsertImageAccessoryResponse() { Success = false };
        
        if (detailErrorList.Count > 0)
        {
            // Error
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }

        var productExist = _context.Accessories.FirstOrDefault(x => x.AccessoryId == request.CodeAccessory && x.IsActive == true);
        if (productExist == null)
        {
            response.SetMessage(MessageId.I00000, CommonMessages.ProductNotFound);
            response.DetailErrorList = detailErrorList;
            return response;
        }

        // True
        response.Success = true;
        return response;
    }
}