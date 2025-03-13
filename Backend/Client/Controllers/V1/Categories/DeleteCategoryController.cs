using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.Categories;

/// <summary>
/// DeleteCategoryController - Delete a category
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DeleteCategoryController : AbstractApiController<DeleteCategoryRequest, DeleteCategoryResponse, string>
{
    private readonly IIdentityService _identityService;
    private readonly ICategoryService _categoryService;
    private readonly IAccessoryService _accessoryService;
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="categoryService"></param>
    /// <param name="identityApiClient"></param>
    /// <param name="accessoryService"></param>
    public DeleteCategoryController(IIdentityService identityService, ICategoryService categoryService, IIdentityApiClient identityApiClient, IAccessoryService accessoryService)
    {
        _identityService = identityService;
        _categoryService = categoryService;
        _accessoryService = accessoryService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Patch
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch]
    [Authorize(Roles = ConstRole.SaleEmployee + "," + ConstRole.Owner, 
        AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override DeleteCategoryResponse ProcessRequest(DeleteCategoryRequest request)
    {
        return ProcessRequest(request, _identityService, Logger, new DeleteCategoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override DeleteCategoryResponse Exec(DeleteCategoryRequest request)
    {
        return _categoryService.DeleteCategory(request, _identityService);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override DeleteCategoryResponse ErrorCheck(DeleteCategoryRequest request, List<DetailError> detailErrorList)
    {
        var response = new DeleteCategoryResponse() { Success = false };
        if (detailErrorList.Count > 0)
        {
            // Error
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }
        
        // Check if category is in use
        var accessory = _accessoryService.Find(x => x.CategoryId == request.CategoryId && x.IsActive == true).ToList();
        if (accessory.Any())
        {
            response.SetMessage(MessageId.I00000, "Category is in use");
            return response;
        }
        
        // True
        response.Success = true;
        return response;
    }
}