using Client.Controllers.V1.DPS;
using Client.Models;

namespace Client.Services;

public interface ICartItemService : IBaseService<CartItem, Guid, VwCartDisplay>
{
    DPSInsertCartResponse InsertCartItem(DPSInsertCartRequest request, IIdentityService identityService);
    DPSDeleteCartItemReponse DeleteCartItem(DPSDeleteCartItemRequest request, IIdentityService identityService);
    
    DPSSelectCartItemResponse SelectCartItem(DPSSelectCartItemRequest request, IIdentityService identityService);
    
    DPSUpdateCartItemResponse UpdateCartItem(DPSUpdateCartItemRequest request, IIdentityService identityService);
}