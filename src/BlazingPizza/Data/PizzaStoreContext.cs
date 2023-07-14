using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Data;

public class PizzaStoreContext : DbContext
{
    public string DbPath { get; }

    public PizzaStoreContext(DbContextOptions<PizzaStoreContext> options) : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "pizza.db");
    }

    public virtual DbSet<PizzaSpecial> PizzaSpecials { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PizzaSpecial>()
            .ToTable("PizzaSpecials");

        base.OnModelCreating(modelBuilder);
    }
}