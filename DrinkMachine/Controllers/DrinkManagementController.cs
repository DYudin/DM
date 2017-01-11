using AutoMapper;
using DrinkMachine.Models;
using DrinkMachine.Services.Implementation;
using DrinkMachine.ViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace DrinkMachine.Controllers
{
    public class DrinkManagementController : ApiController
    {
        private readonly UnitOfWork unitOfWork;
        public DrinkManagementController()
        {
            unitOfWork = new UnitOfWork();
        }
        public IEnumerable<DrinkViewModel> GetDrinks()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Drink, DrinkViewModel>());
            return Mapper.Map<IEnumerable<Drink>, IEnumerable<DrinkViewModel>>(unitOfWork.Drinks.GetAll());
        }

        [HttpPost]
        public void AddDrink(DrinkViewModel drinkVM)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DrinkViewModel, Drink>());
                var drink = Mapper.Map<DrinkViewModel, Drink>(drinkVM);

                unitOfWork.Drinks.Add(drink);
                unitOfWork.Save();
            }
        }

        [HttpPost]
        public void RemoveDrink(DrinkViewModel drinkVM)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DrinkViewModel, Drink>());
                var drink = Mapper.Map<DrinkViewModel, Drink>(drinkVM);

                unitOfWork.Drinks.Delete(drink);
                unitOfWork.Save();
            }
        }

        [HttpPost]
        public void ChangeDrink(DrinkViewModel drinkVM)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DrinkViewModel, Drink>());
                var drink = Mapper.Map<DrinkViewModel, Drink>(drinkVM);

                unitOfWork.Drinks.Edit(drink);
                unitOfWork.Save();
            }
        }

        [HttpPost]
        public void ChangeMoneyUnitAvailability(MoneyItemViewModel moneyUnitVM)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<MoneyItemViewModel, MoneyUnit>());
                var moneyUnit = Mapper.Map<MoneyItemViewModel, MoneyUnit>(moneyUnitVM);

                unitOfWork.Money.Edit(moneyUnit);
                unitOfWork.Save();
            }
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
