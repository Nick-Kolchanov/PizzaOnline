using Microsoft.AspNetCore.Mvc;
using PizzaOnline.Data;
using PizzaOnline.Models;
using PizzaOnline.Utils;
using Microsoft.EntityFrameworkCore;

namespace PizzaOnline.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly PizzaDbContext _context;

        public CartController(ILogger<CartController> logger, PizzaDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public ViewResult Orders()
        {
            return View(_context.OrderPizzas.Include(o => o.Order).Include(o => o.Pizza).ToList());
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Pizza? pizza = _context.Pizzas.FirstOrDefault(p => p.PizzaId == productId);
            if (pizza != null)
            {
                var cart = GetCart();
                cart.AddItem(pizza, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        [HttpPost]
        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Pizza? pizza = _context.Pizzas.FirstOrDefault(p => p.PizzaId == productId);
            if (pizza != null)
            {
                var cart = GetCart();
                cart.RemoveLine(pizza);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public async Task<RedirectToActionResult> ProceedPurchase(string returnUrl)
        {
            var cart = GetCart();

            if (!cart.Lines.Any())
                return RedirectToAction("Index", new { returnUrl });

            var entry = _context.Orders.Add(new Order() { CreatedDate = DateTime.UtcNow });
            await _context.SaveChangesAsync();

            int orderId = entry.Entity.OrderId;
            List<OrderPizzas> orders = new List<OrderPizzas>();
            foreach (var line in cart.Lines)
            {
                orders.Add(new OrderPizzas() { PizzaId = line.Product.PizzaId, OrderId = orderId, Amount = line.Quantity });
            }
            _context.OrderPizzas.AddRange(orders);
            await _context.SaveChangesAsync();

            cart.Clear();
            SaveCart(cart);

            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            return HttpContext.Session.GetObjectFromJson<Cart>("Cart") ?? new Cart();
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetObjectAsJson("Cart", cart);
        }
    }
}
