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
/// MPSInsertProductController - Insert Product
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class MPSInsertProductController : AbstractApiAsyncController<MPSInsertProductRequest, MPSInsertProductResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context; 
    private readonly CommonLogic.CloudinaryService _cloudinaryService;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public MPSInsertProductController(AppDbContext context, IIdentityApiClient identityApiClient, CommonLogic.CloudinaryService cloudinaryService)
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
    [Authorize(Roles = ConstRole.SaleEmployee + "," + ConstRole.Owner, 
        AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override async Task<MPSInsertProductResponse> Post(MPSInsertProductRequest request)
    {
        return await Post(request, _context, logger, new MPSInsertProductResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override async Task<MPSInsertProductResponse> Exec(MPSInsertProductRequest request, IDbContextTransaction transaction)
    {
        var response = new MPSInsertProductResponse() { Success = false };
        
        // Get userName
        var userName = _context.IdentityEntity.UserName;
        
        // Insert Product
        var product = new Product
        {
            ProductId = GenerateProductCode(),
            Name = request.Name,
            Price = request.Price,
            Description = request.Description,
            ShortDescription = request.ShortDescription,
            Quantity = request.Quantity,
            Discount = request.Discount,
            CategoryId = request.CategoryId,
        };
        _context.Products.Add(product);
        _context.SaveChanges(userName);

        // Insert Image
        var imageTasks = request.Images.Select(async image =>
        {
            var imageUrl = await _cloudinaryService.UploadImageAsync(image);
            return new Image
            {
                ProductId = product.ProductId,
                ImageUrl = imageUrl
            };
        });

        // Wait for all images to be uploaded
        var images = await Task.WhenAll(imageTasks);
        _context.Images.AddRange(images);

        // Save changes
        _context.SaveChanges(userName);
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
    protected internal override MPSInsertProductResponse ErrorCheck(MPSInsertProductRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new MPSInsertProductResponse() { Success = false };
        
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
    /// Generate ProductId
    /// </summary>
    /// <returns></returns>
    private string GenerateProductCode()
    {
        return "P00" + Guid.NewGuid().ToString("D").Substring(26).ToUpper();
    }

}