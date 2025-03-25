using Client.Controllers;
using Client.Controllers.V1.Orders;
using Client.Models;
using Client.Services;

namespace Client.Logics.Commons.GHNLogics;

public interface IGHNLogic
{
    Task<CreateOrderGHNResponse> CreateOrderGHNAsync(List<OrderDetail> orderDetails, User user, Address address, List<KeyValuePair<string, string>> accessoryNames);
}