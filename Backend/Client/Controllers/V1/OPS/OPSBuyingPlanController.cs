using Client.Controllers;
using Client.Controllers.V1.MomoPayment.MomoServices;
using Client.Controllers.V1.OnlinePaymentScreen;
using Client.Models;
using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.controllers.v1.onlinepaymentscreen;
/// <summary>
/// opsbuyingplancontroller - buying plan
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class OPSBuyingPlanController : AbstractApiController<OPSBuyingPlanRequest, OPSBuyingPlanResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;
    private readonly IMomoService _momoservice;
    public OPSBuyingPlanController(AppDbContext context, IIdentityApiClient identityapiclient, IMomoService momoservice)
    {
        _context = context;
        _context._Logger = logger;
        _identityApiClient = identityapiclient;
        _momoservice = momoservice;
    }
    /// <summary>
    /// coming post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override OPSBuyingPlanResponse Post(OPSBuyingPlanRequest request)
    {
        return Post(request, _context, logger, new OPSBuyingPlanResponse());
    }

    /// <summary>
    /// main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override OPSBuyingPlanResponse Exec(OPSBuyingPlanRequest request, IDbContextTransaction transaction)
    {
        var response = new OPSBuyingPlanResponse() { Success = false };
        var username = _context.IdentityEntity.UserName;
        Voucher voucher;
        Plan plan = _context.Plans.AsNoTracking().FirstOrDefault(p => p.PlanId == request.PlanId);
        if (plan == null)
        {
            response.SetMessage(MessageId.E11004);
            return response;
        }
        var orderplan = new OrderPlan
        {
            OrderId = Guid.NewGuid(),
            PlanId = request.PlanId,
            Username = username,
            Price = plan.Price,
            Status = (byte)OrderPlansEnum.Pending
        };
        if (request.VoucherId != null)
        {
            voucher = _context.Vouchers.AsNoTracking().FirstOrDefault(v => v.VoucherId == request.VoucherId);
            if (voucher == null)
            {
                response.SetMessage(MessageId.E11004);
                return response;
            }
            orderplan.VoucherId = voucher.VoucherId;
            orderplan.Price = plan.Price - voucher.UnitPrice;
        }
        var res = _momoservice.CreatePaymentAsync(new Controllers.V1.MomoServices.MomoExecuteResponseModel
        {
            FullName = $"{orderplan.OrderId}_{username}",
            Amount = Math.Round(orderplan.Price * 100, 0).ToString(),
            OrderId = orderplan.OrderId.ToString(),
            OrderInfo = plan.Description
        }).Result;

        _context.OrderPlans.Add(orderplan);
        _context.SaveChanges(username);
        //true
        transaction.Commit();
        response.Success = true;
        response.Response = res.PayUrl;
        return response;
    }
    /// <summary>
    /// error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailerrorlist"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override OPSBuyingPlanResponse ErrorCheck(OPSBuyingPlanRequest request, List<DetailError> detailerrorlist, IDbContextTransaction transaction)
    {
        var response = new OPSBuyingPlanResponse() { Success = false };
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

