using Client.Models.Helper;
using Client.Utils;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.DPS;

/// <summary>
/// DPSSelectItemController - Select products
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSSelectItemController : AbstractApiControllerNotToken<DPSSelectItemRequest, DPSSelectItemResponse, DPSSelectItemEntity>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public DPSSelectItemController(AppDbContext context)
    {
        _context = context;
        _context._Logger = logger;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public override DPSSelectItemResponse Post(DPSSelectItemRequest request)
    {
        return Post(request, _context, logger, new DPSSelectItemResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override DPSSelectItemResponse Exec(DPSSelectItemRequest request, IDbContextTransaction transaction)
    {
        var response = new DPSSelectItemResponse() { Success = false };
        var responseEntity = new List<ItemEntity>();
        
        #region SelectItem
        
        // Get request
        decimal? minPrice = request.MinimumPrice.HasValue ? request.MinimumPrice.Value : null;
        decimal? maxPrice = request.MaximumPrice.HasValue ? request.MaximumPrice.Value : null;
        string parentCategory = request.ParentCategoryName?.Trim().ToLower();
        string childCategory = request.ChildCategoryName?.Trim().ToLower();
        
        request.CurrentPage = Math.Max(1, request.CurrentPage);
        request.PageSize = Math.Max(1, request.PageSize);
        
        // If search by product
        if (request.SearchBy == (byte) ConstantEnum.ProductType.Product)
        {
            // Select products
           responseEntity = SelectByProduct( minPrice, maxPrice, parentCategory, childCategory, request.SortBy);
        }
        
        // If search by blind box
        else if (request.SearchBy == (byte) ConstantEnum.ProductType.BlindBox)
        {
            responseEntity = SelectByBlindBox(request.SortBy);
        }

        // Total records
        var totalRecords = responseEntity.Count;
        
        // Paging
        var itemDisplay = responseEntity
            .Skip((request.CurrentPage - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();
        
        // Set response
        var entityReturn = new DPSSelectItemEntity
        {
            Items = itemDisplay,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling((double)totalRecords / request.PageSize)
        };
        
        #endregion
        
        // True
        response.Success = true;
        response.Response = entityReturn;
        return response;
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override DPSSelectItemResponse ErrorCheck(DPSSelectItemRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new DPSSelectItemResponse() { Success = false };

        if (detailErrorList.Count > 0)
        {
            // Error
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }
        
        // True
        response.Success = true;
        return response;
    }

    /// <summary>
    /// Select by product
    /// </summary>
    /// <param name="minPrice"></param>
    /// <param name="maxPrice"></param>
    /// <param name="sortBy"></param>
    /// <returns></returns>
    private List<ItemEntity> SelectByProduct(decimal? minPrice, decimal? maxPrice, 
        string parentCategory, string childCategory, byte? sortBy)
    {
        var responseEntity = new List<ItemEntity>();
        
        // Select Products
        var productDisplays = _context.VwProductDisplays.AsNoTracking();
        
        // Find by minimum price
        if (minPrice >= 0)
        {
            productDisplays = productDisplays.Where(p => p.Price >= minPrice);
        }
        // Find by maximum price
        if (maxPrice > 0)
        {
            productDisplays = productDisplays.Where(p => p.Price <= maxPrice);
        }
        // Find by category
        if (!string.IsNullOrEmpty(parentCategory))
        {
            productDisplays = productDisplays.Where(p => p.ParentCategoryName.ToLower().Contains(parentCategory));
        }
        // Find by subcategory
        if (!string.IsNullOrEmpty(childCategory))
        {
            productDisplays = productDisplays.Where(p => p.ChildCategoryName.ToLower().Contains(childCategory));
        }
        
        // Apply sorting
        productDisplays = ApplySorting(productDisplays, sortBy);

        var productList = productDisplays.ToList();
        foreach (var productDisplay in productList)
        {
            // Get image urls
            var imageUrls = _context.VwImageProducts
                .AsNoTracking()
                .Where(x => x.ProductId == productDisplay.ProductId)
                .Select(x => new DpsSelectItemListImageUrl
                {
                    ImageUrl = x.ImageUrl
                })
                .ToList();
            
            // Create entity
            var entity = new ItemEntity
            {
                CodeProduct = productDisplay.ProductId,
                NameProduct = productDisplay.ProductName,
                Description = productDisplay.Description,
                ShortDescription = productDisplay.ShortDescription,
                Price = StringUtil.ConvertToVND(productDisplay.Price),
                Discount = StringUtil.ConvertToPercent(productDisplay.Discount),
                SalePrice = (productDisplay.Price * (1 - productDisplay.Discount / 100)).ToString(),
                CreatedAt = productDisplay.UpdatedAt,
                ParentCategoryName = productDisplay.ParentCategoryName,
                ChildCategoryName = productDisplay.ChildCategoryName,
                ImageUrl = imageUrls,
                AverageRating = productDisplay.AverageRating,
                TotalReviews = productDisplay.TotalReviews,
            };
            responseEntity.Add(entity);
        }
        
        return responseEntity;
    }

    /// <summary>
    /// Select by blind box
    /// </summary>
    /// <param name="sortBy"></param>
    /// <returns></returns>
    private List<ItemEntity> SelectByBlindBox(byte? sortBy)
    {
        var responseEntity = new List<ItemEntity>();

        // Select Products
        var blindBoxDisplays = _context.VwBlindBoxDisplays.AsNoTracking();
        
        blindBoxDisplays = ApplySorting(blindBoxDisplays, sortBy);
        
        var blindBixList = blindBoxDisplays.ToList();
        foreach (var blindBox in blindBixList)
        {
            // Get image urls
            var imageUrls = _context.VwImageBlindBoxes
                .AsNoTracking()
                .Where(x => x.ImageId == blindBox.BlindBoxId)
                .Select(x => new DpsSelectItemListImageUrl
                {
                    ImageUrl = x.ImageUrl
                })
                .ToList();
            
            // Create entity
            var entity = new ItemEntity
            {
                CodeProduct = blindBox.BlindBoxId.ToString(),
                CreatedAt = blindBox.CreatedAt,
                FirstNameCreator = blindBox.FirstName,
                LastNameCreator = blindBox.LastName,
                ImageUrl = imageUrls,
                ExchangeId = blindBox.ExchangeId
            };
            responseEntity.Add(entity);
        }

        return responseEntity;
    }
    
    /// <summary>
    /// Apply sorting
    /// </summary>
    /// <param name="query"></param>
    /// <param name="sortBy"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private IQueryable<T> ApplySorting<T>(IQueryable<T> query, byte? sortBy) where T : class
    {
        if (!sortBy.HasValue)
            return query;

        return sortBy switch
        {
            (byte)ConstantEnum.Sort.IncreasingPrice => query.OrderBy(x => EF.Property<decimal>(x, "Price")),
            (byte)ConstantEnum.Sort.DecreasingPrice => query.OrderByDescending(x => EF.Property<decimal>(x, "Price")),
            (byte)ConstantEnum.Sort.Newest => query.OrderByDescending(x => EF.Property<DateTime>(x, "CreatedAt")),
            (byte)ConstantEnum.Sort.Oldest => query.OrderBy(x => EF.Property<DateTime>(x, "CreatedAt")),
            (byte)ConstantEnum.Sort.MostPopular => query.OrderByDescending(x =>
                Convert.ToDouble(EF.Property<int>(x, "TotalSold")) * 0.5 +
                Convert.ToDouble(EF.Property<int>(x, "TotalOrders")) * 0.3 +
                Convert.ToDouble(EF.Property<decimal>(x, "AverageRating")) * 0.2
            ),
            _ => query
        };
    }



}