namespace RestaurantPOS.Domain.Entities
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}