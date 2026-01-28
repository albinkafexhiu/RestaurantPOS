using RestaurantPOS.Domain.Enums;

namespace RestaurantPOS.Domain.Entities
{
    public class RestaurantTable : BaseEntity
    {
        public int TableNumber { get; set; }
        public string? Description { get; set; }
        public TableStatus Status { get; set; } = TableStatus.Free;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}