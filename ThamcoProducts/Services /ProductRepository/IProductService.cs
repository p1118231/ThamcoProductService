using System;

namespace ThamcoProducts.Services.ProductRespository;

public interface IProductService{

    Task<IEnumerable<Product>> GetProductsAsync();
}
