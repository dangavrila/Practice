using Humanizer;
using Microsoft.Azure.Cosmos;

public static class CosmosHandler {
    private static readonly CosmosClient _client;

    static CosmosHandler()
    {
        _client = new CosmosClient(
            accountEndpoint: @"https://docdb4practice.documents.azure.com:443/", 
            authKeyOrResourceToken: @"Q7j5N4dzBVHlPfJMWDqSb66zaWdlLOhBizzUQOvUt7rXhsYjSJk3HmMqF8slK8IZzkAvKFrJ6vkpACDbWBFNqw=="
        );
    }

    public static async Task ManageCustomerAsync(string name, string email, string state, string country)
    {
        Container container = await GetContainer();
        string id = name.Kebaberize();
        
        /*var customer = new {
            id = id,
            name = name,
            address = new {
                state = state,
                country = country
            }
        };

        var response = await container.CreateItemAsync(customer);*/

        /*string sql = """
        SELECT
            *
        FROM customers c
        WHERE c.id = @id
        """;

        var query = new QueryDefinition(
            query: sql
        ).WithParameter("@id", id);

        using var feed = container.GetItemQueryIterator<dynamic>(
            queryDefinition: query
        );

        var response = await feed.ReadNextAsync();*/

        var partitionKey = new PartitionKeyBuilder()
            .Add(country)
            .Add(state)
            .Build();

        /*var response = await container.ReadItemAsync<dynamic>(
            id: id, 
            partitionKey: partitionKey
        );*/

        var customerCart = new {
            id = $"{Guid.NewGuid()}",
            customerId = id,
            items = new string[] {},
            address = new {
                state = state,
                country = country
            }
        };

        var customerContactInfo = new {
            id = $"{id}-contact",
            customerId = id,
            email = email,
            location = $"{state}, {country}",
            address = new {
                state = state,
                country = country
            }
        };

        var batch = container.CreateTransactionalBatch(partitionKey)
            .ReadItem(id)
            .CreateItem(customerCart)
            .CreateItem(customerContactInfo);

        using var response = await batch.ExecuteAsync();

        Console.WriteLine($"[{response.StatusCode}]\t{id}\t{response.RequestCharge} RUs");
    }

    private static async Task<Container> GetContainer()
    {
        Database database = _client.GetDatabase("cosmicworks");
        List<string> keyPaths = new()
        {
            "/address/country",
            "/address/state"
        };

        ContainerProperties properties = new(
            id: "customers",
            partitionKeyPaths: keyPaths
        );

        return await database.CreateContainerIfNotExistsAsync(
            containerProperties: properties
        );
    }
}