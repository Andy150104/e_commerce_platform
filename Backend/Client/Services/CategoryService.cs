using Client.Controllers.V1.Categories;
using Client.Models;
using Client.Repositories;
using Client.Utils.Consts;

namespace Client.Services;

public class CategoryService : BaseService<Category, Guid, VwCategoriesDisplay>, ICategoryService
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    public CategoryService(IBaseRepository<Category, Guid, VwCategoriesDisplay> repository) : base(repository)
    {
    }

    /// <summary>
    /// Create new category
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public CreateCategoryResponse CreateCategory(CreateCategoryRequest request, IIdentityService identityService)
    {
        var response = new CreateCategoryResponse { Success = false };
        Repository.ExecuteInTransaction(() =>
        {
            // Create category
            var category = new Category
            {
                CategoryName = request.CategoryName,
                ParentId = request.ParentId ?? null
            };
            Repository.Add(category);
            Repository.SaveChanges(identityService.IdentityEntity.UserName);

            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
            return true;
        });
        return response;
    }

    /// <summary>
    /// Select categories
    /// </summary>
    /// <returns></returns>
    public SelectCategoriesResponse SelectCategories()
    {
        var response = new SelectCategoriesResponse {Success = false};

        // Get all categories
        var selectCategories = FindView()
            .Select(display => new SelectCategoriesEntity
            {
                CategoryId = display!.CategoryId,
                CategoryName = display.CategoryName,
                ParentId = display.ParentId
            }).ToList();
        
        // If there are no categories
        if (!selectCategories.Any())
        {
            response.SetMessage(MessageId.I00000, CommonMessages.CategoryNotFound);
            return response;
        }
        
        // True
        response.Success = true;
        response.Response = selectCategories;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Select category
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public SelectCategoryResponse SelectCategory(SelectCategoryRequest request)
    {
        var response = new SelectCategoryResponse {Success = false};
        
        // Get category
        var category = FindView(x => x.CategoryId == request.CategoryId)
            .Select(x => new SelectCategoryEntity
            {
                CategoryId = x!.CategoryId,
                CategoryName = x.CategoryName,
                ParentId = x.ParentId
            })
            .FirstOrDefault();
        
        // If category is null
        if (category == null)
        {
            response.SetMessage(MessageId.I00000, CommonMessages.CategoryNotFound);
            return response;
        }

        response.Success = true;
        response.Response = category;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Select sub categories
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public SelectSubCategoriesResponse SelectSubCategories(SelectSubCategoriesRequest request)
    {
        var response = new SelectSubCategoriesResponse {Success = false};
        
        // Get sub categories
        var subCategories = FindView(x => x.ParentId == request.ParentCategoryId)
            .Select(x => new SelectSubCategoriesEntity
            {
                CategoryId = x!.CategoryId,
                CategoryName = x.CategoryName,
            })
            .ToList();
        
        // If sub categories is null
        if (!subCategories.Any())
        {
            response.SetMessage(MessageId.I00000, CommonMessages.CategoryNotFound);
            return response;
        }
        
        // True
        response.Success = true;
        response.Response = subCategories;
        response.SetMessage(MessageId.I00001);
        return response;
        
    }

    /// <summary>
    /// Update category
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public UpdateCategoryResponse UpdateCategories(UpdateCategoryRequest request, IIdentityService identityService)
    {
        var response = new UpdateCategoryResponse { Success = false };
        
        // Update category
        Repository.ExecuteInTransaction(() =>
        {
            var category = Find(x => x.CategoryId == request.CategoryId).FirstOrDefault();
            if (category == null)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.CategoryNotFound);
                return true;
            }

            category.CategoryName = request.CategoryName;
            category.ParentId = request.ParentId;
            Repository.Update(category);
            Repository.SaveChanges(identityService.IdentityEntity.UserName);

            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
            return true;
        });
        return response;
    }

    /// <summary>
    /// Delete category
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public DeleteCategoryResponse DeleteCategory(DeleteCategoryRequest request, IIdentityService identityService)
    {
        var response = new DeleteCategoryResponse { Success = false };
        
        // Delete category
        Repository.ExecuteInTransaction(() =>
        {
            var category = Find(x => x.CategoryId == request.CategoryId && x.IsActive == true).FirstOrDefault();
            if (category == null)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.CategoryNotFound);
                return true;
            }
            
            var subCategories = Find(x => x.ParentId == request.CategoryId && x.IsActive == true).ToList();
            foreach (var sCategory in subCategories)
            {
                Update(sCategory!);
            }
            
            // Delete
            Update(category);
            SaveChanges(identityService.IdentityEntity.UserName, true);

            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
            return true;
        });
        return response;
    }
}