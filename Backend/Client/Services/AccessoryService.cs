using Client.Controllers.V1.DPS;
using Client.Controllers.V1.MPS;
using Client.Logics.Commons;
using Client.Models;
using Client.Repositories;
using Client.Utils;
using Client.Utils.Consts;
using Microsoft.EntityFrameworkCore;

namespace Client.Services;

public class AccessoryService : BaseService<Accessory, string, VwAccessoryDisplay>, IAccessoryService
{
    private readonly IBaseService<Image, Guid, VwImageAccessory> _imageAccessoryService;
    private readonly CloudinaryLogic _cloudinaryLogic;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="imageAccessoryService"></param>
    public AccessoryService(IBaseRepository<Accessory, string, VwAccessoryDisplay> repository,
        IBaseService<Image, Guid, VwImageAccessory> imageAccessoryService,
        CloudinaryLogic cloudinaryLogic) : base(repository)
    {
        _imageAccessoryService = imageAccessoryService;
        _cloudinaryLogic = cloudinaryLogic;
    }

    /// <summary>
    /// Insert Accessory
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public async Task<MPSInsertAccessoryResponse> InsertAccessory(MPSInsertAccessoryRequest request,
        IIdentityService identityService)
    {
        var response = new MPSInsertAccessoryResponse() { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        // Begin transaction
        await Repository.ExecuteInTransactionAsync(async () =>
        {
            // Insert Accessory
            var product = new Accessory
            {
                AccessoryId = "P00" + Guid.NewGuid().ToString("D").Substring(26).ToUpper(),
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                ShortDescription = request.ShortDescription,
                Quantity = request.Quantity,
                Discount = request.Discount,
                CategoryId = request.CategoryId,
            };
            Repository.Add(product);
            await Repository.SaveChangesAsync(userName);

            // Upload Images in Parallel
            var uploadTasks = request.Images.Select(async image =>
            {
                var imageUrl = await _cloudinaryLogic.UploadImageAsync(image);
                return new Image
                {
                    AccessoryId = product.AccessoryId,
                    ImageUrl = imageUrl
                };
            }).ToList();
            var images = await Task.WhenAll(uploadTasks);

            // Insert Images
            foreach (var image in images)
            {
                _imageAccessoryService.Add(image);
            }

            // Save changes
            await SaveChangesAsync(userName);

            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });
        return response;
    }

    public async Task<MSPInsertImageAccessoryResponse> InsertImageAccessory(MSPInsertImageAccessoryRequest request, IIdentityService identityService)
    {
        var response = new MSPInsertImageAccessoryResponse() { Success = false };
       
        // Get userName
        var userName = identityService.IdentityEntity.UserName;
       
        // Begin transaction
        await Repository.ExecuteInTransactionAsync(async () =>
        {
            // Get Accessory
            var accessorySelect = Find(x => x.AccessoryId == request.CodeAccessory && x.IsActive == true).FirstOrDefault();
            if (accessorySelect == null)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.ProductNotFound);
                return;
            }
       
            // Add ImageList
            var imageTasks = request.Images.Select(async image =>
            {
                var imageUrl = await _cloudinaryLogic.UploadImageAsync(image);
                return new Image
                {
                    AccessoryId = accessorySelect.AccessoryId,
                    ImageUrl = imageUrl
                };
            });
       
            // Wait for all photos to finish uploading
            var newImages = await Task.WhenAll(imageTasks);

            // Add new images to the database
            foreach (var image in newImages)
            {
                await _imageAccessoryService.AddAsync(image);
            }

            await SaveChangesAsync(userName);
       
            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });
        
