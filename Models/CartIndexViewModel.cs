namespace PizzaOnline.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; } = null!;
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
