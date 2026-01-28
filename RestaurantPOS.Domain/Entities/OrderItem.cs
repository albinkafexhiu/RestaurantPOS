namespace RestaurantPOS.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
        public int UnitPrice { get; set; }   // snapshot of Product.Price
        public int LineTotal { get; set; }   // Quantity * UnitPrice

        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}