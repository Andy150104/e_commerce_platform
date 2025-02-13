using Client.Controllers;
using System.ComponentModel.DataAnnotations;

namespace server.Controllers.V1.DisplayProductScreenExchange
{
    public class DPSESAddToWishlistRequest : AbstractApiRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public Guid? CategoryId { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public List<string>? ImageUrls { get; set; }
    }
}
