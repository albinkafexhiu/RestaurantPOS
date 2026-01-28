using RestaurantPOS.Domain.Entities;
using RestaurantPOS.Repository.Interfaces;
using RestaurantPOS.Service.Interfaces;

namespace RestaurantPOS.Service.Implementation
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IRepository<ProductCategory> _categoryRepository;

        public ProductCategoryService(IRepository<ProductCategory> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public ProductCategory? GetById(Guid id)
        {
            return _categoryRepository.Get(id);
        }

        public void Create(ProductCategory entity)
        {
            _categoryRepository.Insert(entity);
        }

        public void Update(ProductCategory entity)
        {
            _categoryRepository.Update(entity);
        }

        public void Delete(Guid id)
        {
            var category = _categoryRepository.Get(id);
            if (category != null)
            {
                _categoryRepository.Delete(category);
            }
        }
    }
}