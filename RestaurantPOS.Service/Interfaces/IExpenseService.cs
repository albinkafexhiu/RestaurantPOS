using RestaurantPOS.Domain.Entities;

namespace RestaurantPOS.Service.Interfaces
{
    public interface IExpenseService
    {
        IEnumerable<Expense> GetAll();
        Expense? GetById(Guid id);

        void Create(Expense entity);
        void Update(Expense entity);
        void Delete(Guid id);
    }
}