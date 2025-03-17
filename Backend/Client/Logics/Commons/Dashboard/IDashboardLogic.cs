namespace Client.Logics.Commons.Dashboard;

public interface IDashboardLogic
{
    Task<decimal> SelectTotalRevenueAsync();

    Task<decimal> SelectTotalRevenueByMonthAsync(int month, int year);

    Task<decimal> SelectTotalRevenueByDateAsync(DateOnly date);

    Task<int> SelectTotalOrdersAsync();

    Task<List<decimal>> SelectRevenueByMonthAsync();

    Task<List<AccessoryDataResponse>> SelectBestSellingAccessoriesAsync();
    
    
}