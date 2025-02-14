using Client.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.AddExchangeProductScreen
{
    public class AEPSAddExchangeProductRequest : AbstractApiRequest
    {
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
