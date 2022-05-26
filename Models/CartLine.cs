namespace PizzaOnline.Models
{
    public class CartLine
    {
        public Pizza Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
