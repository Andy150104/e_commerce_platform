using Client.Models;
using Client.Services;
using Microsoft.EntityFrameworkCore;

namespace Client.Logics.Commons.Dashboard;

public class DashboardLogic : IDashboardLogic
{
    private readonly IOrderService _orderService;
    private readonly IAccessoryService _accessoryService;
    private readonly IBaseService<OrderDetail, Guid, VwOrderDetailsWithProduct> _orderDetailService;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="orderService"></param>
    /// <param name="orderDetailService"></param>
    public DashboardLogic(IOrderService orderService, IBaseService<OrderDetail, Guid, VwOrderDetailsWithProduct> orderDetailService, IAccessoryService accessoryService)
    {
        _orderService = orderService;
        _orderDetailService = orderDetailService;
        _accessoryService = accessoryService;
    }

    /// <summary>
    /// Select total revenue
    /// </summary>
    /// <returns></returns>
    public async Task<decimal> SelectTotalRevenueAsync()
    {
        return await _orderService.FindView().SumAsync(x => x.TotalPrice);
    }

    /// <summary>
    /// Select total revenue by month
    /// </summary>
    /// <param name="month"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    public async Task<decimal> SelectTotalRevenueByMonthAsync(int month, int year)
    {
        return await _orderService.FindView(o => o.CreatedAt.Value.Month == month && o.CreatedAt.Value.Year == year).SumAsync(o => o.TotalPrice);;
    }

    /// <summary>
    /// Select total revenue by date
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public async Task<decimal> SelectTotalRevenueByDateAsync(DateOnly date)
    {
        // 00:00:00
        DateTime dateTimeStart = date.ToDateTime(TimeOnly.MinValue);
        
        // 23:59:59
        DateTime dateTimeEnd = date.ToDateTime(TimeOnly.MaxValue); 

        return await _orderService.FindView(o => o.CreatedAt >= dateTimeStart && o.CreatedAt <= dateTimeEnd)
            .SumAsync(o => o.TotalPrice);
    }

    /// <summary>
    /// Select total orders
    /// </summary>
    /// <returns></returns>
    public async Task<int> SelectTotalOrdersAsync()
    {
        return await _orderService.FindView().CountAsync();
    }

    /// <summary>
    /// Select revenue by month
    /// </summary>
    /// <returns></returns>
    public async Task<List<decimal>> SelectRevenueByMonthAsync()
    {
        // Get revenues
        var revenues = await _orderService
            .FindView()
            .GroupBy(o => o.CreatedAt.Value.Month)
            .Select(g => new { Month = g.Key, Total = g.Sum(o => o.TotalPrice) })
            .OrderBy(g => g.Month)
            .ToListAsync();
        
        // Initialize revenue data
        var revenueData = new decimal[12];
        foreach (var revenue in revenues)
        {
            // Set revenue data
            revenueData[revenue.Month - 1] = revenue.Total;
        }

        // Return revenue data
        return revenueData.ToList();
    }

    /// <summary>
    /// Select best selling accessories
    /// </summary>
    /// <returns></returns>
    public async Task<List<AccessoryDataResponse>> SelectBestSellingAccessoriesAsync()
    {
        //Group data by AccessoryId and calculate total sale
        var result = await _orderDetailService
            .FindView()
            .GroupBy(od => od.AccessoryId)
            .Select(g => new
            {
                AccessoryId = g.Key,
                TotalSale = g.Sum(od => od.Quantity)
            })
            .OrderByDescending(g => g.TotalSale)
            .ToListAsync();

        // Get list of AccessoryId from result
        var accessoryIds = result.Select(r => r.AccessoryId).ToList();

        // Get list of AccessoryName from AccessoryId
        var accessoryNames = await _accessoryService.FindView(a => accessoryIds.Contains(a.AccessoryId))
            .ToDictionaryAsync(a => a.AccessoryId, a => a.AccessoryName);

        // Create response
        var bestSellingProducts = result.Select(r => new AccessoryDataResponse
        {
            AccessoryName = accessoryNames.ContainsKey(r.AccessoryId) ? accessoryNames[r.AccessoryId] : "",
            TotalSale = r.TotalSale
        }).ToList();

        return bestSellingProducts;
    }

}

public class AccessoryDataResponse
{
    public string AccessoryName { get; set; }
    
    public int TotalSale { get; set; }
}