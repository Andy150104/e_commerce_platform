using Client.Logics.Commons.Dashboard;

namespace Client.Controllers.V1.Dashboard;

public class DashboardResponse : AbstractApiResponse<DashboardEntity>
{
    public override DashboardEntity Response { get; set; }
}

public class DashboardEntity
{
    public decimal TotalRevenue { get; set; }
    
    public decimal TotalRevenueThisMonth { get; set; }
    
    public decimal RevenueGrowthRateLastMonth { get; set; }
    
    public decimal TotalRevenueToday { get; set; }
    
    public decimal RevenueGrowthRateYesterday { get; set; }
    
    public int TotalOrder { get; set; }
    
    public List<decimal> RevenueData { get; set; }
    
    public List<AccessoryDataResponse> AccessoryData { get; set; }
}