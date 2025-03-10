using Client.Models;
using Client.Repositories;

namespace Client.Services;

public class OrderService : BaseService<Order, Guid, VwOrder>, IOrderService
{
    public OrderService(IBaseRepository<Order, Guid, VwOrder> repository) : base(repository)
    {
    }
}