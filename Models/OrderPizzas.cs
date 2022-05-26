namespace PizzaOnline.Models
{
    public class OrderPizzas
    {
        public int OrderId { get; set; }
        public int PizzaId { get; set; }
        public int Amount { get; set; }

        public Pizza Pizza { get; set; } = null!;
        public Order Order { get; set; } = null!;
    }
}