        return response;
    }

    /// <summary>
    /// Select Accessory
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public DPSSelectAccessoryResponse SelectAccessory(DPSSelectAccessoryRequest request)
    {
        var response = new DPSSelectAccessoryResponse { Success = false };

        // Get Accessory
        var productSelect = Repository
            .FindView(x => x.AccessoryId == request.CodeAccessory)
            .FirstOrDefault();

        if (productSelect == null)
        {
            response.SetMessage(MessageId.I00000, CommonMessages.ProductNotFound);
            return response;
        }

        // Get ImageList
        var imageList = _imageAccessoryService
            .FindView(x => x.AccessoryId == productSelect.AccessoryId)
            .Select(x => new DpsSelectAccessoryListImageUrl
            {
                ImageUrl = x.ImageUrl
            })
            .ToList();

        // Response
        var responseEntity = new DPSSelectAccessoryEntity
        {
            CodeAccessory = productSelect!.AccessoryId,
            NameAccessory = productSelect.AccessoryName,
            ShortDescription = productSelect.ShortDescription,
            Description = productSelect.Description,
            Price = StringUtil.ConvertToVND(productSelect.Price),
            Discount = StringUtil.ConvertToPercent(productSelect.Discount),
            SalePrice = (productSelect.Price * (1 - productSelect.Discount / 100)).ToString(),
            CreatedAt = productSelect.CreatedAt,
            AverageRating = productSelect.AverageRating,
            TotalReviews = productSelect.TotalReviews,
            ChildCategoryName = productSelect.ChildCategoryName,
            ParentCategoryName = productSelect.ParentCategoryName,
            ImageUrls = imageList,
        };

        // True
        response.Success = true;
        response.Response = responseEntity;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Select Accessories
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public MPSSelectAccessoriesResponse SelectAccessories(MPSSelectAccessoriesRequest request)
    {
        var response = new MPSSelectAccessoriesResponse { Success = false };
        var mpsSelectAccessoriesEntity = new List<MPSSelectAccessoriesEntity>();

        // Get Accessory
        var accessorySelects = FindView().ToList();

        foreach (var accessory in accessorySelects)
        {
            // Get Image Accessory
            var imageAccessory = _imageAccessoryService
                .FindView(img => img.AccessoryId == accessory.AccessoryId)
                .Select(x => new MPSSelectImageAccessories
                {
                    AccessoryId = x.AccessoryId,
                    ImageId = x.ImageId,
                    ImageUrl = x.ImageUrl,
                })
                .ToList();

            // Response
            var entity = new MPSSelectAccessoriesEntity
            {
                AccessoryId = accessory.AccessoryId,
                AccessoryName = accessory.AccessoryName,
                Price = accessory.Price,
                Description = accessory.Description,
                ShortDescription = accessory.ShortDescription,
                CreatedAt = accessory.UpdatedAt,
                Discount = accessory.Discount,
                Quantity = accessory.Quantity,
                AverageRating = accessory.AverageRating,
                TotalOrders = accessory.TotalOrders,
                TotalReviews = accessory.TotalReviews,
                TotalSold = accessory.TotalSold,
                ChildCategoryName = accessory.ChildCategoryName,
                ParentCategoryName = accessory.ParentCategoryName,
                ImageAccessoriesList = imageAccessory,
            };
            mpsSelectAccessoriesEntity.Add(entity);
        }

        // True
        response.Success = true;
        response.Response = mpsSelectAccessoriesEntity;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    public List<ItemEntity> SelectByAccessory(decimal? minPrice, decimal? maxPrice, string nameAccessory,
        string parentCategory,
        string childCategory, byte? sortBy)
    {
        var responseEntity = new List<ItemEntity>();

        // Select Accessories
        var productDisplays = Repository.FindView();

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

        // Find by name
        if (!string.IsNullOrEmpty(nameAccessory))
        {
            productDisplays = productDisplays.Where(p => p.AccessoryName.ToLower().Contains(nameAccessory));
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
        productDisplays = CommonLogic.ApplySorting(productDisplays, sortBy);

        var productList = productDisplays.ToList();
        foreach (var productDisplay in productList)
        {
            // Get image urls
            var imageUrls = _imageAccessoryService.FindView(x => x.AccessoryId == productDisplay.AccessoryId)
                .Select(x => new DpsSelectItemListImageUrl
                {
                    ImageUrl = x.ImageUrl
                })
                .ToList();

            // Create entity
            var entity = new ItemEntity
            {
                CodeProduct = productDisplay.AccessoryId,
                NameAccessory = productDisplay.AccessoryName,
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

    public async Task<MPSUpdateAccessoryResponse> UpdateAccessory(MPSUpdateAccessoryRequest request,
        IIdentityService identityService)
    {
        var response = new MPSUpdateAccessoryResponse() { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        // Begin transaction
        await Repository.ExecuteInTransactionAsync(async () =>
        {
            // Get Accessory
            var product = Find(x => x.AccessoryId == request.CodeAccessory && x.IsActive == true).FirstOrDefault();
            if (product == null)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.ProductNotFound);
                return;
            }

            if (request.Name != null)
            {
                product.Name = request.Name;
            }

            if (request.Price != 0)
            {
                product.Price = request.Price;
            }

            if (request.Description != null)
            {
                product.Description = request.Description;
            }

            if (request.ShortDescription != null)
            {
                product.ShortDescription = request.ShortDescription;
            }

            if (request.Quantity != 0)
            {
                product.Quantity = request.Quantity;
            }

            if (request.Discount != null)
            {
                product.Discount = request.Discount;
            }

            if (request.CategoryId != null)
            {
                product.CategoryId = request.CategoryId;
            }

            if (request.Images != null && request.ImageId.Count > 0)
            {
                var imagesToDelete = await _imageAccessoryService
                    .Find(img => request.ImageId.Contains(img.ImageId) && img.IsActive == true)
                    .ToListAsync();
                
                var missingImages = request.ImageId.Except(imagesToDelete.Select(img => img.ImageId)).ToList();
                if (missingImages.Any())
                {
                    response.SetMessage(MessageId.I00000, $"Image Ids not found: {string.Join(", ", missingImages)}");
                    return;
                }

                // Delete old images
                imagesToDelete.ForEach(image => _imageAccessoryService.Update(image));
                await SaveChangesAsync(userName, true);

                // Upload new images
                var imageTasks = request.Images.Select(async image =>
                {
                    var imageUrl = await _cloudinaryLogic.UploadImageAsync(image);
                    return new Image
                    {
                        AccessoryId = product.AccessoryId,
                        ImageUrl = imageUrl
                    };
                });

                // Wait for all images to be uploaded
                var images = await Task.WhenAll(imageTasks);
                foreach (var image in images)
                {
                    _imageAccessoryService.Add(image);
                }
            }

            Update(product);
            await SaveChangesAsync(userName);

            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });
        return response;
    }

    /// <summary>
    /// Delete Accessory
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public MPSDeleteAccessoryResponse DeleteAccessory(MPSDeleteAccessoryRequest request,
        IIdentityService identityService)
    {
        var response = new MPSDeleteAccessoryResponse() { Success = false };

        Repository.ExecuteInTransaction(() =>
        {
            // Get Accessory
            foreach (var code in request.CodeAccessory)
            {
                var product = Repository.Find(x => x.AccessoryId == code && x.IsActive == true)
                    .FirstOrDefault();
                if (product == null)
                {
                    // Error
                    response.SetMessage(MessageId.I00000, $"{"Accessory Id: " + code} is not found");
                    return;
                }

                Update(product);

                var accessoryImage = _imageAccessoryService.Find(x => x.AccessoryId == code);
                foreach (var image in accessoryImage)
                {
                    _imageAccessoryService.Update(image);
                }
            }

            SaveChanges(identityService.IdentityEntity.UserName, true);

            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });
        return response;
    }

    /// <summary>
    /// Delete Image
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public MPSDeleteImageAccessoryResponse DeleteImageAccessory(MPSDeleteImageAccessoryRequest request,
        IIdentityService identityService)
    {
        var response = new MPSDeleteImageAccessoryResponse { Success = false };
        Repository.ExecuteInTransaction(() =>
        {
            // Delete Image
            foreach (var imgId in request.ImageId)
            {
                var imagesToDelete = _imageAccessoryService.Find(img => img.ImageId == imgId && img.IsActive == true)
                    .FirstOrDefault();
                if (imagesToDelete == null)
                {
                    response.SetMessage(MessageId.I00000, $"{"Image Id: " + imagesToDelete} is not found");
                    return;
                }

                _imageAccessoryService.Update(imagesToDelete);
            }

            // Delete
            SaveChanges(identityService.IdentityEntity.UserName, true);

            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });
        return response;
    }
}