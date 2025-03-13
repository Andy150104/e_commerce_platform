using Client.Controllers.AbstractClass;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Client.Controllers.V1.Categories;

/// <summary>
/// SelectCategoryController - Selecting a category
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class SelectCategoryController : AbstractApiGetControllerNotToken<SelectCategoryRequest, SelectCategoryResponse, SelectCategoryEntity>
{
    private readonly IIdentityService _identityService;
    private readonly ICategoryService _categoryService;
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="categoryService"></param>
    public SelectCategoryController(IIdentityService identityService, ICategoryService categoryService)
    {
        _identityService = identityService;
        _categoryService = categoryService;
    }
    
    /// <summary>
    /// Incoming Get
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    public override SelectCategoryResponse Get(SelectCategoryRequest filter)
    {
        return Get(filter, _identityService, Logger, new SelectCategoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    protected override SelectCategoryResponse ExecGet(SelectCategoryRequest filter)
    {
        return _categoryService.SelectCategory(filter);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override SelectCategoryResponse ErrorCheck(SelectCategoryRequest request, List<DetailError> detailErrorList)
    {
        var response = new SelectCategoryResponse() { Success = false };
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
}