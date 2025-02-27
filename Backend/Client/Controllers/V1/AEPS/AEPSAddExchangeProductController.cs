using Client.Models;
using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.AEPS;

/// <summary>
/// AEPSAddExchangeProductController - Add Exchange Product
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class AEPSAddExchangeProductController : AbstractApiController<AEPSAddExchangeProductRequest,
    AEPSAddExchangeProductResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    public AEPSAddExchangeProductController(AppDbContext context, IIdentityApiClient identityApiClient)
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
    public override AEPSAddExchangeProductResponse Post(AEPSAddExchangeProductRequest request)
    {
        return Post(request, _context, logger, new AEPSAddExchangeProductResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override AEPSAddExchangeProductResponse Exec(AEPSAddExchangeProductRequest request, IDbContextTransaction transaction)
    {
        var response = new AEPSAddExchangeProductResponse() { Success = false };
        var userName = _context.IdentityEntity.UserName;
        var checkImage = ImageValidationService.ImageCheck(request.ImageUrls, _context).Result;

        var blindBox = new BlindBox
        {
            Username = userName
        };

        blindBox.ImagesBlindBoxes = request.ImageUrls
            .Select(i => new ImagesBlindBox
                { 
                    BlindBoxId = blindBox.BlindBoxId, 
                    ImageUrl = i 
                }).ToList();

        var exchange = new Exchange
        {
            BlindBoxId = blindBox.BlindBoxId
        };

        if (!checkImage)
        {
            exchange.Status = (byte) ConstantEnum.ExchangeStatus.Fail;
        }
        else
            exchange.Status = (byte) ConstantEnum.ExchangeStatus.PendingExchange;

        _context.BlindBoxs.Add(blindBox);
        _context.Exchanges.Add(exchange);
        _context.SaveChanges(userName);
        //True
        transaction.Commit();
        response.Success = true;
        response.SetMessage(MessageId.I00003);
        if (!checkImage)
        {
            response.SetMessage("Invalid Images");
        }

        return response;
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override AEPSAddExchangeProductResponse ErrorCheck(AEPSAddExchangeProductRequest request,
        List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new AEPSAddExchangeProductResponse() { Success = false };
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