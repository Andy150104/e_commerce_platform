using Client.Controllers.V1.HS;
using Client.Controllers.V1.MomoPayment;
using Client.Controllers.V1.MomoPayment.MomoServices;
using Client.Controllers.V1.OnlinePaymentScreen;
using Client.Controllers.V1.OPS;
using Client.Models;
using Client.Repositories;
using Client.Utils.Consts;

namespace Client.Services;

public class PlanService : BaseService<Plan, Guid, VwPlan>, IPlanService
{
    private readonly IBaseService<OrderPlan, Guid, object> _orderPlanService;
    private readonly IBaseService<Voucher, Guid, object> _voucherService;
    private readonly IBaseService<RefundPlanRequest, Guid, object> _refundPlanService;
    private readonly IMomoService _momoService;


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="orderPlanService"></param>
    /// <param name="voucherService"></param>
    /// <param name="refundPlanService"></param>
    /// <param name="momoService"></param>
    public PlanService(IBaseRepository<Plan, Guid, VwPlan> repository,
        IBaseService<OrderPlan, Guid, object> orderPlanService,
        IBaseService<Voucher, Guid, object> voucherService, 
        IBaseService<RefundPlanRequest, Guid, object> refundPlanService, 
        IMomoService momoService) : base(repository)
    {
        _orderPlanService = orderPlanService;
        _voucherService = voucherService;
        _refundPlanService = refundPlanService;
        _momoService = momoService;
    }

    /// <summary>
    /// Select Plan
    /// </summary>
    /// <returns></returns>
    public HSShowPlanResponse SelectPlan()
    {
        var response = new HSShowPlanResponse() { Success = false };

        // Get plans
        var plans = Repository
            .FindView()
            .Select(x => new HSShowPlanEntity
            {
                PlanId = x.PlanId,
                PlanName = x.PlanName,
                Description = x.Description,
                Price = x.Price,
                DurationMonths = x.DurationMonths
            })
            .ToList();

        // True
        response.Success = true;
        response.Response = plans;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public OPSAddPlanRefundResponse AddPlanRefund(OPSAddPlanRefundRequest request, IIdentityService identityService)
    {
        var response = new OPSAddPlanRefundResponse() { Success = false };
        
        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            // Get userName
            var username = identityService.IdentityEntity.UserName;

            // Get OrderPlan
            var orderPlan = _orderPlanService.GetById(request.OrderPlanId);
            if (orderPlan == null)
            {
                response.SetMessage(MessageId.E11004);
                return;
            }

            // Get RefundPlanRequest
            var refundRequest = new RefundPlanRequest
            {
                RefundRequests = Guid.NewGuid(),
                Status = (byte)RefundRequestEnum.Pending,
                OrderPlanId = orderPlan.OrderId,
                Reason = request.Reason,
            };

            // Add RefundPlanRequest
            _refundPlanService.Add(refundRequest);
            Repository.SaveChanges(username);

            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });
        return response;
    }

    /// <summary>
    /// Buying Plan
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public OPSBuyingPlanResponse BuyingPlan(OPSBuyingPlanRequest request, IIdentityService identityService)
    {
        var response = new OPSBuyingPlanResponse() { Success = false };

        // Get userName
        var username = identityService.IdentityEntity.UserName;
        Repository.ExecuteInTransaction(() =>
        {

            // Get Plan
            var plan = Repository.GetById(request.PlanId);
            if (plan == null)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.PlanNotFound);
                return;
            }

            var orderPlan = new OrderPlan
            {
                OrderId = Guid.NewGuid(),
                PlanId = request.PlanId,
                Username = username,
                Price = plan.Price,
                Status = (byte)ConstantEnum.OrderPlans.Pending
            };

            // Create Payment
            var res = _momoService.CreatePaymentAsync(new MomoExecuteResponseModel
            {
                FullName = $"{orderPlan.OrderId}_{username}",
                Amount = ((int)orderPlan.Price).ToString(),
                OrderId = orderPlan.OrderId.ToString(),
                OrderInfo = plan.Description!
            }).Result;

            // Add OrderPlan
            _orderPlanService.Add(orderPlan);
            Repository.SaveChanges(username);

            // True
            response.Success = true;
            response.Response = res.PayUrl;
        });
        return response;
    }
}