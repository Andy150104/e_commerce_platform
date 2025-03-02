using System.Data;
using Client.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using Auth.Controllers;
using Auth.Models.Helper;
using Auth.Utils.Consts;

namespace client.Identity.Controllers.V1.User.UserRole;

/// <summary>
/// UpdateRoleController - Update User Role
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UpdateRoleController : AbstractApiControllerNotToken<UpdateRoleRequest, UpdateRoleResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public UpdateRoleController(AppDbContext context)
    {
        _context = context;
        _context._Logger = logger;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public override UpdateRoleResponse Post(UpdateRoleRequest request)
    {
        return Post(request, _context, logger, new UpdateRoleResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override UpdateRoleResponse Exec(UpdateRoleRequest request, IDbContextTransaction transaction)
    {
        var response = new UpdateRoleResponse() { Success = false };
        
        // Set parameters
        var paramList = new List<SqlParameter>
        {
            new SqlParameter("@Username", request.UserName),
            new SqlParameter("@PlanId", SqlDbType.UniqueIdentifier) 
            { 
                Value = request.PlanId.HasValue && request.PlanId != Guid.Empty ? 
                    request.PlanId.Value : DBNull.Value
            },
            new SqlParameter("@Result", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            }
        };
        
        // Execute stored procedure
        var result = _context.ExecuteStoredProcedure("SP_UpdateUserRole", paramList);
        
        // Get result
        var updateResult = Convert.ToBoolean(paramList.First(p => p.ParameterName == "@Result").Value);
        
        // If failed to update
        if (!updateResult)
        {
            transaction.Rollback();
            response.SetMessage(MessageId.E00000, "Failed to update user role.");
            return response;
        }
        
        // True
        transaction.Commit();
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
    protected internal override UpdateRoleResponse ErrorCheck(UpdateRoleRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new UpdateRoleResponse() { Success = false };

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