using System;

namespace ThamcoProducts.Services.ProductRespository;

public interface IProductSerivce{

    Task<IEnumerable<Product>> GetProductsAsync();
}