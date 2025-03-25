using Client.Controllers.V1.RefundRequestOrders;
using Client.Models;

namespace Client.Services;

public interface IRefundRequestOrderService : IBaseService<RefundRequestsOrder, Guid, VwRefundRequestOrder>
{
    RROInsertRefundRequestOrderResponse InsertRefundRequestOrder(RROInsertRefundRequestOrderRequest request, IIdentityService identityService);
    
    RROUpdateRefundRequestOrderResponse UpdateRefundRequestOrder(RROUpdateRefundRequestOrderRequest request, IIdentityService identityService);
    
    RRODeleteRefundRequestOrderResponse DeleteRefundRequestOrder(RRODeleteRefundRequestOrderRequest request, IIdentityService identityService);
    
    RROSelectRefundRequestOrdersResponse SelectRefundRequestOrders();
    RROSelectRefundRequestOrdersResponse SelectRefundRequestOrders(IIdentityService identityService);
        
    RROSelectRefundRequestOrderResponse SelectRefundRequestOrder(RROSelectRefundRequestOrderRequest request);
    RROSelectRefundRequestOrderResponse SelectRefundRequestOrder(RROSelectRefundRequestOrderRequest request, IIdentityService identityService);
    
    RROApproveRefundRequestOrderResponse ApproveRefundRequestOrder(RROApproveRefundRequestOrderRequest request, IIdentityService identityService);
}