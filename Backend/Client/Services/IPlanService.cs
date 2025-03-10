using Client.Controllers.V1.HS;
using Client.Controllers.V1.OnlinePaymentScreen;
using Client.Controllers.V1.OPS;
using Client.Models;

namespace Client.Services;

public interface IPlanService : IBaseService<Plan, Guid, VwPlan>
{
    HSShowPlanResponse SelectPlan();
    
    OPSAddPlanRefundResponse AddPlanRefund(OPSAddPlanRefundRequest request, IIdentityService identityService);
    
    OPSBuyingPlanResponse BuyingPlan(OPSBuyingPlanRequest request, IIdentityService identityService);
}