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
            case AggregateException: // Report API connection error
                logger.Warn($"Report API connection error ：{e.Message}");
                returnValue.SetMessage(MessageId.E99001);
                break;
            case DbUpdateConcurrencyException: // Exclusive control error UPDT_DATE is different
                logger.Error($"Exclusive error ：{e.Message}");
                returnValue.SetMessage(MessageId.I00001);
                break;
            case InvalidOperationException:
                if (e.InnerException?.HResult == -2146233088)
                {
                    // Exclusive control error Another update in the transaction
                    logger.Error($"Exclusive error ：{e.Message}");
                    returnValue.SetMessage(MessageId.I00001);
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
                    returnValue.SetMessage(MessageId.I00001);
                }
                else if (ex.Number == 3961)
                {
                    // Error that occurs when the definition of a view or the like is changed during execution
                    logger.Warn($"The definition of a view, etc. was changed during processing ：{e.Message} {e.StackTrace} {e.InnerException}");
                    returnValue.SetMessage(MessageId.E99999);
                }
                else
                {
                    logger.Warn($"A system error has occurred ：{e.Message} {e.StackTrace} {e.InnerException}");
                    returnValue.SetMessage(MessageId.E99999);
                }

                break;
            case Exception:
                // System error
                logger.Warn($"A system error has occurred ：{e.Message} {e.StackTrace} {e.InnerException}");
                returnValue.SetMessage(MessageId.E99999);
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
        // Check the state of ModelState
        if (modelState.IsValid) 
            return detailErrorList;

        var keys = modelState.Keys;
        foreach (var key in keys)
        {
            ModelStateEntry modelStateEntity = null;
            if (modelState.TryGetValue(key, out modelStateEntity) && modelStateEntity != null && modelStateEntity.ValidationState != ModelValidationState.Valid)
            {
                // Remove Value., Value[] from the beginning (corresponding to multiple updates in the master screen)
                var keyReplace = key;
                keyReplace = Regex.Replace(keyReplace, @"^Value\.", "");
                keyReplace = Regex.Replace(keyReplace, @"^Value\[\d+\]\.", "");
                var error = new KeyValuePair<string, string>(keyReplace, string.Join(";", modelStateEntity.Errors.Select(e => e.ErrorMessage)));
                Match matchesKey = null;
                var detailError = new DetailError();

                // Get the key
                if ((matchesKey = new Regex(@"^(.*?)\[(.*?)\]\.(.*?)$").Match(error.Key)).Success)
                {
                    // For lists such as spreadsheets
                    int i = 0;
                    var field = "";
                    var row = 0;
                    var columnName = "";
                    foreach (Group item in matchesKey.Groups)
                    {
                        if (i == 1) field = item.Value.ToString();
                        if (i == 2) row = Convert.ToInt32(item.Value.ToString());
                        if (i == 3) columnName = item.Value.ToString();
                        i++;
                    }
                    detailError.field = field;
                    // Since the line number starts from 0, add 1
                    detailError.columnName = columnName;
                }
                else
                {
                    // For normal fields
                    detailError.field = error.Key.Split('.').LastOrDefault();
                }
                // Convert the first letter of the field name to lowercase
                detailError.field = StringUtil.ToLowerCase(detailError.field);
                detailErrorList.Add(detailError);
            }
        }

        return detailErrorList;
    }
}