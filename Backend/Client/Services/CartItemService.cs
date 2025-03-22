using Client.Controllers.V1.DPS;
using Client.Models;
using Client.Repositories;
using Client.Utils.Consts;

namespace Client.Services;

public class CartItemService : BaseService<CartItem, Guid, VwCartDisplay>, ICartItemService
{
    private readonly IBaseService<Cart, Guid, VwCartDisplay> _cartService;
    private readonly IAccessoryService _accessoryService;
    private readonly IBaseService<Image, Guid, VwImageAccessory> _imageService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="cartService"></param>
    /// <param name="accessoryService"></param>
    /// <param name="imageService"></param>
    public CartItemService(IBaseRepository<CartItem, Guid, VwCartDisplay> repository,
        IBaseService<Cart, Guid, VwCartDisplay> cartService, IAccessoryService accessoryService, IBaseService<Image, Guid, VwImageAccessory> imageService) : base(repository)
    {
        _cartService = cartService;
        _accessoryService = accessoryService;
        _imageService = imageService;
    }

    /// <summary>
    /// Delete CartItem
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public DPSDeleteCartItemReponse DeleteCartItem(DPSDeleteCartItemRequest request, IIdentityService identityService)
    {
        var response = new DPSDeleteCartItemReponse { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            // Get Cart
            var cartSelect = _cartService.Find(x => x.Username == userName && x.IsActive == true).FirstOrDefault();

            // Get CartItem
            var cartItemSelect = Repository
                .Find(x => x.CartId == cartSelect.CartId
                           && x.AccessoryId == request.CodeAccessory
                           && x.IsActive == true)
                .FirstOrDefault();
            
            
            

            // Delete CartItem
            cartItemSelect.Quantity = 0;
            Repository.Update(cartItemSelect);
            Repository.SaveChanges(userName, true);

            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
            return true;
        });
        return response;
    }

    /// <summary>
    /// Select CartItem
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public DPSSelectCartItemResponse SelectCartItem(DPSSelectCartItemRequest request, IIdentityService identityService)
    {
        var response = new DPSSelectCartItemResponse { Success = false };
        
        // Get userName
        var userName = identityService.IdentityEntity.UserName;
        
        // Get Cart
        var cartSelects = Repository
            .FindView(x => x.CustomerUsername == userName)
            .ToList();
        
        // Get cart result
        var cartItemEntity = new List<DPSSelectCartItem>();
        var cartItemResponse = new DPSSelectCartItemEntity();

        foreach (var cart in cartSelects)
        {
            var imageSelect = _imageService
                .FindView(x => x.AccessoryId == cart.AccessoryId)
                .Select(x => new DPSSelectCartItemImages
                {
                    ImageUrl = x.ImageUrl
                })
                .ToList();

            var cartEntity = new DPSSelectCartItem
            {
                AccessoryId = cart.AccessoryId,
                Price = cart.Price,
                Quantity = cart.Quantity,
                AccessoryName = cart.AccessoryName,
                UnitPrice = cart.TotalPrice,
                Images = imageSelect,
                ShortDescription = cart.ShortDescription
            };
            cartItemEntity.Add(cartEntity);
            cartItemResponse.TotalPrice += cartEntity.UnitPrice ?? 0;
        }
        cartItemResponse.Items = cartItemEntity;
        
        // True
        response.Success = true;
        response.Response = cartItemResponse;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    public DPSUpdateCartItemResponse UpdateCartItem(DPSUpdateCartItemRequest request, IIdentityService identityService)
    {
        var response = new DPSUpdateCartItemResponse { Success = false };
        
        // Get userName
        var userName = identityService.IdentityEntity.UserName;
        
        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            // Get Cart
            var cartSelect = _cartService.Find(x => x.Username == userName && x.IsActive == true).FirstOrDefault();
        
            // Get CartItem
            var cartItemSelect = Repository.Find(x => x.CartId == cartSelect.CartId 
                                                      && x.IsActive == true
                                                      && x.AccessoryId == request.AccessoryId)
                .FirstOrDefault();
        
            // Update CartItem
            cartItemSelect.Quantity = request.Quantity;
            Repository.Update(cartItemSelect);
            Repository.SaveChanges(userName);
        
            // If Quantity is 0, delete CartItem
            if (request.Quantity <= 0)
            {
                cartItemSelect.Quantity = 0;
                Repository.Update(cartItemSelect);
                Repository.SaveChanges(userName, true);
            }
            
            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
            return true;
        });
        return response;
    }

    public DPSInsertCartResponse InsertCartItem(DPSInsertCartRequest request, IIdentityService identityService)
    {
        var response = new DPSInsertCartResponse() { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            // Get Cart
            var cartSelect = _cartService.Find(x => x.Username == userName && x.IsActive == true).FirstOrDefault();

            if (cartSelect == null)
            {
                var cart = new Cart
                {
                    Username = userName
                };
                _cartService.Add(cart);
                Repository.SaveChanges(userName);
                cartSelect = cart;
            }

            // Get Accessory
            var productSelect = _accessoryService
                .Find(x => x.AccessoryId == request.CodeAccessory && x.IsActive == true)
                .FirstOrDefault();
            
            if (productSelect == null)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.ProductNotFound);
                return true;
            }

            if (request.Quantity > productSelect?.Quantity)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.QuantityIsGreaterStock);
                return true;
            }

            // Get CartItem
            var cartItem = Repository.Find(item => item.AccessoryId == productSelect.AccessoryId 
                                                   && item.CartId == cartSelect.CartId
                                                   && item.IsActive == true)
                .FirstOrDefault();

            if (cartItem != null)
            {
                cartItem.Quantity += request.Quantity;
                Repository.Update(cartItem);
            }
            else
            {
                // Insert CartItem
                cartItem = new CartItem
                {
                    CartId = cartSelect.CartId,
                    AccessoryId = productSelect.AccessoryId,
                    Quantity = request.Quantity
                };
                Repository.Add(cartItem);
            }

            _cartService.Update(cartSelect);
            Repository.SaveChanges(userName);

            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
            return true;
        });
        return response;
    }
}