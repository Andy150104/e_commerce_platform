using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.Dashboard;

public class DashboardRequest : AbstractApiRequest
{
    [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
    public byte Month { get; set; }
    
    [Range(2000, 2100, ErrorMessage = "Year must be between 2000 and 2100")]
    public short Year { get; set; }
    
    public DateOnly Date { get; set; }
}