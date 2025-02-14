using Client.Models;

namespace Client.Models
{
    public partial class Images
    {
        public Guid ImageId { get; set; }
        public Guid ProductId { get; set; }
        public string ImageUrl { get; set; }
        public virtual Product Product { get; set; }
    }
}
