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
    public virtual DbSet<Order> Orders {get;set;}
    public virtual DbSet<Pizza> Pizzas { get; set; }
    public virtual DbSet<Topping> Toppings {get;set;}
    public virtual DbSet<Address> Addresses { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PizzaSpecial>()
            .ToTable("PizzaSpecials");
        
        // Configuring a many-to-many special -> topping
        modelBuilder.Entity<PizzaTopping>()
            .HasKey("PizzaId", "ToppingId");
        modelBuilder.Entity<PizzaTopping>()
            .HasOne<Pizza>()
            .WithMany(ps => ps.Toppings);
        modelBuilder.Entity<PizzaTopping>()
            .HasOne(pst => pst.Topping)
            .WithMany();

        /*modelBuilder.Entity<Address>()
            .HasMany(a => a.Orders)
            .WithOne(o => o.DeliveryAddress)
            .HasForeignKey(o => o.DeliveryAddressId);*/

        modelBuilder.Entity<Order>()
            .HasOne(o => o.DeliveryAddress)
            .WithMany(da => da.Orders)
            .HasForeignKey(o => o.DeliveryAddressId)
            .IsRequired(false);
    }
}