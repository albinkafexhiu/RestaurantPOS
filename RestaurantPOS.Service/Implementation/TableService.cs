using RestaurantPOS.Domain.Entities;
using RestaurantPOS.Repository.Interfaces;
using RestaurantPOS.Service.Interfaces;

namespace RestaurantPOS.Service.Implementation
{
    public class TableService : ITableService
    {
        private readonly IRepository<RestaurantTable> _tableRepository;

        public TableService(IRepository<RestaurantTable> tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public IEnumerable<RestaurantTable> GetAll()
        {
            return _tableRepository.GetAll();
        }

        public RestaurantTable? GetById(Guid id)
        {
            return _tableRepository.Get(id);
        }

        public void Create(RestaurantTable entity)
        {
            _tableRepository.Insert(entity);
        }

        public void Update(RestaurantTable entity)
        {
            _tableRepository.Update(entity);
        }

        public void Delete(Guid id)
        {
            var table = _tableRepository.Get(id);
            if (table != null)
            {
                _tableRepository.Delete(table);
            }
        }
    }
}