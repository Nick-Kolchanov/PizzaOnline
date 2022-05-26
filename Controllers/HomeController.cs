using Microsoft.AspNetCore.Mvc;
using PizzaOnline.Data;
using PizzaOnline.Models;
using System.Diagnostics;

namespace PizzaOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PizzaDbContext _context;

        public HomeController(ILogger<HomeController> logger, PizzaDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Pizza> pizzas = _context.Pizzas;
            return View(pizzas);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}