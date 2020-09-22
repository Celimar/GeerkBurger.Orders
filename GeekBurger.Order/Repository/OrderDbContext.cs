using Microsoft.EntityFrameworkCore;

namespace GeekBurger.Order.Repository
{
    public class OrderDbContext : DbContext
    {

        public OrderDbContext(DbContextOptions<OrderDbContext> options): base(options)
        {
        }

        public DbSet<Model.Order> Orders { get; set; }
        public DbSet<Model.Payment> Payments { get; set; }
        public DbSet<Model.Product> Products { get; set; }
        public DbSet<Model.Store> Stores { get; set; }
        public DbSet<Model.OrderChangedEvent> OrderChangedEvents { get; set; }

    }
}
