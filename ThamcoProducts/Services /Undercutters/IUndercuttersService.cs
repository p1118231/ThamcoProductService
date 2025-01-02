using System;

namespace ThamcoProducts.Services.Undercutters;

//Interface for getting products

public interface IUndercuttersService{

    Task<IEnumerable<ProductDto>> GetProductsAsync();
}