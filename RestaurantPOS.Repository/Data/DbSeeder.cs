using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Domain.Enums;
using RestaurantPOS.Web.Data;
using RestaurantPOS.Domain.Entities;
using RestaurantPOS.Repository.Data;

namespace RestaurantPOS.Web.Data
{
    public static class DbSeeder
    {
        // Change these whenever you want
        private const string MainWaiterName = "Main Waiter";
        private const string MainWaiterPin = "1111";

        public static async Task SeedAsync(ApplicationDbContext db)
        {
            // Ensure DB schema is applied
            await db.Database.MigrateAsync();

            // Seed main waiter if none exist
            if (!await db.Waiters.AnyAsync())
            {
                db.Waiters.Add(new Waiter
                {
                    Id = Guid.NewGuid(),
                    FullName = MainWaiterName,
                    PinCode = MainWaiterPin,
                    IsActive = true
                });

                await db.SaveChangesAsync();
            }

            // Seed tables if none exist
            if (!await db.RestaurantTables.AnyAsync())
            {
                for (int i = 1; i <= 15; i++)
                {
                    db.RestaurantTables.Add(new RestaurantTable
                    {
                        Id = Guid.NewGuid(),
                        TableNumber = i,
                        Status = TableStatus.Free
                    });
                }

                await db.SaveChangesAsync();
            }

            // Seed categories if none exist
            if (!await db.ProductCategories.AnyAsync())
            {
                db.ProductCategories.AddRange(
                    new ProductCategory { Id = Guid.NewGuid(), Name = "Drinks" },
                    new ProductCategory { Id = Guid.NewGuid(), Name = "Food" }
                );

                await db.SaveChangesAsync();
            }
        }
    }
}