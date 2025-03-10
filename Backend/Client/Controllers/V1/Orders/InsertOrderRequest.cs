using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.TOS;

public class InsertOrderRequest : AbstractApiRequest
{
    public List<TOSOrderDetailRequest> OrderDetails { get; set; }
    
    [Required(ErrorMessage = "PaymentMethod is required")]
    public byte PaymentMethod { get; set; }
    
    [Required(ErrorMessage = "AddressId is required")]
    public Guid AddressId { get; set; }
}

public class TOSOrderDetailRequest
{
    [Required(ErrorMessage = "AccessoriesId is required")]
    public string AccessoryId { get; set; }
    
    [Required(ErrorMessage = "Quantity is required")]
    public int Quantity { get; set; }
}