namespace RestaurantPOS.Domain.Entities
{
    public class Expense : BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        public int Amount { get; set; }  // MKD
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Type { get; set; }  // Supplies, Salary, etc.
    }
}