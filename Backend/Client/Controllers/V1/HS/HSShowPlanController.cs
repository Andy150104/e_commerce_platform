using Client.Controllers;
using Client.Models.Helper;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace server.Controllers.V1.HomeScreen;
/// <summary>
/// HSShowPlanController - Show Plans Home Screen
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class HSShowPlanController : AbstractApiControllerNotToken<HSShowPlanRequest, HSShowPlanResponse, List<HSShowPlanResponseEntity>>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;
    public HSShowPlanController(AppDbContext context)
    {
        _context = context;
        _context._Logger = logger;
    }
    /// <summary>
    /// Coming Posh
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

        var plans = _context.VwPlans.AsNoTracking().ToList();

        if (!plans.Any() || plans.Count == 0)
        {
            response.SetMessage(MessageId.E00000, "No plans found");
            return response;
        }

        response.Response = plans.Select(p => new HSShowPlanResponseEntity
        {
            PlanId = p.PlanId,
            PlanName = p.PlanName,
            Description = p.Description,
            DurationMonths = p.DurationMonths,
            Price = p.Price
        }).ToList();
        response.Success = true;
        response.SetMessage(MessageId.I00001, "Plan retrieved successfully");
        return response;
    }

    /// <summary>
    /// Error check
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
        //true
        response.Success = true;
        return response;
    }
}

