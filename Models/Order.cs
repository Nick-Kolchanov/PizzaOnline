namespace PizzaOnline.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<OrderPizzas> OrderPizzas { get; set; } = new List<OrderPizzas>();
    }
}
