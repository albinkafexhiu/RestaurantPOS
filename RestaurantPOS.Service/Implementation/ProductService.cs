using RestaurantPOS.Domain.Entities;
using RestaurantPOS.Repository.Interfaces;
using RestaurantPOS.Service.Interfaces;

namespace RestaurantPOS.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product? GetById(Guid id)
        {
            return _productRepository.Get(id);
        }

        public void Create(Product entity)
        {
            _productRepository.Insert(entity);
        }

        public void Update(Product entity)
        {
            _productRepository.Update(entity);
        }

        public void Delete(Guid id)
        {
            var product = _productRepository.Get(id);
            if (product != null)
            {
                _productRepository.Delete(product);
            }
        }
    }
}