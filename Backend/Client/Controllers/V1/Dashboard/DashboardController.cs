using Client.Controllers.AbstractClass;
using Client.Logics.Commons.Dashboard;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.Dashboard;

/// <summary>
/// DashboardController - Dashboard for admin
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DashboardController : AbstractApiGetAsyncController<DashboardRequest, DashboardResponse, DashboardEntity>
{
    private readonly IIdentityService _identityService;
    private readonly IDashboardLogic _dashboardLogic;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="dashboardLogic"></param>
    /// <param name="identityApiClient"></param>
    public DashboardController(IIdentityService identityService, IDashboardLogic dashboardLogic, IIdentityApiClient identityApiClient)
    {
        _identityService = identityService;
        _dashboardLogic = dashboardLogic;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Get
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [Authorize(Roles = ConstRole.Owner, AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override async Task<DashboardResponse> Get(DashboardRequest filter)
    {
        return await Get(filter, _identityService, _logger, new DashboardResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override async Task<DashboardResponse> ExecGet(DashboardRequest request)
    {
        var response = new DashboardResponse { Success = false };
        
        // Get data
        var entity = new DashboardEntity();
        
        // Total revenue
        entity.TotalRevenue = await _dashboardLogic.SelectTotalRevenueAsync();
        
        // Total of number order
        entity.TotalOrder = await _dashboardLogic.SelectTotalOrdersAsync();
        
        // Total of revenue this month
        entity.TotalRevenueThisMonth = await _dashboardLogic.SelectTotalRevenueByMonthAsync(request.Month, request.Year);
        
        // Revenue growth rate last month
        var totalRevenueLastMonth = await _dashboardLogic.SelectTotalRevenueByMonthAsync(request.Month - 1, request.Year);
        entity.RevenueGrowthRateLastMonth = await CalculateGrowthRate(entity.TotalRevenueThisMonth, totalRevenueLastMonth);
        
        // Total revenue today
        entity.TotalRevenueToday = await _dashboardLogic.SelectTotalRevenueByDateAsync(request.Date);
        
        // Revenue growth rate yesterday
        var totalRevenueYesterday = await _dashboardLogic.SelectTotalRevenueByDateAsync(request.Date.AddDays(-1));
        entity.RevenueGrowthRateYesterday = await CalculateGrowthRate(entity.TotalRevenueToday, totalRevenueYesterday);
        
        // Total revenue data
        entity.TotalOrder = await _dashboardLogic.SelectTotalOrdersAsync();

        // Revenue data by month
        entity.RevenueData = await _dashboardLogic.SelectRevenueByMonthAsync();
        
        // Best selling products ( accessories )
        entity.AccessoryData = await _dashboardLogic.SelectBestSellingAccessoriesAsync();
        
        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        response.Response = entity;
        return response;
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override DashboardResponse ErrorCheck(DashboardRequest request, List<DetailError> detailErrorList)
    {
        var response = new DashboardResponse() { Success = false };
        if (detailErrorList.Count > 0)
        {
            // Error
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }

        if (request.Month > DateTime.Now.Month || request.Year > DateTime.Now.Year || request.Date > DateOnly.FromDateTime(DateTime.Now))
        {
            response.SetMessage(MessageId.I00000, $"Check the time of the request. Month: {request.Month}, Year: {request.Year}, Date: {request.Date}");
            return response;
        }
        
        // True
        response.Success = true;
        return response;
    }
    
    private async Task<decimal> CalculateGrowthRate(decimal current, decimal previous)
    {
        // If last month is 0, return 100%
        if (previous == 0) 
            return 100;
        return Math.Round(((current - previous) / previous) * 100, 2);
    }
}