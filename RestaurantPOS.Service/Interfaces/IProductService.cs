using RestaurantPOS.Domain.Entities;

namespace RestaurantPOS.Service.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product? GetById(Guid id);

        void Create(Product entity);
        void Update(Product entity);
        void Delete(Guid id);
    }
}