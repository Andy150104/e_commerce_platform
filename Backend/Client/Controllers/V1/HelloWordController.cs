using Client.Controllers;
using Client.Controllers.V1;
using Client.Models.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using Client.SystemClient;
using Client.Utils.Consts;

namespace server.Controllers.V1;

/// <summary>
///  HelloController - Print Hello World
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class HelloWordController : AbstractApiController<HelloWordRequest, HelloWordResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public HelloWordController(AppDbContext context, IIdentityApiClient identityApiClient)
    {
        _context = context;
        _context._Logger = logger;
        _identityApiClient = identityApiClient;
    }
    
    /// <summary>
    ///  
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override HelloWordResponse Post(HelloWordRequest request)
    {
        return Post(request, _context, logger, new HelloWordResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override HelloWordResponse Exec(HelloWordRequest request, IDbContextTransaction transaction)
    {
        var response = new HelloWordResponse() { Success = false };
        
        // True
        response.Success = true;
        response.SetMessage(MessageId.E10000);
        return response;
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected internal override HelloWordResponse ErrorCheck(HelloWordRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new HelloWordResponse() { Success = false };

        if (detailErrorList.Count > 0)
        {
            // Error
            response.Success = false;
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }
        // True
        response.Success = true;
        return response;
    }
}