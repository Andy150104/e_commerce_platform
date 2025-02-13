namespace server.Models
{
    public partial class ImagesBlindBox
    {
        public Guid ImageId { get; set; }
        public Guid BlindBoxId { get; set; }
        public string ImageUrl { get; set; }
        public virtual BlindBox BlindBox { get; set; }
    }
}
