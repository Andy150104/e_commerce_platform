using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.MPS;

/// <summary>
/// MPSDeleteImageAccessoryController - Delete image accessory
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class MPSDeleteImageAccessoryController : AbstractApiController<MPSDeleteImageAccessoryRequest,
    MPSDeleteImageAccessoryResponse, string>
{
    private readonly AppDbContext _context;
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public MPSDeleteImageAccessoryController(AppDbContext context, IIdentityApiClient identityApiClient)
    {
        _context = context;
        _context._Logger = logger;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = ConstRole.SaleEmployee + "," + ConstRole.Owner, 
        AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override MPSDeleteImageAccessoryResponse Post(MPSDeleteImageAccessoryRequest request)
    {
        return Post(request, _context, logger, new MPSDeleteImageAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override MPSDeleteImageAccessoryResponse Exec(MPSDeleteImageAccessoryRequest request, IDbContextTransaction transaction)
    {
        var response = new MPSDeleteImageAccessoryResponse { Success = false };
        
        // Delete Image
        var imagesToDelete = _context.Images
            .Where(img => request.ImageId.Contains(img.ImageId) && img.IsActive == true)
            .ToList();
        
        // Delete
        _context.Images.UpdateRange();
        _context.SaveChanges(_context.IdentityEntity.UserName, true);
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
    protected internal override MPSDeleteImageAccessoryResponse ErrorCheck(MPSDeleteImageAccessoryRequest request,
        List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new MPSDeleteImageAccessoryResponse() { Success = false };

        if (detailErrorList.Count > 0)
        {
            // Error
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }

        // Check image
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
        

        // True
        response.Success = true;
        return response;
    }
}