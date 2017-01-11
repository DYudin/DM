using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DrinkMachine.ViewModels
{
    public class DrinkViewModel
    {
        [Required(ErrorMessage = "Укажите название напитка")]
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Укажите количество напитка")]
        public int Count { get; set; }
        [Required(ErrorMessage = "Укажите стоимость напитка")]
        [Range(5, 50, ErrorMessage = "Стоимость напитка должна быть между 5 и 50")]
        public decimal Cost { get; set; }       
    }
}