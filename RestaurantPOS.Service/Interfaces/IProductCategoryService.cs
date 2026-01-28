using RestaurantPOS.Domain.Entities;

namespace RestaurantPOS.Service.Interfaces
{
    public interface IProductCategoryService
    {
        IEnumerable<ProductCategory> GetAll();
        ProductCategory? GetById(Guid id);

        void Create(ProductCategory entity);
        void Update(ProductCategory entity);
        void Delete(Guid id);
    }
}