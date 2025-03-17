using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.DPS;

public class DPSUpdateCartItemRequest : AbstractApiRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive value")]
    public int Quantity { get; set; }

    public string AccessoryId { get; set; }
}