using Client.Controllers.V1.Orders;
using Client.Controllers.V1.ThirdParty;
using Client.Controllers.V1.TOS;
using Client.Logics.Commons;
using Client.Logics.Commons.GHNLogics;
using Client.Logics.Commons.MomoLogics;
using Client.Models;
using Client.Repositories;
using Client.Utils.Consts;
using Microsoft.EntityFrameworkCore;
using MomoResponse = Client.Controllers.V1.TOS.MomoResponse;

namespace Client.Services;

public class OrderService : BaseService<Order, Guid, VwOrder>, IOrderService
{
    private readonly IMomoService _momoService;
    private readonly IAddressService _addressService;
    private readonly IAccessoryService _accessoryService;
    private readonly ICartItemService _cartItemService;
    private readonly IBaseService<OrderDetail, Guid, VwOrderDetailsWithProduct> _orderDetailService;
    private readonly IGHNLogic _ghnLogic;
    private readonly HttpClient _httpClient;
    private readonly static string _apiToken = "880e415a-fc97-11ef-82e7-a688a46b55a3";
    private readonly static string _shopId = "196113";

    private readonly static string _createOrderUrl = "https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/create";
    private readonly static string _trackingOrderUrl = "https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/detail";

    public OrderService(IBaseRepository<Order, Guid, VwOrder> repository, IMomoService momoService,
        IAddressService addressService, IAccessoryService accessoryService, IGHNLogic ghnLogic,
        ICartItemService cartItemService, IBaseService<OrderDetail, Guid, VwOrderDetailsWithProduct> orderDetailService, HttpClient httpClient) : base(repository)
    {
        _momoService = momoService;
        _addressService = addressService;
        _accessoryService = accessoryService;
        _ghnLogic = ghnLogic;
        _cartItemService = cartItemService;
        _orderDetailService = orderDetailService;
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("Token", _apiToken);
        _httpClient.DefaultRequestHeaders.Add("ShopId", _shopId);
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
                return false;
            }

            // Insert new Order
            var newOrder = new Order
            {
                OrderId = Guid.NewGuid(),
                Username = userName,
                AddressId = address.AddressId,
                Quantity = request.OrderDetails!.Count,
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
                    return false;
                }

                // Check Quantity of Accessory
                if (item.Quantity > accessory.Quantity)
                {
                    response.SetMessage(MessageId.I00000, CommonMessages.QuantityIsGreaterStock);
                    return false;
                }

