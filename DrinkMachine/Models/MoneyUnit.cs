using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkMachine.Models
{
    public class MoneyUnit : IEntityBase
    {
        public string Sign { get; set; }
        public int Id { get; set; }
        public decimal Dignity { get; set; }        
        public string Description { get; set; }
        public int Count { get; set; }
        public bool IsActive { get; set; }
    }
}