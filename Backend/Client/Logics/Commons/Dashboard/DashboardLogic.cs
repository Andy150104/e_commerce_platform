using Client.Models;
using Client.Services;
using Client.Utils.Consts;
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
        return await _orderService.FindView(x => x.OrderStatus == ((byte) ConstantEnum.OrderStatus.Success)).SumAsync(x => x.TotalPrice);
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

        return await _orderService.FindView(o =>
                o.CreatedAt >= dateTimeStart &&
                o.CreatedAt <= dateTimeEnd && 
                o.OrderStatus == ((byte) ConstantEnum.OrderStatus.Success))
            .SumAsync(o => o.TotalPrice);
    }

    /// <summary>
    /// Select total orders
    /// </summary>
    /// <returns></returns>
    public async Task<int> SelectTotalOrdersAsync()
    {
        return await _orderService.FindView(x => x.OrderStatus == ((byte) ConstantEnum.OrderStatus.Success)).CountAsync();
    }

    /// <summary>
    /// Select revenue by month
    /// </summary>
    /// <returns></returns>
    public async Task<List<decimal>> SelectRevenueByMonthAsync()
    {
        // Get revenues
        var revenues = await _orderService
            .FindView(x => x.OrderStatus == ((byte) ConstantEnum.OrderStatus.Success))
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
        // Get Order Success
        var orderIds = await _orderService
            .FindView(o => o.OrderStatus == (byte) ConstantEnum.OrderStatus.Success)
            .Select(o => o.OrderId)
            .ToListAsync();
        
        //Group data by AccessoryId and calculate total sale
        var bestSellingAccessories = await _orderDetailService
            .FindView(od => orderIds.Contains(od.OrderId))
            .GroupBy(od => new { od.AccessoryId, od.ProductName }) // Join Accessory luôn
            .Select(g => new AccessoryDataResponse
            {
                AccessoryName = g.Key.ProductName,
                TotalSale = g.Sum(od => od.Quantity)
            })
            .OrderByDescending(g => g.TotalSale)
            .ToListAsync();

        return bestSellingAccessories;
    }

}

public class AccessoryDataResponse
{
    public string AccessoryName { get; set; }
    
    public int TotalSale { get; set; }
}