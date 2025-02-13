using Client.Controllers;
using System.ComponentModel.DataAnnotations;

namespace server.Controllers.V1.DetailOfProductScreenExchange
{
    public class DOPSEShowProductByIdRequest : AbstractApiRequest
    {
        [Required(ErrorMessage = "Product Id is required")]
        public Guid ProductId {  get; set; } 
    }
}
