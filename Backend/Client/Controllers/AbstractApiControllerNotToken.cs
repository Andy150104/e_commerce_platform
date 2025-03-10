using System.Data;
using Client.Controllers.AbstractClass;
using Client.Services;
using Client.Utils;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Client.Controllers;

public abstract class AbstractApiControllerNotToken<T, TU, TV> : ControllerBase
    where T : AbstractApiRequest
    where TU : AbstractApiResponse<TV>
{
    /// <summary>
    /// API entry point
    /// </summary>
    public abstract TU Post(T request);

    /// <summary>
    /// Main processing
    /// </summary>
    protected abstract TU Exec(T request);

    /// <summary>
    /// Error check
    /// </summary>
    protected internal abstract TU ErrorCheck(T request, List<DetailError> detailErrorList);

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
    /// <param name="identityService"></param>
    /// <param name="logger"></param>
    /// <param name="returnValue"></param>
    /// <returns></returns>
    protected TU Post(T request, IIdentityService identityService, Logger logger, TU returnValue)
    {
        var loggingUtil = new LoggingUtil(logger, identityService.IdentityEntity.UserName ?? "System");
        try
        {
            // Error check
            var detailErrorList = AbstractFunction<TU, TV>.ErrorCheck(this.ModelState);
            returnValue = ErrorCheck(request, detailErrorList);

            // If there is no error, execute the main process
            if (returnValue.Success) returnValue = Exec(request);
        }
        catch (Exception e)
        {
            return AbstractFunction<TU, TV>.GetReturnValue(returnValue, loggingUtil, e);
        }

        // Processing end log
        loggingUtil.EndLog(returnValue);
        return returnValue;
    }
}