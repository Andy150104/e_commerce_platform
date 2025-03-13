using System.Data;
using Client.Controllers.AbstractClass;
using Client.Services;
using Client.Utils;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Client.Controllers;

public abstract class AbstractApiAsyncControllerNotToken
    <T, U, V> : ControllerBase
    where T : AbstractApiRequest
    where U : AbstractApiResponse<V>
{
    /// <summary>
    /// API entry point
    /// </summary>
    public abstract Task<U> ProcessRequest(T request);

    /// <summary>
    /// Main processing
    /// </summary>
    protected abstract Task<U> Exec(T request);

    /// <summary>
    /// Error check
    /// </summary>
    protected internal abstract U ErrorCheck(T request, List<DetailError> detailErrorList);

    /// <summary>
    /// Transaction isolation level
    /// </summary>
    /// <remarks>
    /// Default SNAPSHOT Change it in the constructor
    /// </remarks>
    protected IsolationLevel _isolationLevel = IsolationLevel.Snapshot;

    /// <summary>
    /// TemplateMethod
    /// </summary>
    /// <param name="request"></param>
    /// <param name="appDbContext"></param>
    /// <param name="logger"></param>
    /// <param name="returnValue"></param>
    /// <returns></returns>
    protected async Task<U> ProcessRequest(T request, IIdentityService identityService, Logger logger, U returnValue)
    {
        var loggingUtil = new LoggingUtil(logger, identityService.IdentityEntity?.UserName ?? "System");
        loggingUtil.StartLog(request);
        try
        {
            // Error check
            var detailErrorList = AbstractFunction<U, V>.ErrorCheck(this.ModelState);
            returnValue = ErrorCheck(request, detailErrorList);

            // If there is no error, execute the main process
            if (returnValue.Success) returnValue = await Exec(request);
        }
        catch (Exception e)
        {
            return AbstractFunction<U, V>.GetReturnValue(returnValue, loggingUtil, e);
        }

        // Processing end log
        loggingUtil.EndLog(returnValue);
        return returnValue;
    }
}