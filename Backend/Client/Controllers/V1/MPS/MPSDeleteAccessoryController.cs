using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.MPS;

/// <summary>
/// MPSDeleteAccessoryController - Delete Accessory
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class MPSDeleteAccessoryController : AbstractApiController<MPSDeleteAccessoryRequest, MPSDeleteAccessoryResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public MPSDeleteAccessoryController(AppDbContext context, IIdentityApiClient identityApiClient)
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
    public override MPSDeleteAccessoryResponse Post(MPSDeleteAccessoryRequest request)
    {
       return Post(request, _context, logger, new MPSDeleteAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override MPSDeleteAccessoryResponse Exec(MPSDeleteAccessoryRequest request, IDbContextTransaction transaction)
    {
        var response = new MPSDeleteAccessoryResponse() { Success = false };
        
        // Get Accessory
        foreach (var code in request.CodeAccessory)
        {
            var product = _context.Accessories.FirstOrDefault(x => x.AccessoryId == code && x.IsActive == true);
            _context.Accessories.Update(product);
        }
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
    protected internal override MPSDeleteAccessoryResponse ErrorCheck(MPSDeleteAccessoryRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new MPSDeleteAccessoryResponse() { Success = false };
        
        if (detailErrorList.Count > 0)
        {
            // Error
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }

        foreach (var code in request.CodeAccessory)
        {
            var product = _context.Accessories.FirstOrDefault(x => x.AccessoryId == code && x.IsActive == true);
            if (product == null)
            {
                // Error
                response.SetMessage(MessageId.I00000, $"{"Accessory Id: " + code} is not found");
                response.DetailErrorList = detailErrorList;
                return response;
            }
        }

        // True
        response.Success = true;
        return response;
    }
}