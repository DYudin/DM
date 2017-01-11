using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DrinkMachine.ViewModels
{
    public class MoneyItemViewModel
    {
        [Required(ErrorMessage = "Укажите обозначение монеты")]
        public string Sign { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "Укажите номинал монеты")]
        //[RegularExpression(@"[1,2,5,10]", ErrorMessage = "Некорректный номинал")]
        public decimal Dignity { get; set; }
        [Required(ErrorMessage = "Укажите число монет")]
        public int Count { get; set; }
        public string Description { get; set; } 
    }
}