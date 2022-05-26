namespace PizzaOnline.Models
{
    public class Pizza
    {
        public int PizzaId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageName { get; set; } = "error.jpg";

        public List<OrderPizzas> OrderPizzas { get; set; } = new List<OrderPizzas>();
    }
}
