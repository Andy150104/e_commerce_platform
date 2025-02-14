//using Client.Controllers;
//using Client.Models;
//using Client.Models.Helper;
//using Client.Utils.Consts;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Storage;
//using NLog;
//using server.Models;

//namespace Client.Controllers.V1.DisplayProductScreenExchange;
///// <summary>
///// HSShowPlanController - Show Plans Home Screen
///// </summary>
//[Route("api/v1/[controller]")]
//[ApiController]
//public class DPSESAddToWishlistController : AbstractApiController<DPSESAddToWishlistRequest, DPSESAddToWishlistResponse, string>
//{
//    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
//    private readonly AppDbContext _context;
//    public DPSESAddToWishlistController(AppDbContext context)
//    {
//        _context = context;
//        _context._Logger = logger;
//    }
//    /// <summary>
//    /// Coming Posh
//    /// </summary>
//    /// <param name="request"></param>
//    /// <returns></returns
//    [HttpPost]
//    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
//    public override DPSESAddToWishlistResponse Post(DPSESAddToWishlistRequest request)
//    {
//        return Post(request, _context, logger, new DPSESAddToWishlistResponse());
//    }

//    /// <summary>
//    /// Main processing
//    /// </summary>
//    /// <param name="request"></param>
//    /// <param name="transaction"></param>
//    /// <returns></returns>
//    protected override DPSESAddToWishlistResponse Exec(DPSESAddToWishlistRequest request, IDbContextTransaction transaction)
//    {
//        var response = new DPSESAddToWishlistResponse() { Success = false };

//        var userName = _context.IdentityEntity.UserName;
//        if(request.Quantity <= 0 || request.Price <= 0)
//        {
//            response.SetMessage(MessageId.E11004);
//            return response;
//        }
//        var wishlist = _context.Wishlists.FirstOrDefault(w => w.Products.Any(p => p.Username == userName));

//        if (wishlist == null)
//        {
//            wishlist = new Models.Wishlist()
//            {
//                WishlistId = Guid.NewGuid()
//            };
//            _context.Wishlists.Add(wishlist);
//        }
//        var product = new Product
//        {
//            ProductId = Guid.NewGuid(),
//            Name = request.Name,
//            CategoryId = request.CategoryId,
//            Description = request.Description,
//            IsActive = false,
//            Username = userName,
//            WishlistId = wishlist.WishlistId
//        };

//        product.Images = request.ImageUrls.Select(url => new Images { ImageId = Guid.NewGuid(), ImageUrl = url, ProductId = product.ProductId}).ToList();

//        wishlist.Products.Add(product); 

//        _context.Products.Add(product); 
//        _context.SaveChanges(userName);
//        transaction.Commit();

//        response.Success = true;
//        response.SetMessage(MessageId.I00003);
//        return response;
//    }

//    /// <summary>
//    /// Error check
//    /// </summary>
//    /// <param name="request"></param>
//    /// <param name="detailErrorList"></param>
//    /// <param name="transaction"></param>
//    /// <returns></returns>
//    protected internal override DPSESAddToWishlistResponse ErrorCheck(DPSESAddToWishlistRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
//    {
//        var response = new DPSESAddToWishlistResponse() { Success = false };
//        if (detailErrorList.Count > 0)
//        {
//            response.SetMessage(MessageId.E10000);
//            response.DetailErrorList = detailErrorList;
//            return response;
//        }
//        //true
//        response.Success = true;
//        return response;
//    }
//}

