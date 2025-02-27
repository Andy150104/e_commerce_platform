using Client.Controllers.V1.MomoPayment;
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

namespace Client.Controllers.V1.OPS;

/// <summary>
/// OPSBuyingPlanController - Buying Plan
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class OPSBuyingPlanController : AbstractApiController<OPSBuyingPlanRequest, OPSBuyingPlanResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;
    private readonly IMomoService _momoService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    /// <param name="momoService"></param>
    public OPSBuyingPlanController(AppDbContext context, IIdentityApiClient identityApiClient, IMomoService momoService)
    {
        _context = context;
        _context._Logger = logger;
        _identityApiClient = identityApiClient;
        _momoService = momoService;
    }
    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override OPSBuyingPlanResponse Post(OPSBuyingPlanRequest request)
    {
        return Post(request, _context, logger, new OPSBuyingPlanResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override OPSBuyingPlanResponse Exec(OPSBuyingPlanRequest request, IDbContextTransaction transaction)
    {
        var response = new OPSBuyingPlanResponse() { Success = false };
        
        // Get userName
        var username = _context.IdentityEntity.UserName;
        
        Voucher voucher;
        
        // Get Plan
        var plan = _context.Plans.AsNoTracking().FirstOrDefault(p => p.PlanId == request.PlanId);
        if (plan == null)
        {
            response.SetMessage(MessageId.I00000, CommonMessages.PlanNotFound);
            return response;
        }
        
        var orderPlan = new OrderPlan
        {
            PlanId = request.PlanId,
            Username = username,
            Price = plan.Price,
            Status = (byte) ConstantEnum.OrderPlans.Pending
        };
        
        if (request.VoucherId != null)
        {
            voucher = _context.Vouchers.AsNoTracking().FirstOrDefault(v => v.VoucherId == request.VoucherId);
            if (voucher == null)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.VoucherNotFound);
                return response;
            }
            orderPlan.VoucherId = voucher.VoucherId;
            orderPlan.Price = plan.Price - voucher.UnitPrice;
        }
        
        // Create Payment
        var res = _momoService.CreatePaymentAsync(new MomoExecuteResponseModel
        {
            FullName = $"{orderPlan.OrderId}_{username}",
            Amount = Math.Round(orderPlan.Price * 100, 0).ToString(),
            OrderId = orderPlan.OrderId.ToString(),
            OrderInfo = plan.Description
        }).Result;

        // Add OrderPlan
        _context.OrderPlans.Add(orderPlan);
        _context.SaveChanges(username);
        transaction.Commit();

        // True
        response.Success = true;
        response.Response = res.PayUrl;
        return response;
    }
    
    /// <summary>
    /// Error check
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
        
        // True
        response.Success = true;
        return response;
    }
}

