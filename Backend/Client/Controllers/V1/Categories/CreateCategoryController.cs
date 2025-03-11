using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.Categories;

/// <summary>
/// CreateCategoryController - Create a new category
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class CreateCategoryController : AbstractApiController<CreateCategoryRequest, CreateCategoryResponse, string>
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly ICategoryService _categoryService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="categoryService"></param>
    /// <param name="identityApiClient"></param>
    public CreateCategoryController(IIdentityService identityService, ICategoryService categoryService, IIdentityApiClient identityApiClient)
    {
        _identityService = identityService;
        _categoryService = categoryService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = ConstRole.SaleEmployee + "," + ConstRole.Owner, 
        AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override CreateCategoryResponse ProcessRequest(CreateCategoryRequest request)
    {
        return ProcessRequest(request, _identityService, Logger, new CreateCategoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override CreateCategoryResponse Exec(CreateCategoryRequest request)
    {
        return _categoryService.CreateCategory(request, _identityService);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override CreateCategoryResponse ErrorCheck(CreateCategoryRequest request, List<DetailError> detailErrorList)
    {
        var response = new CreateCategoryResponse() { Success = false };
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