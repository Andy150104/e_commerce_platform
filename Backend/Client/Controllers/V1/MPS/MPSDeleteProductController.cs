using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.MPS;

/// <summary>
/// MPSDeleteProductController - Delete Product
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class MPSDeleteProductController : AbstractApiController<MPSDeleteProductRequest, MPSDeleteProductResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public MPSDeleteProductController(AppDbContext context, IIdentityApiClient identityApiClient)
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
    [Authorize(Roles = ConstRole.SaleEmployee + "," + ConstRole.Owner, 
        AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override MPSDeleteProductResponse Post(MPSDeleteProductRequest request)
    {
       return Post(request, _context, logger, new MPSDeleteProductResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override MPSDeleteProductResponse Exec(MPSDeleteProductRequest request, IDbContextTransaction transaction)
    {
        var response = new MPSDeleteProductResponse() { Success = false };
        
        // Get Product
        var product = _context.Products.Find(request.CodeProduct);
        
        // Delete
        product.IsActive = false;
        _context.Products.Update(product);
        _context.SaveChanges(_context.IdentityEntity.UserName);
        
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
    protected internal override MPSDeleteProductResponse ErrorCheck(MPSDeleteProductRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new MPSDeleteProductResponse() { Success = false };
        
        var product = _context.VwProductDisplays.Find(request.CodeProduct);
        if (product == null)
        {
            // Error
            response.SetMessage(MessageId.I00000, CommonMessages.ProductNotFound);
            return response;
        }
        
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
}