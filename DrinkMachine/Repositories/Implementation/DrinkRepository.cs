using DrinkMachine.Models;
using DrinkMachine.Repositories.Abstract;

namespace DrinkMachine.Repositories.Implementation
{
     public class DrinkRepository : EntityRepository<Drink>, IDrinkRepository
    {
        DrinkMachineContext _context;

        public DrinkRepository(DrinkMachineContext context)
            : base(context)
        {
        }
    }
}