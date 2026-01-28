using RestaurantPOS.Domain.Entities;
using RestaurantPOS.Repository.Interfaces;
using RestaurantPOS.Service.Interfaces;

namespace RestaurantPOS.Service.Implementation
{
    public class ExpenseService : IExpenseService
    {
        private readonly IRepository<Expense> _expenseRepository;

        public ExpenseService(IRepository<Expense> expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public IEnumerable<Expense> GetAll()
        {
            return _expenseRepository.GetAll();
        }

        public Expense? GetById(Guid id)
        {
            return _expenseRepository.Get(id);
        }

        public void Create(Expense entity)
        {
            _expenseRepository.Insert(entity);
        }

        public void Update(Expense entity)
        {
            _expenseRepository.Update(entity);
        }

        public void Delete(Guid id)
        {
            var expense = _expenseRepository.Get(id);
            if (expense != null)
            {
                _expenseRepository.Delete(expense);
            }
        }
    }
}