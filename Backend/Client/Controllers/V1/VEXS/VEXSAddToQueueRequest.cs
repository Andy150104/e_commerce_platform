using Client.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.ViewQueueExchangeScreen
{
    public class VEXSAddToQueueRequest : AbstractApiRequest
    {
        [Required(ErrorMessage = "BlindBoxId is required")]
        public Guid BlindBoxId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Images is required")]
        public List<string> ImageUrls { get; set; }
    }
}
