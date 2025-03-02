using Client.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using Auth.Controllers;
using Auth.Models.Helper;
using Auth.Utils.Consts;

namespace Auth.Controllers.V1.UserRegisterScreen;

/// <summary>
/// URSUserVerifyController - Verify User
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UserVerifyKeyController : AbstractApiControllerNotToken<UserVerifyKeyRequest, UserVerifyKeyResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public UserVerifyKeyController(AppDbContext context)
    {
        _context = context;
        _context._Logger = logger;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="keyRequest"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override UserVerifyKeyResponse Post(UserVerifyKeyRequest keyRequest)
    {
        return Post(keyRequest, _context, logger, new UserVerifyKeyResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="keyRequest"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override UserVerifyKeyResponse Exec(UserVerifyKeyRequest keyRequest, IDbContextTransaction transaction)
    {
        var response = new UserVerifyKeyResponse() { Success = false };

        // Check key exist
        var keyExist = _context.VwUserVerifies.AsNoTracking().FirstOrDefault(x => x.Key == keyRequest.Key);
        
        if (keyExist == null)
        {
            response.SetMessage(MessageId.E00000, "Verify error. Check the key of the error item.");
            return response;
        }
        
        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="keyRequest"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected internal override UserVerifyKeyResponse ErrorCheck(UserVerifyKeyRequest keyRequest, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new UserVerifyKeyResponse() { Success = false };

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