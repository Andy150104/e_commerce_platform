using Client.Controllers;
using server.Models;

namespace server.Controllers.V1.DetailOfProductScreenExchange
{
    public class DOPSEShowProductByIdResponse : AbstractApiResponse<DOPSEShowProductByIdEntity>
    {
        public override DOPSEShowProductByIdEntity Response { get; set; }
    }

    public class DOPSEShowProductByIdEntity
    {
        public Guid ProductId { get; set; }

        public string? Description { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Username { get; set; } = null!;
    }

}
