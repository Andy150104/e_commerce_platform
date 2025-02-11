using Client.Controllers;
using Client.Models.Helper;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using server.Logics.Commons;

namespace server.Controllers.V1.UserRegisterScreen;

/// <summary>
/// URSUserVerifyController - Verify User
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class URSUserVerifyController : AbstractApiControllerNotToken<URSUserVerifyRequest, URSUserVerifyResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public URSUserVerifyController(AppDbContext context)
    {
        _context = context;
        _context._Logger = logger;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override URSUserVerifyResponse Post(URSUserVerifyRequest request)
    {
        return Post(request, _context, logger, new URSUserVerifyResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override URSUserVerifyResponse Exec(URSUserVerifyRequest request, IDbContextTransaction transaction)
    {
        var response = new URSUserVerifyResponse() { Success = false };
        
        // Decrypt Key
        var keyDecrypt = CommonLogic.DecryptText(request.Key, _context);
        
        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
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
    protected internal override URSUserVerifyResponse ErrorCheck(URSUserVerifyRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new URSUserVerifyResponse() { Success = false };

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