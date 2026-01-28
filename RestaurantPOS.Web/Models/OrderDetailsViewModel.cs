using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantPOS.Web.Models
{
    public class OrderItemDisplay
    {
        public Guid OrderItemId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int LineTotal { get; set; }
    }

    public class OrderDetailsViewModel
    {
        public Guid TableId { get; set; }
        public int TableNumber { get; set; }

        public Guid? OrderId { get; set; }
        public bool HasOpenOrder => OrderId.HasValue;

        public List<OrderItemDisplay> Items { get; set; } = new();
        public List<RestaurantPOS.Domain.Entities.Product> Products { get; set; } = new();
        public List<SelectListItem> Waiters { get; set; } = new();

        // Form fields
        public Guid SelectedWaiterId { get; set; }
        public Guid SelectedProductId { get; set; }
        public int Quantity { get; set; } = 1;

        public int Total { get; set; }
    }
}