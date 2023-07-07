using Cosmosdbnosql_webapp.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace Cosmosdbnosql_webapp.Services;

public class CosmosService : ICosmosService
{
    private readonly CosmosClient _client;

    public CosmosService(){
        _client = new CosmosClient(
            connectionString: ""
        );
    }

    public async Task<IEnumerable<Product>> RetrieveActiveProductsAsync(){
        string sql = """
            SELECT
                p.id,
                p.categoryId,
                p.categoryName,
                p.sku,
                p.name,
                p.description,
                p.price,
                p.tags
            FROM product p
            JOIN t IN p.tags
            WHERE t.name = @tagFilter
            """;
        var query = new QueryDefinition(query: sql).WithParameter("@tagFilter", "Tag-75");
        using FeedIterator<Product> feed = container.GetItemQueryIterator<Product>(
            queryDefinition: query
        );

        List<Product> results = new();

        while (feed.HasMoreResults)
        {
            FeedResponse<Product> response = await feed.ReadNextAsync();
            foreach (Product item in response)
            {
                results.Add(item);
            }
        }

        return results;
    }

    public async Task<IEnumerable<Product>> RetrieveAllProductsAsync(){
        var queryable = container.GetItemLinqQueryable<Product>();

        using FeedIterator<Product> feed = queryable
            .Where(p => p.price < 2000m)
            .OrderByDescending(p => p.price)
            .ToFeedIterator();

        List<Product> results = new();
        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            foreach (Product item in response)
            {
                results.Add(item);
            }
        }

        return results;
    }

    private Container container
    {
        get => _client.GetDatabase("cosmicworks").GetContainer("product");
    }
}