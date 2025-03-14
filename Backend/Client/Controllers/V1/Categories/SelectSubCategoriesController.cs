using Client.Controllers.AbstractClass;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Client.Controllers.V1.Categories;

/// <summary>
/// SelectSubCategoriesController - Select Sub Categories
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class SelectSubCategoriesController : AbstractApiGetControllerNotToken<SelectSubCategoriesRequest, SelectSubCategoriesResponse, List<SelectSubCategoriesEntity>>
{
    private readonly IIdentityService _identityService;
    private readonly ICategoryService _categoryService;
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="categoryService"></param>
    /// <param name="identityApiClient"></param>
    public SelectSubCategoriesController(IIdentityService identityService, ICategoryService categoryService)
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
    public override SelectSubCategoriesResponse Get(SelectSubCategoriesRequest filter)
    {
        return Get(filter, _identityService, Logger, new SelectSubCategoriesResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    protected override SelectSubCategoriesResponse ExecGet(SelectSubCategoriesRequest filter)
    {
        return _categoryService.SelectSubCategories(filter);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override SelectSubCategoriesResponse ErrorCheck(SelectSubCategoriesRequest request, List<DetailError> detailErrorList)
    {
        var response = new SelectSubCategoriesResponse() { Success = false };
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