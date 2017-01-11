
namespace DrinkMachine.Models
{
    public class Drink : IEntityBase
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public decimal Cost { get; set; }
        public bool IsActive { get; set; }
    }
}