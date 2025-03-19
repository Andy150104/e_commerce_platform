using Client.Controllers.V1.AEPS;
using Client.Controllers.V1.VEXS;
using Client.Models;

namespace Client.Services;

public interface IQueueService : IBaseService<Queue, Guid, object>
{
    VEXSAddToQueueResponse AddToQueue(VEXSAddToQueueRequest request, IIdentityService identityService);
    VEXSGetToQueueResponse GetQueue(VEXSGetToQueueRequest request, IIdentityService identityService);
    VEXSApproveQueueResponse ApproveQueue(VEXSApproveQueueRequest request, IIdentityService identityService);
    VEXSGetOrderExchangeResponse GetOrderExchange(VEXSGetOrderExchangeRequest request, IIdentityService identityService);
}