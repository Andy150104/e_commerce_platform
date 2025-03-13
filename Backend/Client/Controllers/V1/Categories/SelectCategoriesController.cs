using Client.Controllers.AbstractClass;
using Client.Services;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Client.Controllers.V1.Categories;

/// <summary>
/// SelectCategoriesController - Select Categories
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class SelectCategoriesController : AbstractApiGetControllerNotToken<SelectCategoriesRequest, SelectCategoriesResponse, List<SelectCategoriesEntity>>
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly ICategoryService _categoryService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="categoryService"></param>
    public SelectCategoriesController(IIdentityService identityService, ICategoryService categoryService)
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
    public override SelectCategoriesResponse Get(SelectCategoriesRequest filter)
    {
        return Get(filter, _identityService, _logger, new SelectCategoriesResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    protected override SelectCategoriesResponse ExecGet(SelectCategoriesRequest filter)
    {
        return _categoryService.SelectCategories();
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override SelectCategoriesResponse ErrorCheck(SelectCategoriesRequest request, List<DetailError> detailErrorList)
    {
        var response = new SelectCategoriesResponse() { Success = false };
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