                var accessoryPrice =
                    Math.Floor((accessory.Price - (accessory.Price * ((accessory.Discount ?? 0) / 100)))) *
                    item.Quantity;
                // Insert new OrderDetail
                var orderDetail = new OrderDetail
                {
                    OrderId = newOrder.OrderId,
                    AccessoryId = accessory.AccessoryId,
                    Quantity = item.Quantity,
                    UnitPrice = accessoryPrice
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

            SaveChanges(userName);
            // Update TotalPrice of Order
            newOrder.TotalPrice = totalPrice;

            // Delete CartItem
            foreach (var orderDetail in request.OrderDetails)
            {
                var cartItem = _cartItemService.Find(c =>
                    c.AccessoryId == orderDetail.AccessoryId && c.IsActive == true &&
                    c.Quantity == orderDetail.Quantity).FirstOrDefault();
                if (cartItem == null)
                {
                    response.SetMessage(MessageId.I00000,
                        $"{CommonMessages.CartItemNotFound}/Invalid quantity with AccessoryId: {orderDetail.AccessoryId}");
                    return false;
                }

                _cartItemService.Update(cartItem);
            }

            _cartItemService.SaveChanges(userName, true);

            if (request.PaymentMethod == (byte)ConstantEnum.PaymentMethod.Online)
            {
                var momoExcuteResponseModel = new MomoExecuteResponseModel
                {
                    FullName = $"{userName}",
                    Amount = ((int)newOrder.TotalPrice).ToString(),
                    OrderId = newOrder.OrderId.ToString(),
                    OrderInfo = $"{user.LastName}-{user.FirstName}_{CommonMessages.OrderDescription}_{newOrder.OrderId}",
                };

                var res = await _momoService.CreatePaymentOrderAsync(momoExcuteResponseModel, request.PlatForm,
                    request.AddressId);

                // If payment failed
                if (res.ErrorCode != (byte)ConstantEnum.PaymentStatus.Success || 
                    res.PayUrl == null ||
                    res.QrCodeUrl == null ||
                    res.DeeplinkWebInApp == null ||
                    res.Deeplink == null)
                {
                    response.SetMessage(MessageId.I00000, CommonMessages.PaymentFailed);
                    return false;
                }

                newOrder.Status = (byte)ConstantEnum.OrderStatus.Processing;

                var momoResponse = new MomoResponse
                {
                    PaymentUrl = res.PayUrl,
                    QrCodeUrl = res.QrCodeUrl,
                    DeeplinkWebInApp = res.DeeplinkWebInApp,
                    Deeplink = res.Deeplink,
                };
                
                if(momoResponse.QrCodeUrl == null || momoResponse.PaymentUrl == null || momoResponse.Deeplink == null || momoResponse.DeeplinkWebInApp == null)
                {
                    response.SetMessage(MessageId.I00000, CommonMessages.PaymentFailed);
                    return false;
                }

                // Set Response
                response.Response = new InsertOrderResponseEntity
                {
                    Momo = momoResponse,
                };
                var momoUrl = $"{momoResponse.PaymentUrl},{momoResponse.QrCodeUrl},{momoResponse.Deeplink},{momoResponse.DeeplinkWebInApp}";
                
                newOrder.MomoUrl = momoUrl;
            }
            else
            {
                var ghn = await _ghnLogic.CreateOrderGHNAsync(orderDetailList, user, address, listAccessoryName);
                if (ghn.Code != 200)
                {
                    response.SetMessage(MessageId.I00000, CommonMessages.FailedToFetchGHNApi);
                    return false;
                }

                newOrder.Status = (byte)ConstantEnum.OrderStatus.Success;

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
            return true;
        });
        return response;
    }

    /// <summary>
    /// Update Order Status By System
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public MomoOrderLogicReturnResponse UpdateOrderStatusBySystem(MomoOrderLogicReturnRequest request, IIdentityService identityService)
    {
        var response = new MomoOrderLogicReturnResponse { Success = false };
        // Get UserName
        var userName = identityService.IdentityEntity.UserName;
        
        Repository.ExecuteInTransaction(() =>
        {
            var order = Find(o => o.OrderId == request.OrderId && o.Username == userName && o.IsActive == true).FirstOrDefault();

            if (order == null)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.OrderNotFound);
                return false;
            }

            order.Status = (byte) ConstantEnum.OrderStatus.Success;
            order.GhnCode = request.GhnOrderCode;
            
            Update(order);
            SaveChanges("OrderSystem");

            response.Success = true;
            response.SetMessage(MessageId.I00001);
            return true;
        });
        return response;
    }

    /// <summary>
    /// Select Orders
    /// </summary>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public SelectOrdersResponse SelectOrders(IIdentityService identityService)
    {
        var response = new SelectOrdersResponse { Success = false };
        var responseOrder = new List<SelectOrdersEntity>();

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        // Get Orders
        var orders = FindView(order => order.Username == userName)
            .ToList()
            .OrderByDescending(o => o.CreatedAt);

        foreach (var order in orders)
        {
            // Select Order Details
            var orderDetailsList = _orderDetailService.FindView(o => o.Username == userName && o.OrderId == order.OrderId)
                .Select(x => new TOSSelectOrderDetails
                {
                    OrderDetailId = x.OrderDetailId,
                    AccessoryId = x.AccessoryId,
                    AccessoryName = x.ProductName,
                    Quantity = x.Quantity,
                    OriginalPrice = x.OriginalPrice,
                    UnitPrice = x.UnitPrice,
                    DiscountPercent = x.DiscountPercent ?? 0,
                    DiscountedPrice = x.DiscountedPrice ?? 0,
                })
                .ToList();
            
            // Add Order
            responseOrder.Add(new SelectOrdersEntity
            {
                OrderId = order.OrderId,
                Quantity = order.Quantity,
                TotalPrice = order.TotalPrice,
                Status = order.OrderStatus,
                OrderDetails = orderDetailsList
            });
        }
        
        // True
        response.Success = true;
        response.Response = responseOrder;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Select Order
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public SelectOrderResponse SelectOrder(SelectOrderRequest request, IIdentityService identityService)
    {
        var response = new SelectOrderResponse { Success = false };
        
        // Get userName
        var userName = identityService.IdentityEntity.UserName;
        
        // Get Order
        var order = FindView(o => o.OrderId == request.OrderId && o.Username == userName).FirstOrDefault();
        if (order == null)
        {
            response.SetMessage(MessageId.I00000, CommonMessages.OrderNotFound);
            return response;
        }
        
        // Get Order Details
        var orderDetailsList = _orderDetailService
            .FindView(o => o.Username == userName && o.OrderId == request.OrderId)
            .Select(x => new SelectOrderDetails
            {
                OrderDetailId = x.OrderDetailId,
                AccessoryId = x.AccessoryId,
                AccessoryName = x.ProductName,
                Quantity = x.Quantity,
                OriginalPrice = x.OriginalPrice,
                UnitPrice = x.UnitPrice,
                DiscountPercent = x.DiscountPercent ?? 0,
                DiscountedPrice = x.DiscountedPrice ?? 0,
            })
            .ToList();
        
        // Return Order
        var orderEntity = new SelectOrderEntity
        {
            OrderId = order.OrderId,
            Quantity = order.Quantity,
            TotalPrice = order.TotalPrice,
            Status = order.OrderStatus,
            CreatedAt = order.CreatedAt,
            CreatedBy = order.CreatedBy,
            UpdatedAt = order.UpdatedAt,
            UpdatedBy = order.UpdatedBy,
            OrderDetails = orderDetailsList
        };
        
        response.Success = true;
        response.Response = orderEntity;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    public async Task<PaymentOrderCallbackResponse> PaymentOrderCallback(PaymentOrderCallbackRequest request, IIdentityService identityService)
    {
        var response = new PaymentOrderCallbackResponse { Success = false };
        
        // Get userName
        var userName = identityService.IdentityEntity.UserName;
        
        // Get Order
        var order = Find(o => 
                o.OrderId == request.OrderId && 
                o.Username == userName &&
                o.Status == ((byte) ConstantEnum.OrderStatus.Processing) &&
                o.GhnCode == null &&
                o.IsActive == true,
                true,
                u => u.UsernameNavigation)
            .FirstOrDefault();

        if (order == null)
        {
            response.SetMessage(MessageId.I00000, CommonMessages.OrderNotFound);
            return response;
        }

        if (order.CreatedAt.Value.AddMinutes(100) > DateTime.Now)
        {
            var momoUrls = order.MomoUrl.Split(',');
            var momoResponse = new MomoResponse
            {
                PaymentUrl = momoUrls[0],
                QrCodeUrl = momoUrls[1],
                Deeplink = momoUrls[2],
                DeeplinkWebInApp = momoUrls[3],
            };
            response.Response = momoResponse;
        }
        else
        {
            Update(order);
            SaveChanges("OrderSystem", true);
            
            var newOrder = new Order
            {
                OrderId = Guid.NewGuid(),
                Username = userName,
                AddressId = order.AddressId,
                Quantity = order.Quantity,
                TotalPrice = order.TotalPrice,
                Status = (byte) ConstantEnum.OrderStatus.Processing,
            };
            
            // Execute Payment Order
            var momoExcuteResponseModel = new MomoExecuteResponseModel
            {
                FullName = $"{userName}",
                Amount = ((int)order.TotalPrice).ToString(),
                OrderId = newOrder.OrderId.ToString(),
                OrderInfo = $"{order.UsernameNavigation.LastName}-{order.UsernameNavigation.FirstName}_{CommonMessages.OrderDescription}_{order.OrderId}",
            };
        
            var res = await _momoService.CreatePaymentOrderAsync(momoExcuteResponseModel, request.Platform,
                order.AddressId ?? Guid.Empty);
        
            var momoResponse = new MomoResponse
            {
                PaymentUrl = res.PayUrl,
                QrCodeUrl = res.QrCodeUrl,
                DeeplinkWebInApp = res.DeeplinkWebInApp,
                Deeplink = res.Deeplink,
            };

            if(momoResponse.QrCodeUrl == null || momoResponse.PaymentUrl == null || momoResponse.Deeplink == null || momoResponse.DeeplinkWebInApp == null)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.PaymentFailed);
                return response;
            }
            
            var momoUrl = $"{momoResponse.PaymentUrl},{momoResponse.QrCodeUrl},{momoResponse.Deeplink},{momoResponse.DeeplinkWebInApp}";
            newOrder.MomoUrl = momoUrl;
            
            response.Response = momoResponse;
            Add(newOrder);
            SaveChanges(userName);
        }
        
        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        return response;
    }
    
    
    
    public async Task<TrackingGhnOrderResponse> TrackingOrderAsync(TrackingGhnOrderRequest trackingGhnOrderRequest, IIdentityService identityService)
    {
        var response = new TrackingGhnOrderResponse { Success = false };
        
        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        var ghnCode = Find(x => x.OrderId == trackingGhnOrderRequest.OrderId && x.Username == userName)
            .Select(x => x.GhnCode).FirstOrDefault();
        
        
        var apiClient = new CommonLogic.ApiClient<TrackingGhnOrderResponse, GhnOrderResponse>(_httpClient);
        
        var headers = new Dictionary<string, string>
        {
            {"Token", _apiToken},
            {"ShopId", _shopId}
        };
        
        var queryParams = new Dictionary<string, string>
        {
            { "order_code", ghnCode }
        };

        var apiResponse = await apiClient.CallApiAsync(HttpMethod.Get, _trackingOrderUrl, null, headers, queryParams);
        
        if (!apiResponse.Success)
        {
            response.MessageId = apiResponse.MessageId;
            response.Message = apiResponse.Message;
            return response;
        }

        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        response.Response = apiResponse.Response;
        return response;
    }
}