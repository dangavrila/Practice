using BlazingPizza.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlazingPizza
{
    public class PizzaStoreDbContextFactory : IDesignTimeDbContextFactory<PizzaStoreContext>
    {
        public PizzaStoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreContext>();
            optionsBuilder.UseSqlite("Data Source=pizza.db");

            return new PizzaStoreContext(optionsBuilder.Options);
        }
    }
}
