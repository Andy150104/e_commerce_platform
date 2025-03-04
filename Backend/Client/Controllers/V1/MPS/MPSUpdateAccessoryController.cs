using Client.Logics.Commons;
using Client.Models;
using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.MPS;

/// <summary>
/// MPSUpdateAccessoryController - Update Accessory
/// </summary>
[Route("api/v1/mps/[controller]")]
[ApiController]
public class
    MPSUpdateAccessoryController : AbstractApiAsyncController<MPSUpdateAccessoryRequest, MPSUpdateAccessoryResponse,
    string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;
    private readonly CommonLogic.CloudinaryService _cloudinaryService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    /// <param name="cloudinaryService"></param>
    public MPSUpdateAccessoryController(AppDbContext context, IIdentityApiClient identityApiClient,
        CommonLogic.CloudinaryService cloudinaryService)
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
        AuthenticationSchemes =
            OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override Task<MPSUpdateAccessoryResponse> Post(MPSUpdateAccessoryRequest request)
    {
        return Post(request, _context, logger, new MPSUpdateAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override async Task<MPSUpdateAccessoryResponse> Exec(MPSUpdateAccessoryRequest request,
        IDbContextTransaction transaction)
    {
        var response = new MPSUpdateAccessoryResponse() { Success = false };

        // Get userName
        var userName = _context.IdentityEntity.UserName;

        // Get Accessory
        var product = _context.Accessories.FirstOrDefault(x => x.AccessoryId == request.CodeAccessory && x.IsActive == true);
        if (request.Name != null)
        {
            product.Name = request.Name;
        }

        if (request.Price != 0)
        {
            product.Price = request.Price;
        }

        if (request.Description != null)
        {
            product.Description = request.Description;
        }

        if (request.ShortDescription != null)
        {
            product.ShortDescription = request.ShortDescription;
        }

        if (request.Quantity != 0)
        {
            product.Quantity = request.Quantity;
        }

        if (request.Discount != null)
        {
            product.Discount = request.Discount;
        }

        if (request.CategoryId != null)
        {
            product.CategoryId = request.CategoryId;
        }

        if (request.Images != null && request.ImageId.Count > 0)
        {
            // Get Image to Delete
            var imagesToDelete = await _context.Images
                .Where(img => request.ImageId.Contains(img.ImageId))
                .ToListAsync();
            
            foreach (var image in imagesToDelete)
            {
                _context.Images.Update(image);
            }
            // Delete old images
            await _context.SaveChangesAsync(userName, true);

            // Upload new images
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
        }

        _context.Accessories.Update(product);
        await _context.SaveChangesAsync(userName);
        transaction.Commit();

        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override MPSUpdateAccessoryResponse ErrorCheck(MPSUpdateAccessoryRequest request,
        List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new MPSUpdateAccessoryResponse() { Success = false };

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

        if (request.ImageId.Any())
        {
            foreach (var imgId in request.ImageId)
            {
                var imagesToDelete = _context.Images.FirstOrDefault(img => img.ImageId == imgId && img.IsActive == true);
                if (imagesToDelete == null)
                {
                    response.SetMessage(MessageId.I00000, $"{"Image Id: " + imagesToDelete} is not found");
                    response.DetailErrorList = detailErrorList;
                    return response;
                }
            }
        }

        // True
        response.Success = true;
        return response;
    }
}