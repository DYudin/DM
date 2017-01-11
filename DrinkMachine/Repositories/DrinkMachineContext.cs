
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinkMachine.Models;

namespace DrinkMachine.Repositories
{
    public class DrinkMachineContext : DbContext
    {
        public DrinkMachineContext()
            : base("DrinkMachineDB")
        {
            Initialize();
        }

        public DbSet<Drink> Drinks { get; set; }
        public DbSet<MoneyUnit> MoneyUnits { get; set; }

        private void Initialize()
        {
            if (!Drinks.Any())
            {
                //var user1 = new User("John Smith", "hh@mail.ru") { CurrentBalance = 500, Salt = "6nE3MGQbNRfyvnQ72cE0xQ==", HashedPassword = "LuHEjGKKpu541fUyKiv5QQFFY7j8cu35bJ2Zcf65PZI=" };
                //var user2 = new User("Margaret Dobi", "DobiM@mail.ru") { CurrentBalance = 500, Salt = "kgdvYJ29VhEVxccnQo3mDw==", HashedPassword = "HLwIG/CeMgR5QqTxODnYuLxlhEyVmKvyO+d5Qqho0uw=" };
                //var user3 = new User("Dmitriy Yudin", "exdv@mail.ru") { CurrentBalance = 500, Salt = "7acTAIx7GlqtnRwbWnSQeQ==", HashedPassword = "/C9lLTUlSnOhLwA5RThR/YMyQDaXtXdCU5pw/zjDaGU=" };

                //Users.Add(user1);
                //Users.Add(user2);
                //Users.Add(user3);                   

                SaveChanges();
            }

            if (!MoneyUnits.Any())
            {

            }
        }
    }
}