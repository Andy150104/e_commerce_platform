using System.Text.RegularExpressions;
using Client.Models.Helper;
using Client.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NLog;
using Client.Utils.Consts;

namespace Client.Controllers;

/// <summary>
/// 
/// </summary>
public class AbstractFunction<T, U, V>
    where T : AbstractApiRequest
    where U : AbstractApiResponse<V>
{
    /// <summary>
    /// 共通エラーレスポンス
    /// </summary>
    public static U GetReturnValue(U returnValue, Logger logger, Exception e, AppDbContext context)
    {
        switch (e)
        {
            case AggregateException:
                logger.Warn($"Report API connection error ：{e.Message}");
                returnValue.SetMessage(MessageId.E99001);
                break;
            case DbUpdateConcurrencyException: 
                logger.Error($"Exclusive error ：{e.Message}");
                returnValue.SetMessage(MessageId.E99001);
                break;
            case InvalidOperationException:
                if (e.InnerException?.HResult == -2146233088)
                {
                    // Exclusive control error Another update in the transaction
                    logger.Error($"Exclusive error ：{e.Message}");
                    returnValue.SetMessage(MessageId.E99001);
                }
                else
                {
                    logger.Warn($"A system error has occurred.：{e.Message} {e.StackTrace} {e.InnerException}");
                    returnValue.SetMessage(MessageId.E99999);
                }

                break;
            case SqlException:
                var ex = e as SqlException;
                if (e.InnerException?.HResult == -2147467259)
                {
                    // SQL timeout
                    logger.Error($"SQL timeout error ：{e.Message} {e.StackTrace} {e.InnerException}");
                    returnValue.SetMessage(MessageId.E99001);
                }
                else if (ex.Number == 3961)
                {
                    // Error that occurs when the definition of a view or the like is changed during execution
                    logger.Warn($"The definition of a view, etc. was changed during processing ：{e.Message} {e.StackTrace} {e.InnerException}");
                    returnValue.SetMessage(MessageId.E99001);
                }
                else
                {
                    logger.Warn($"A system error has occurred ：{e.Message} {e.StackTrace} {e.InnerException}");
                    returnValue.SetMessage(MessageId.E99001);
                }

                break;
            case Exception:
                // System error
                logger.Warn($"A system error has occurred ：{e.Message} {e.StackTrace} {e.InnerException}");
                returnValue.SetMessage(MessageId.E99001);
                break;
        }

        returnValue.Success = false;
        logger.Warn(returnValue);
        return returnValue;
    }
    
    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="modelState"></param>
    /// <returns></returns>
    public static List<DetailError> ErrorCheck(ModelStateDictionary modelState)
    {
        var detailErrorList = new List<DetailError>();

        // If there is no error, return
        if (modelState.IsValid) 
            return detailErrorList;

        foreach (var entry in modelState)
        {
            var key = entry.Key;
            var modelStateEntity = entry.Value;

            if (modelStateEntity == null || modelStateEntity.ValidationState == ModelValidationState.Valid)
                continue;

            // Remove the prefix "Value." from the key
            var keyReplace = Regex.Replace(key, @"^Value\.", "");
            keyReplace = Regex.Replace(keyReplace, @"^Value\[\d+\]\.", "");

            // Get error message
            var errorMessage = string.Join("; ", modelStateEntity.Errors.Select(e => e.ErrorMessage));

            var detailError = new DetailError();
            Match matchesKey;

            // Extract information from the key in the structure: object[index].property
            if ((matchesKey = new Regex(@"^(.*?)\[(\d+)\]\.(.*?)$").Match(keyReplace)).Success)
            {
                // In the case of a list
                detailError.field = matchesKey.Groups[1].Value;
            }
            else
            {
                // In the case of a single item
                detailError.field = keyReplace.Split('.').LastOrDefault();
            }

            // Convert the field name to lowercase
            detailError.field = StringUtil.ToLowerCase(detailError.field);

            // Set the error message
            detailError.ErrorMessage = errorMessage;

            detailErrorList.Add(detailError);
        }
        return detailErrorList;
    }

}