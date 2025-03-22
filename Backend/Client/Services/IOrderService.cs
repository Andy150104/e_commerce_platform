using Client.Controllers.V1.Orders;
using Client.Controllers.V1.ThirdParty;
using Client.Controllers.V1.TOS;
using Client.Models;

namespace Client.Services;

public interface IOrderService : IBaseService<Order, Guid, VwOrder>
{
    Task<InsertOrderResponse> InsertOrder(InsertOrderRequest request, IIdentityService identityService);
    
    MomoOrderLogicReturnResponse UpdateOrderStatusBySystem(MomoOrderLogicReturnRequest request, IIdentityService identityService);
    
    SelectOrdersResponse SelectOrders(IIdentityService identityService);
    
    SelectOrderResponse SelectOrder(SelectOrderRequest request, IIdentityService identityService);
}