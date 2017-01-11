

using DrinkMachine.Models;

namespace DrinkMachine.Repositories.Abstract
{
    public interface IDrinkRepository : IEntityRepository<Drink> { }

    public interface IMoneyRepository : IEntityRepository<MoneyUnit> { }
}
