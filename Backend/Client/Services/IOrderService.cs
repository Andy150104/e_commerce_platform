using Client.Controllers.V1.Orders;
using Client.Controllers.V1.ThirdParty;
using Client.Controllers.V1.TOS;
using Client.Models;
using MomoResponse = Client.Controllers.V1.TOS.MomoResponse;

namespace Client.Services;

public interface IOrderService : IBaseService<Order, Guid, VwOrder>
{
    Task<InsertOrderResponse> InsertOrder(InsertOrderRequest request, IIdentityService identityService);
    
    MomoOrderLogicReturnResponse UpdateOrderStatusBySystem(MomoOrderLogicReturnRequest request, IIdentityService identityService);
    
    SelectOrdersResponse SelectOrders(IIdentityService identityService);
    
    SelectOrderResponse SelectOrder(SelectOrderRequest request, IIdentityService identityService);
    
    Task<PaymentOrderCallbackResponse> PaymentOrderCallback(PaymentOrderCallbackRequest request, IIdentityService identityService);
    
    Task<TrackingGhnOrderResponse> TrackingOrderAsync(TrackingGhnOrderRequest trackingGhnOrderRequest, IIdentityService identityService);

}