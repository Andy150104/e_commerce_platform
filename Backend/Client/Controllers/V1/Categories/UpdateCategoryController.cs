using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.Categories;

/// <summary>
/// UpdateCategoryController - Updating a category
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UpdateCategoryController : AbstractApiController<UpdateCategoryRequest, UpdateCategoryResponse, string>
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
    public UpdateCategoryController(IIdentityService identityService, ICategoryService categoryService, IIdentityApiClient identityApiClient)
    {
        _identityService = identityService;
        _categoryService = categoryService;
        _identityApiClient = identityApiClient;
    }
    
    /// <summary>
    /// Incoming Put
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Roles = ConstRole.SaleEmployee + "," + ConstRole.Owner, 
        AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override UpdateCategoryResponse ProcessRequest(UpdateCategoryRequest request)
    {
        return ProcessRequest(request, _identityService, Logger, new UpdateCategoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override UpdateCategoryResponse Exec(UpdateCategoryRequest request)
    {
        return _categoryService.UpdateCategories(request, _identityService);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override UpdateCategoryResponse ErrorCheck(UpdateCategoryRequest request, List<DetailError> detailErrorList)
    {
        var response = new UpdateCategoryResponse() { Success = false };
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