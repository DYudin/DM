using DrinkMachine.Repositories;
using DrinkMachine.Repositories.Abstract;
using DrinkMachine.Repositories.Implementation;
using System;

namespace DrinkMachine.Services.Implementation
{
    public class UnitOfWork : IDisposable
    {
        private DrinkMachineContext db = new DrinkMachineContext();
        private IDrinkRepository drinkRepository;
        private IMoneyRepository moneyRepository;

        public IDrinkRepository Drinks
        {
            get
            {
                if (drinkRepository == null)
                    drinkRepository = new DrinkRepository(db);
                return drinkRepository;
            }
        }

        public IMoneyRepository Money
        {
            get
            {
                if (moneyRepository == null)
                    moneyRepository = new MoneyRepository(db);
                return moneyRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}