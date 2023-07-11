namespace Cosmosdbnosql_webapp.Models;

public record Product(
    string id,
    string categoryId,
    string categoryName,
    string sku,
    string name,
    string description,
    decimal price
);