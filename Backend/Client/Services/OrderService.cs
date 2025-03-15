using Client.Controllers.V1.MomoPayment;
using Client.Controllers.V1.MomoPayment.MomoServices;
using Client.Controllers.V1.Orders;
using Client.Controllers.V1.TOS;
using Client.Logics.Commons.GHNLogics;
using Client.Models;
using Client.Repositories;
using Client.Utils.Consts;

namespace Client.Services;

public class OrderService : BaseService<Order, Guid, VwOrder>, IOrderService
{
    private readonly IMomoService _momoService;
    private readonly IAddressService _addressService;
    private readonly IAccessoryService _accessoryService;
    private readonly ICartItemService _cartItemService;
    private readonly IGHNLogic _ghnLogic;

    public OrderService(IBaseRepository<Order, Guid, VwOrder> repository, IMomoService momoService,
        IAddressService addressService, IAccessoryService accessoryService, IGHNLogic ghnLogic, ICartItemService cartItemService) : base(repository)
    {
        _momoService = momoService;
        _addressService = addressService;
        _accessoryService = accessoryService;
        _ghnLogic = ghnLogic;
        _cartItemService = cartItemService;
    }

    /// <summary>
    /// Insert Order
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public async Task<InsertOrderResponse> InsertOrder(InsertOrderRequest request, IIdentityService identityService)
    {
        var response = new InsertOrderResponse { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        // Begin transaction
        await Repository.ExecuteInTransactionAsync(async () =>
        {
            var user = identityService.Find(u => u.UserName == userName).FirstOrDefault();

            // Check Address of user
            var address = _addressService
                .Find(a => a.AddressId == request.AddressId && a.Username == userName && a.IsActive == true)
                .FirstOrDefault();
            
            if (address == null)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.AddressOfUserNotFound);
                return;
            }

            // Insert new Order
            var newOrder = new Order
            {
                OrderId = Guid.NewGuid(),
                Username = userName,
                Quantity = request.OrderDetails.Count,
            };

            decimal totalPrice = 0;
            var listAccessoryName = new List<KeyValuePair<string, string>>();
            var orderDetailList = new List<OrderDetail>();
            
            foreach (var item in request.OrderDetails)
            {
                // Check Accessory
                var accessory = _accessoryService
                    .Find(a => a.AccessoryId == item.AccessoryId && a.IsActive == true)
                    .FirstOrDefault();
                if (accessory == null)
                {
                    response.SetMessage(MessageId.I00000, CommonMessages.ProductNotFound);
                    return;
                }

                // Check Quantity of Accessory
                if (item.Quantity > accessory.Quantity)
                {
                    response.SetMessage(MessageId.I00000, CommonMessages.QuantityIsGreaterStock);
                    return;
                }

                // Insert new OrderDetail
                var orderDetail = new OrderDetail
                {
                    OrderId = newOrder.OrderId,
                    AccessoryId = accessory.AccessoryId,
                    Quantity = item.Quantity,
                    UnitPrice = accessory.Price * item.Quantity
                };

                listAccessoryName.Add(new KeyValuePair<string, string>(accessory.AccessoryId, accessory.Name));

                newOrder.OrderDetails.Add(orderDetail);
                orderDetailList.Add(orderDetail);

                // Calculate TotalPrice
                totalPrice += orderDetail.UnitPrice;
                
                // Update Quantity of Accessory
                accessory.Quantity -= item.Quantity;
                
                _accessoryService.Update(accessory);
            }
            
            // Update TotalPrice of Order
            newOrder.TotalPrice = totalPrice;
            
            // Delete CartItem
            foreach (var orderDetail in request.OrderDetails)
            {
                var cartItem = _cartItemService.Find(c => c.AccessoryId == orderDetail.AccessoryId && c.IsActive == true && c.Quantity == orderDetail.Quantity).FirstOrDefault();
                if (cartItem == null)
                {
                    response.SetMessage(MessageId.I00000, $"{CommonMessages.CartItemNotFound}/Invalid quantity with AccessoryId: {orderDetail.AccessoryId}");
                    return;
                }
                _cartItemService.Update(cartItem);
            }

            _cartItemService.SaveChanges(userName, true);

            if (request.PaymentMethod == (byte) ConstantEnum.PaymentMethod.Online)
            {
                var res = await _momoService.CreatePaymentOrderAsync(new MomoExecuteResponseModel
                {
                    FullName = $"{newOrder.OrderId}_{userName}",
                    Amount = Math.Round(totalPrice * 100, 0).ToString(),
                    OrderId = newOrder.OrderId.ToString(),
                    OrderInfo = $"{CommonMessages.OrderDescription}_{newOrder.OrderId}",
                });

                // If payment failed
                if (res == null)
                {
                    response.SetMessage(MessageId.I00000, CommonMessages.PaymentFailed);
                    return;
                }

                newOrder.Status = (byte)ConstantEnum.OrderStatus.Pending;

                // Set Response
                response.Response = new InsertOrderResponseEntity
                {
                    PaymentUrl = res.PayUrl,
                };
            }
            else
            {
                var ghn = await _ghnLogic.CreateOrderGHNAsync(orderDetailList, user, address, listAccessoryName);
                if (ghn.Code != 200)
                {
                    response.SetMessage(MessageId.I00000, CommonMessages.FailedToFetchGHNApi);
                    return;
                }

                newOrder.Status = (byte)ConstantEnum.OrderStatus.Processing;

                response.Response = new InsertOrderResponseEntity
                {
                    GHNCode = ghn.Data.OrderCode
                };
            }

            // Save Changes
            Add(newOrder);
            SaveChanges(userName);
            
            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });
        return response;
    }

    /// <summary>
    /// Update Order Status
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="userName"></param>
    public void UpdateOrderStatus(Guid orderId, string userName)
    {
        var order = Find(o => o.OrderId == orderId && o.Username == userName).FirstOrDefault();
        
        if (order == null) 
            return;
        
        order.Status = (byte) ConstantEnum.OrderStatus.Processing;
        Update(order);    
        SaveChanges(userName);
    }
    
}