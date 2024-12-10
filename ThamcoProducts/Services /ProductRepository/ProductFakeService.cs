using System;

namespace ThamcoProducts.Services.ProductRespository;

public class ProductFakeService : IProductSerivce
{

    private readonly Product[]  _products =
    {
    new Product  {Id = 1, Name = "Fake product A" },
    new Product { Id = 2, Name = "Fake product B" },
    new Product{Id = 3, Name = "Fake product C"}
    };

    public Task<IEnumerable<Product>> GetProductsAsync()
    {
        var products = _products.AsEnumerable();
        return Task.FromResult(products);
    }
}