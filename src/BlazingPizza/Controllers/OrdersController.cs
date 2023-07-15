using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazingPizza.Data;

namespace BlazingPizza.Controllers;

[Route("orders")]
[ApiController]
public class OrdersController: Controller{
    private readonly IDbContextFactory<PizzaStoreContext> _dbContextFactory;

    public OrdersController(IDbContextFactory<PizzaStoreContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    [HttpGet]
    public ActionResult<IEnumerable<OrderWithStatus>> GetOrders(){
        List<OrderWithStatus> result = new();
        var orders = (IEnumerable<Order>) null;
        using (var context = _dbContextFactory.CreateDbContext()){
            var ordersQuery = context.Orders
            .Include(o => o.Pizzas).ThenInclude(o => o.Special)
            .Include(o => o.Pizzas).ThenInclude(o => o.Toppings).ThenInclude(o => o.Topping)
            .OrderByDescending(o => o.CreatedTime);

            foreach(var order in ordersQuery){
                var orderWithStatus = OrderWithStatus.FromOrder(order);
                result.Add(orderWithStatus);
            }
        }
        return new OkObjectResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<int>> PlaceOrder(Order order)
    {
        order.CreatedTime = DateTime.Now;

        // Enforce existence of Pizza.SpecialId and Topping.ToppingId
        // in the database - prevent the submitter from making up
        // new specials and toppings
        foreach (var pizza in order.Pizzas)
        {
            pizza.SpecialId = pizza.Special.Id;
            pizza.Special = null;
        }

        await using (var context = _dbContextFactory.CreateDbContext()){
            context.Orders.Attach(order);
            await context.SaveChangesAsync();
        }

        return order.OrderId;
    }
}