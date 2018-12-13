using Microsoft.EntityFrameworkCore;

namespace Project1.DataAccess
{
    public class Project1Context : DbContext
    {
        public Project1Context(DbContextOptions<Project1Context> options) : base(options)
        { }

        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<OrderPizzas> OrderPizzas { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Pizzas> Pizzas { get; set; }
        public DbSet<PizzasIngredients> PizzasIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // using dataannotations for now
        }
    }
}
