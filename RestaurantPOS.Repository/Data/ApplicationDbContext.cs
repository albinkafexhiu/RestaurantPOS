using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Domain.Entities;

namespace RestaurantPOS.Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<RestaurantTable> RestaurantTables { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Expense> Expenses { get; set; }

    }
}