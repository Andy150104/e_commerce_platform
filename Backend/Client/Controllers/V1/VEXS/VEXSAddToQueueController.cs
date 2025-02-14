 using Client.Controllers;
using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using server.Controllers.V1.AEPS.OpenAI;
using server.Models;
using server.Utils.Consts;

namespace Client.Controllers.V1.ViewQueueExchangeScreen;
/// <summary>
/// AEPSAddExchangeProductController - Add Exchange Product
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class VEXSAddToQueueController : AbstractApiController<VEXSAddToQueueRequest, VEXSAddToQueueResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;
    public VEXSAddToQueueController(AppDbContext context, IIdentityApiClient identityApiClient)
    {
        _context = context;
        _context._Logger = logger;
        _identityApiClient = identityApiClient;
    }
    /// <summary>
    /// Coming Posh
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns
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
        var userName = _context.IdentityEntity.UserName;
        var checkImage = ImageValidationService.ImageCheck(request.ImageUrls, _context).Result;
        if(!checkImage)
        {
            response.SetMessage("Invalid Images");
            return response;
        }
        var blindBoxPost = _context.Exchanges.FirstOrDefault(b => b.BlindBoxId == request.BlindBoxId);
        if(blindBoxPost == null)
        {
            response.SetMessage(MessageId.E11004);
            return response;
        }
        var blindBox = new Client.Models.BlindBox
        {
            BlindBoxId = Guid.NewGuid(),
            Username = userName,
        };
        blindBox.ImagesBlindBoxes = request.ImageUrls.Select(i => new Client.Models.ImagesBlindBox { BlindBoxId = blindBox.BlindBoxId, ImageId = Guid.NewGuid(), ImageUrl = i }).ToList();

        var Queue = new Client.Models.Queue
        {
            BlindBoxId = blindBox.BlindBoxId,
            ExchangeId = blindBoxPost.ExchangeId,
            QueueId = Guid.NewGuid(),
            Status = "Active"
        };
        _context.BlindBoxs.Add(blindBox);
        _context.Queues.Add(Queue);
        _context.SaveChanges(userName);
        //True
        transaction.Commit();
        response.Success = true;
        response.SetMessage(MessageId.I00003);
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
        //true
        response.Success = true;
        return response;
    }
}

