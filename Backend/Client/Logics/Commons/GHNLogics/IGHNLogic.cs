using Client.Models;

namespace Client.Logics.Commons.GHNLogics;

public interface IGHNLogic
{
    Task<CreateOrderGHNResponse> CreateOrderGHNAsync(List<OrderDetail> orderDetails, User user, Address address, List<KeyValuePair<string, string>> accessoryNames);
}