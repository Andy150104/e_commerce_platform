using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.Orders;

public class InsertOrderRequest : AbstractApiRequest
{
    public List<TOSOrderDetailRequest>? OrderDetails { get; set; }
    
    [Required(ErrorMessage = "PaymentMethod is required")]
    public byte PaymentMethod { get; set; }
    
    [Required(ErrorMessage = "PlatForm is required")]
    [Range(1, 2, ErrorMessage = "PlatForm between 1 and 2")]
    public byte PlatForm { get; set; }
    
    [Required(ErrorMessage = "AddressId is required")]
    public Guid AddressId { get; set; }
}

public class TOSOrderDetailRequest
{
    [Required(ErrorMessage = "AccessoriesId is required")]
    public string AccessoryId { get; set; }
    
    [Required(ErrorMessage = "Quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive value")]
    public int Quantity { get; set; }
}