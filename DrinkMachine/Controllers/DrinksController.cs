using AutoMapper;
using DrinkMachine.Models;
using DrinkMachine.Services.Implementation;
using DrinkMachine.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DrinkMachine.Controllers
{
    public class DrinksController : ApiController
    {
        private readonly UnitOfWork unitOfWork;
        public DrinksController()
        {
            unitOfWork = new UnitOfWork();
        }

        //GET api/Movies
        [Route("api/drinks")]
        public IEnumerable<DrinkViewModel> GetDrinks()
        {            
            Mapper.Initialize(cfg => cfg.CreateMap<Drink, DrinkViewModel>());           
            return Mapper.Map<IEnumerable<Drink>, IEnumerable<DrinkViewModel>>(unitOfWork.Drinks.GetAll());            
        }

        [HttpPost]
        public IHttpActionResult BuyDrink(OrderViewModel orderVM)
        {
            // todo проверить
            if (orderVM == null) return BadRequest();
           
            if (ModelState.IsValid)
            {
                OrderViewModel orderToReturn = new OrderViewModel();

                var drink = unitOfWork.Drinks.GetSingle(orderVM.Drink.Id);
                if (drink == null) return NotFound();
                IEnumerable<MoneyUnit> oddMoneyUnits = new List<MoneyUnit>();

                // 1. compare input money with cost
                var userMoney = orderVM.Money.Select(x => x.Dignity).Sum();
                if (userMoney < drink.Cost)
                {
                    ModelState.AddModelError("orderVM.Money", "Недостаточно средств");
                    return BadRequest(ModelState);
                }
                // 2. check odd money existence
                if (userMoney > drink.Cost)
                {
                    var oddMoney = userMoney - drink.Cost;
                    var machineMoney = unitOfWork.Money.FindBy(x => x.IsActive).ToList();
                    var machineMoneySum = machineMoney.Select(x => x.Dignity).Sum();

                    if (machineMoneySum < oddMoney)
                    {
                        // TODO: net sdachi, return money to user
                        orderToReturn = orderVM;
                        orderToReturn.Success = false;
                        return Ok();
                    }
                    // 3. check odd money dignity existence
                    oddMoneyUnits = checkOddMoneyDignityExistence(machineMoney, oddMoney);

                    if (!oddMoneyUnits.Any())
                    {
                        orderToReturn = orderVM;
                        orderToReturn.Success = false;
                        //TODO: net neobhodimogo nominala, return money
                        return Ok(orderToReturn);
                    }
                }

                // 4. Put user money to machine store
                foreach (var moneyItem in orderVM.Money)
                {
                    var coin = unitOfWork.Money.GetSingle(moneyItem.Id);
                    if (coin == null) return NotFound();
                    coin.Count++;
                    unitOfWork.Money.Edit(coin);
                }
                
                // 5. Get machine money and send to user as odd 
                foreach (var moneyItem in oddMoneyUnits)
                {
                    var coin = unitOfWork.Money.GetSingle(moneyItem.Id);
                    if (coin == null) return NotFound();
                    coin.Count = coin.Count - moneyItem.Count;
                    unitOfWork.Money.Edit(coin);
                }
               
                drink.Count--;
                unitOfWork.Drinks.Edit(drink);

                // not needeed
                Mapper.Initialize(cfg => cfg.CreateMap<Drink, DrinkViewModel>());
                orderToReturn.Drink = Mapper.Map<Drink, DrinkViewModel>(drink);

                Mapper.Initialize(cfg => cfg.CreateMap<MoneyUnit, MoneyItemViewModel>());
                orderToReturn.Money = Mapper.Map<IEnumerable<MoneyUnit>, List<MoneyItemViewModel>>(oddMoneyUnits);
                orderToReturn.Success = true;

                unitOfWork.Save();

                return Ok(orderToReturn);
            }

            return BadRequest();
        }      

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        private IEnumerable<MoneyUnit> checkOddMoneyDignityExistence(IEnumerable<MoneyUnit> machineMoney, decimal oddMoney)
        {
            //var tempSpentMoneyDict = new Dictionary<MoneyUnit, int>();
            List<MoneyUnit> sourceMoney = machineMoney.ToList();
            var oddMoneyUnits = sourceMoney.OrderByDescending(x => x.Dignity).ToList();

            foreach (var moneyUnit in oddMoneyUnits)
            {
                // experiment
                moneyUnit.Count = 0;

                // Get count of selected moneyUnit in the store
                var remainingMoneyUnitCount = sourceMoney.Where(x => x.Id == moneyUnit.Id).Select(t => t.Count).FirstOrDefault();
                //int spentMoneyUnitCount = remainingMoneyUnitCount;
                while (oddMoney > moneyUnit.Dignity && oddMoney != 0 && remainingMoneyUnitCount > 0)
                {
                    // decrease count in machine store
                    remainingMoneyUnitCount--;
                    moneyUnit.Count++;
                    // decrease odd money needed to return to user
                    oddMoney = oddMoney - moneyUnit.Dignity;
                }

                ///spentMoneyUnitCount = spentMoneyUnitCount - remainingMoneyUnitCount;
                ///tempSpentMoneyDict.Add(moneyUnit, spentMoneyUnitCount);
            }

            if (oddMoney != 0) oddMoneyUnits.ToList().Clear();
            return oddMoneyUnits;
            //return tempSpentMoneyDict;
        }

        //private bool checkOddMoneyDignityExistence(IEnumerable<MoneyUnit> machineMoney, decimal oddMoney)
        //{
        //    var checkResult = false;
        //    var tempSpentMoneyDict = new Dictionary<decimal, int>();

        //    var dignities = machineMoney.Select(x => x.Dignity).OrderByDescending(x=>x);

        //    foreach (var dignity in dignities)
        //    {
        //        var remainingMachineMoney = machineMoney.Where(x => x.Dignity == dignity).Select(t => t.Count).FirstOrDefault();
        //        int spentMoney = remainingMachineMoney;
        //        while (oddMoney > dignity && oddMoney != 0 && remainingMachineMoney > 0)
        //        {
        //            // decrease count in machine store
        //            remainingMachineMoney--;
        //            // decrease odd money needed to return to user
        //            oddMoney = oddMoney - dignity;
        //        }

        //        spentMoney = spentMoney - remainingMachineMoney;
        //        tempSpentMoneyDict.Add(dignity, spentMoney);
        //    }

        //    if (oddMoney == 0)
        //    {
        //        checkResult = true;
        //        foreach (var t in tempSpentMoneyDict)
        //        {
        //            var editableMoneyUnit = machineMoney.FirstOrDefault(x => x.Dignity == t.Key);
        //            if (editableMoneyUnit != null)
        //            {
        //                editableMoneyUnit.Count = t.Value;
        //                unitOfWork.Money.Edit(editableMoneyUnit);
        //                unitOfWork.Save();
        //            }
        //        }
        //    }

        //    return checkResult;
        //}
    }
}
