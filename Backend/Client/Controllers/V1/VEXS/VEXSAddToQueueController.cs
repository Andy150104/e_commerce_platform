using Client.Models;
using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.VEXS;

/// <summary>
/// AEPSAddExchangeProductController - Add Exchange Product
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class VEXSAddToQueueController : AbstractApiController<VEXSAddToQueueRequest, VEXSAddToQueueResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public VEXSAddToQueueController(AppDbContext context, IIdentityApiClient identityApiClient)
    {
        _context = context;
        _context._Logger = logger;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override VEXSAddToQueueResponse Post(VEXSAddToQueueRequest request)
    {
        return Post(request, _context, logger, new VEXSAddToQueueResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override VEXSAddToQueueResponse Exec(VEXSAddToQueueRequest request, IDbContextTransaction transaction)
    {
        var response = new VEXSAddToQueueResponse() { Success = false };
        
        // Get userName
        var userName = _context.IdentityEntity.UserName;
        
        // Get blindBoxPost
        var blindBoxPost = _context.Exchanges.FirstOrDefault(b => b.BlindBoxId == request.BlindBoxId);
        if (blindBoxPost == null)
        {
            response.SetMessage(MessageId.I00000, CommonMessages.BlindBoxNotFound);
            return response;
        }
        
        var blindBox = new BlindBox
        {
            Username = userName,
        };
        _context.BlindBoxs.Add(blindBox);
        var queue = new Queue
        {
            Description = request.Description,
            ExchangeId = blindBoxPost.ExchangeId,
            Status = (byte) ConstantEnum.ExchangeStatus.PendingExchange,
        };
        _context.Queues.Add(queue);
        
        // Save
        _context.SaveChanges(userName);
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
    protected internal override VEXSAddToQueueResponse ErrorCheck(VEXSAddToQueueRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new VEXSAddToQueueResponse() { Success = false };
        if (detailErrorList.Count > 0)
        {
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }
        
        // True
        response.Success = true;
        return response;
    }
}

