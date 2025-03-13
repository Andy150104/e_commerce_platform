using Client.Controllers.V1.MomoPayment;
using Client.Controllers.V1.MomoPayment.MomoServices;
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
    private readonly IGHNLogic _ghnLogic;
    public OrderService(IBaseRepository<Order, Guid, VwOrder> repository, IMomoService momoService, IAddressService addressService, IAccessoryService accessoryService, IGHNLogic ghnLogic) : base(repository)
    {
        _momoService = momoService;
        _addressService = addressService;
        _accessoryService = accessoryService;
        _ghnLogic = ghnLogic;
    }

    public InsertOrderResponse InsertOrder(InsertOrderRequest request, IIdentityService identityService)
    {
        var response = new InsertOrderResponse { Success = false };
        
        // Get userName
        var userName = identityService.IdentityEntity.UserName;
        
        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            var user = identityService.Find(u => u.UserName == userName).FirstOrDefault();
            
        var address = _addressService.Find(a => a.AddressId == request.AddressId && a.Username == userName && a.IsActive == true).FirstOrDefault();
        if (address == null)
        {
            response.SetMessage(MessageId.I00000, CommonMessages.AddressOfUserNotFound);
            return ;
        }
        
        // Insert new Order
        var newOrder = new Order
        {
            OrderId = Guid.NewGuid(),
            Username = userName,
            Status = (byte) ConstantEnum.OrderStatus.Pending,
            Quantity = request.OrderDetails.Count,
        };

        decimal totalPrice = 0;
        var listAccessoryName = new List<KeyValuePair<string, string>>();
        var orderDetailList = new List<OrderDetail>();
        foreach (var item in request.OrderDetails)
        {
            var accessory = _accessoryService.Find(a => a.AccessoryId == item.AccessoryId && a.IsActive == true).FirstOrDefault();
            if (accessory == null)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.ProductNotFound);
                return ;
            }

            if (item.Quantity > accessory.Quantity)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.QuantityIsGreaterStock);
                return ;
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
            
            totalPrice += orderDetail.UnitPrice;
            accessory.Quantity -= item.Quantity;
            _accessoryService.Update(accessory);            
        }

        // Update TotalPrice of Order
        newOrder.TotalPrice = totalPrice;
        
        // Save Changes
        Add(newOrder);
        SaveChanges(userName);
        
        var res = _momoService.CreatePaymentAsync(new MomoExecuteResponseModel
        {
            FullName = $"{newOrder.OrderId}_{userName}",
            Amount = Math.Round(totalPrice * 100, 0).ToString(),
            OrderId = newOrder.OrderId.ToString(),
            OrderInfo = $"{CommonMessages.OrderDescription}_{newOrder.OrderId}",
        }).Result;
        
        if (res == null)
        {
            response.SetMessage(MessageId.I00000, CommonMessages.PaymentFailed);
            return ;
        }
        
        _ghnLogic.CreateOrderGHNAsync(orderDetailList ,user, address, listAccessoryName).Wait();
        
        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        });
        return response;
    }
}