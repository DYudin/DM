
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrinkMachine.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public bool Success { get; set; }

        [Required]
        public DrinkViewModel Drink { get; set; }
        //public decimal Cost { get; set; }
        [Required]
        public IEnumerable<MoneyItemViewModel> Money { get; set; }
    }
}