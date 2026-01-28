namespace RestaurantPOS.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public Guid ProductCategoryId { get; set; }
        public int Price { get; set; }  // stored in MKD
        public bool IsAvailable { get; set; } = true;

        // Optional mapping to external API source
        public string? ExternalSourceId { get; set; }
        public string? Description { get; set; }

        public ProductCategory? ProductCategory { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}