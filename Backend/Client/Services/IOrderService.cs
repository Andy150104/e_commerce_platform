using Client.Models;

namespace Client.Services;

public interface IOrderService : IBaseService<Order, Guid, VwOrder>
{
    
}