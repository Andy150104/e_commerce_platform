using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers;

public abstract class AbstractApiAsyncController<T, U, V> : ControllerBase
    where T : AbstractApiRequest
    where U : AbstractApiResponse<V>
{
    /// <summary>
    /// API entry point
    /// </summary>
    public abstract Task<U> Post(T request);

    /// <summary>
    /// Main processing
    /// </summary>
    protected abstract Task<U> Exec(T request, IDbContextTransaction transaction);

    /// <summary>
    /// Error check
    /// </summary>
    protected internal abstract U ErrorCheck(T request, List<DetailError> detailErrorList, IDbContextTransaction transaction);

    /// <summary>
    /// Authentication API client
    /// </summary>
    protected IIdentityApiClient _identityApiClient;

    /// <summary>
    /// Transaction isolation level
    /// </summary>
    /// <remarks>
    /// Default SNAPSHOT Change it in the constructor
    /// </remarks>
    protected System.Data.IsolationLevel _isolationLevel = System.Data.IsolationLevel.Snapshot;

    /// <summary>
    /// TemplateMethod
    /// </summary>
    /// <param name="request"></param>
    /// <param name="appDbContext"></param>
    /// <param name="logger"></param>
    /// <param name="returnValue"></param>
    /// <returns></returns>
    protected async Task<U> Post(T request, AppDbContext appDbContext, Logger logger, U returnValue)
    {
        var loggingUtil = new LoggingUtil(logger, appDbContext?.IdentityEntity?.UserName ?? "System");

        // Get identity information 
        appDbContext.IdentityEntity = _identityApiClient?.GetIdentity(User);
        loggingUtil.StartLog(request);

        // Check authentication information
        if (appDbContext.IdentityEntity == null)
        {
            // Authentication error
            loggingUtil.FatalLog($"Authenticated, but information is missing.");
            returnValue.Success = false;
            returnValue.SetMessage(MessageId.E11006);
            loggingUtil.EndLog(returnValue);
            return returnValue;
        }
        // Additional user information
        try
        {
            appDbContext.IdentityEntity.UserName = appDbContext.VwUserAuthentications
                .AsNoTracking()
                .FirstOrDefault(x => x.UserName == appDbContext.IdentityEntity.UserName).UserName;
        }
        catch (Exception e)
        {
            // Additional user information error
            loggingUtil.FatalLog($"Failed to get additional user information.：{e.Message}");
            returnValue.Success = false;
            returnValue.SetMessage(MessageId.E11006);
            loggingUtil.EndLog(returnValue);
            return returnValue;
        }

        try
        {
            appDbContext. _Logger = logger;

            // Start transaction
            using (var transaction = appDbContext.Database.BeginTransaction())
            {
                // Error check
                var detailErrorList = AbstractFunction<T, U, V>.ErrorCheck(this.ModelState);
                returnValue = ErrorCheck(request, detailErrorList, transaction);

                // If there is no error, execute the main process
                if (returnValue.Success && !request.IsOnlyValidation) returnValue = await Exec(request, transaction);
            }
        }
        catch (Exception e)
        {
            return AbstractFunction<T, U, V>.GetReturnValue(returnValue, loggingUtil, e, appDbContext);
        }

        // Processing end log
        loggingUtil.EndLog(returnValue);
        return returnValue;
    }
}