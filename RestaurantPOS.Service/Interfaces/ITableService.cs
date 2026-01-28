using RestaurantPOS.Domain.Entities;

namespace RestaurantPOS.Service.Interfaces
{
    public interface ITableService
    {
        IEnumerable<RestaurantTable> GetAll();
        RestaurantTable? GetById(Guid id);

        void Create(RestaurantTable entity);
        void Update(RestaurantTable entity);
        void Delete(Guid id);
    }
}