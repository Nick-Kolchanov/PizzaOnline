using Microsoft.EntityFrameworkCore;
using PizzaOnline.Models;

namespace PizzaOnline.Data
{
    public class PizzaDbContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; } = null!;
        public DbSet<OrderPizzas> OrderPizzas { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
        : base(options)
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderPizzas>().HasKey(u => new { u.OrderId, u.PizzaId });
            modelBuilder.Entity<OrderPizzas>()
                .HasOne(u => u.Pizza)
                .WithMany(p => p.OrderPizzas)
                .HasForeignKey(u => u.PizzaId);
            modelBuilder.Entity<OrderPizzas>()
                .HasOne(u => u.Order)
                .WithMany(p => p.OrderPizzas)
                .HasForeignKey(u => u.OrderId);

            modelBuilder.Entity<Pizza>().HasData(
                    new Pizza { PizzaId = 1, Name = "Пепперони", Description = "Cыры Моцарелла и Пармезан, шампиньоны, бекон, колбаса пепперони, помидоры, куриная грудка, чеснок, лук красный, зелень.", 
                                Price = 220, ImageName = "Pepperoni.jpg" },
                    new Pizza { PizzaId = 2, Name = "Маргарита", Description = "Cыр Моцарелла, помидоры.", Price = 200, ImageName = "Margarita.jpg" },
                    new Pizza { PizzaId = 3, Name = "Гавайская", Description = "Cыр Моцарелла, ветчина, ананасы.", Price = 200, ImageName = "/Нawaiian.jpg" }
            );
        }
    }
}
