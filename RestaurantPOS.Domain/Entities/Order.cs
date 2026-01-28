using RestaurantPOS.Domain.Enums;

namespace RestaurantPOS.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid RestaurantTableId { get; set; }
        public Guid WaiterId { get; set; }

        public DateTime OpenedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ClosedAt { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Open;
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Unknown;

        public int TotalAmount { get; set; }  // set when closing

        public RestaurantTable? RestaurantTable { get; set; }
        public Waiter? Waiter { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}