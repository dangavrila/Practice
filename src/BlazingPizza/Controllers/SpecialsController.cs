using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazingPizza.Data;

namespace BlazingPizza.Controllers;

[Route("specials")]
[ApiController]
public class SpecialsController : Controller
{
    private readonly IDbContextFactory<PizzaStoreContext> _dbContextFactory;

    public SpecialsController(IDbContextFactory<PizzaStoreContext> db)
    {
        _dbContextFactory = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PizzaSpecial>> GetSpecials()
    {
        var result = (IEnumerable<PizzaSpecial>)null;
        using(var context = _dbContextFactory.CreateDbContext())
        {
            result = context.PizzaSpecials.ToList().OrderByDescending(s => s.BasePrice).ToList();
        }

        return new OkObjectResult(result);
    }
}