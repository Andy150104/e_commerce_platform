using Client.Controllers;
using Client.Controllers.V1.MomoPayment.MomoServices;
using Client.Controllers.V1.MomoServices;
using Client.Controllers.V1.OnlinePaymentScreen;
using Client.Models;
using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using NLog;

namespace Client.controllers.v1.OnlinePaymentScreen;
/// <summary>
/// opsbuyingplancontroller - buying plan
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class OPSRefundPlanController : AbstractApiController<OPSRefundPlanRequest, OPSRefundPlanResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;
    private readonly IMomoService _momoservice;
    public OPSRefundPlanController(AppDbContext context, IIdentityApiClient identityapiclient, IMomoService momoservice)
    {
        _context = context;
        _context._Logger = logger;
        _identityApiClient = identityapiclient;
        _momoservice = momoservice;
    }
    /// <summary>
    /// coming posh
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override OPSRefundPlanResponse Post(OPSRefundPlanRequest request)
    {
        return Post(request, _context, logger, new OPSRefundPlanResponse());
    }

    /// <summary>
    /// main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override OPSRefundPlanResponse Exec(OPSRefundPlanRequest request, IDbContextTransaction transaction)
    {
        var response = new OPSRefundPlanResponse() { Success = false };
        var username = _context.IdentityEntity.UserName;
        var refundRequest = _context.RefundPlanRequests.FirstOrDefault(rp => rp.RefundRequests == request.RefundRequestId);
        if (refundRequest == null)
        {
            response.SetMessage(MessageId.E11004);
            return response;
        }
        var orderPlan = _context.OrderPlans.FirstOrDefault(op => op.OrderId == refundRequest.OrderPlanId);
        if (orderPlan == null)
        {
            response.SetMessage(MessageId.E11004);
            return response;
        }
        var refundResponse = _momoservice.CreateRefundAsync(new Controllers.V1.MomoServices.MomoRefundRequest
        {
            OrderId = "",
            Amount = long.Parse(Math.Round(orderPlan.Price * 100, 0).ToString()),
            Description = refundRequest.Reason,
            Lang = "vi",
            RequestId = Guid.NewGuid().ToString(),
            PartnerCode = "MOMO",
            TransId = long.Parse(orderPlan.Description)

        }).Result;
        refundRequest.ResultCode = refundResponse.ResultCode;
        refundRequest.ResultResponse = refundResponse.Message;
        if (refundResponse.ResultCode == 0)
        {   
            refundRequest.Status = (byte)RefundRequestEnum.Accepted;
            orderPlan.Status = (byte)OrderPlansEnum.Refunded;
            _context.OrderPlans.Update(orderPlan);
        }
        else
        {
            refundRequest.Status = (byte)RefundRequestEnum.Denied;
            _context.RefundPlanRequests.Update(refundRequest);
            _context.SaveChanges(username);
            //false
            transaction.Commit();
            response.SetMessage(refundResponse.Message);
            return response;

        }
        _context.RefundPlanRequests.Update(refundRequest);
        _context.SaveChanges(username);
        //true
        transaction.Commit();
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        return response;
    }
    /// <summary>
    /// error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailerrorlist"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override OPSRefundPlanResponse ErrorCheck(OPSRefundPlanRequest request, List<DetailError> detailerrorlist, IDbContextTransaction transaction)
    {
        var response = new OPSRefundPlanResponse() { Success = false };
        if (detailerrorlist.Count > 0)
        {
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailerrorlist;
            return response;
        }
        //true
        response.Success = true;
        return response;
    }
}

