using Client.Controllers.V1.TOS;
using Client.Models;

namespace Client.Services;

public interface IOrderService : IBaseService<Order, Guid, VwOrder>
{
    InsertOrderResponse InsertOrder(InsertOrderRequest request, IIdentityService identityService);
}