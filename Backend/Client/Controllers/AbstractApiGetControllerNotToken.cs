using Client.Services;
using Client.Utils;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Client.Controllers.AbstractClass;

public abstract class AbstractApiGetControllerNotToken<T, U, V> : ControllerBase
    where U : AbstractApiResponse<V>
    where T : class 
{
    public abstract U Get([FromQuery] T filter);

    /// <summary>
    /// Main processing method for GET with filters
    /// </summary>
    protected abstract U ExecGet(T filter);

    /// <summary>
    /// Error check method for GET
    /// </summary>
    protected internal abstract U ErrorCheck(T request,List<DetailError> detailErrorList);

    /// <summary>
    /// Template method for GET with filters
    /// </summary>
    protected U Get(T filter, IIdentityService identityService, Logger logger, U returnValue)
    {
        var loggingUtil = new LoggingUtil(logger, identityService.IdentityEntity?.UserName ?? "System");
        loggingUtil.StartLog();
        try
        {
                var detailErrorList = AbstractFunction<U, V>.ErrorCheck(this.ModelState);
                returnValue = ErrorCheck(filter, detailErrorList);

                if (returnValue.Success) returnValue = ExecGet(filter);
        }
        catch (Exception e)
        {
            return AbstractFunction<U, V>.GetReturnValue(returnValue, loggingUtil, e);
        }

        loggingUtil.EndLog(returnValue);
        return returnValue;
    }
}