using Client.Controllers.V1.Orders;
using Client.Controllers.V1.TOS;
using Client.Models;

namespace Client.Services;

public interface IOrderService : IBaseService<Order, Guid, VwOrder>
{
    Task<InsertOrderResponse> InsertOrder(InsertOrderRequest request, IIdentityService identityService);
    
    void UpdateOrderStatus(Guid orderId, string userName);
}