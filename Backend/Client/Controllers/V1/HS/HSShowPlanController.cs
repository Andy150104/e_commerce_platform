using Client.Controllers;
using Client.Models;
using Client.Models.Helper;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.HomeScreen;

/// <summary>
/// HSShowPlanController - Show Plans Home Screen
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class HSShowPlanController : AbstractApiControllerNotToken<HSShowPlanRequest, HSShowPlanResponse, List<VwPlan>>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    public HSShowPlanController(AppDbContext context)
    {
        _context = context;
        _context._Logger = logger;
    }

    /// <summary>
    /// Incomng Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override HSShowPlanResponse Post(HSShowPlanRequest request)
    {
        return Post(request, _context, logger, new HSShowPlanResponse());
    }


    /// <summary>
    /// Main processing - Retrieved VwPlan table from database
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override HSShowPlanResponse Exec(HSShowPlanRequest request, IDbContextTransaction transaction)
    {
        var response = new HSShowPlanResponse() { Success = false };

        var plans = _context.VwPlans.AsNoTracking().ToList();
        if (plans == null || plans.Count == 0)
        {
            response.SetMessage(MessageId.E00000, "No plans found");
            return response;
        }

        response.Response = plans;
        response.Success = true;
        response.SetMessage(MessageId.I00001, "Plan retrieved successfully");
        return response;
    }
    /// <summary>
    /// ERROR CHECK
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override HSShowPlanResponse ErrorCheck(HSShowPlanRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new HSShowPlanResponse() { Success = false };

        if (detailErrorList.Count > 0)
        {
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }

        response.Success = true;
        return response;
    }
}

