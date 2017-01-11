using DrinkMachine.Models;
using DrinkMachine.Repositories.Abstract;

namespace DrinkMachine.Repositories.Implementation
{
    public class MoneyRepository : EntityRepository<MoneyUnit>, IMoneyRepository
    {
        public MoneyRepository(DrinkMachineContext context)
            : base(context)
        {
        }
    }
}