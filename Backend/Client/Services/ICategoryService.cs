using Client.Controllers.V1.Categories;
using Client.Models;

namespace Client.Services;

public interface ICategoryService : IBaseService<Category, Guid, VwCategoriesDisplay>
{
    CreateCategoryResponse CreateCategory(CreateCategoryRequest request, IIdentityService identityService);

    SelectCategoriesResponse SelectCategories();
    
    SelectCategoryResponse SelectCategory(SelectCategoryRequest request);
    
    SelectSubCategoriesResponse SelectSubCategories(SelectSubCategoriesRequest request);
    
    UpdateCategoryResponse UpdateCategories(UpdateCategoryRequest request, IIdentityService identityService);
    
    DeleteCategoryResponse DeleteCategory(DeleteCategoryRequest request, IIdentityService identityService);
}