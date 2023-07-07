using Cosmosdbnosql_webapp.Models;

namespace Cosmosdbnosql_webapp.Services;

public interface ICosmosService
{
    Task<IEnumerable<Product>> RetrieveActiveProductsAsync();

    Task<IEnumerable<Product>> RetrieveAllProductsAsync();
}