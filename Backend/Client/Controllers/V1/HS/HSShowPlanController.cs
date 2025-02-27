using Client.Models;
using Client.Models.Helper;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.HS;

/// <summary>
/// HSShowPlanController - Show Plans Home Screen
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class HSShowPlanController : AbstractApiControllerNotToken<HSShowPlanRequest, HSShowPlanResponse, List<HSShowPlanEntity>>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    public HSShowPlanController(AppDbContext context)
    {
        _context = context;
        _context._Logger = logger;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override HSShowPlanResponse Post(HSShowPlanRequest request)
    {
        return Post(request, _context, logger, new HSShowPlanResponse());
    }
    
    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override HSShowPlanResponse Exec(HSShowPlanRequest request, IDbContextTransaction transaction)
    {
        var response = new HSShowPlanResponse() { Success = false };

        // Get plans
        var plans = _context.VwPlans.AsNoTracking()
            .Select(x => new HSShowPlanEntity
            {
                PlanId = x.PlanId,
                PlanName = x.PlanName,
                Description = x.Description,
                Price = x.Price,
                DurationMonths = x.DurationMonths
            })
            .ToList();
        
        // True
        response.Success = true;
        response.Response = plans;
        response.SetMessage(MessageId.I00001);
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

