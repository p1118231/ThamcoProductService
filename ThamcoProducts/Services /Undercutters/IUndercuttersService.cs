using System;

namespace ThamcoProducts.Services.Undercutters;

public interface IUndercuttersService{

    Task<IEnumerable<ProductDto>> GetProductsAsync();
}