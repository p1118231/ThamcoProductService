using System;
using System.Net;

namespace ThamcoProducts.Services.Undercutters;

public class UndercuttersService : IUndercuttersService
{

    private readonly HttpClient _client;

        public UndercuttersService(HttpClient client, IConfiguration configuration)
        {
            // FIXME: don't hardcode base URLs
            var baseUrl = configuration["WebServices:Undercutters:BaseURL"] ?? "";
            client.BaseAddress = new System.Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(10);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = client;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var uri = "api/Product"; 
            try
            {
                var response = await _client.GetAsync(uri);
                response.EnsureSuccessStatusCode();  // Throws if not a successful status code
                var products = await response.Content.ReadAsAsync<IEnumerable<ProductDto>>();
                return products;
            }
            catch (Exception)
            {
                return Array.Empty<ProductDto>();  // Return an empty list if any exception occurs
            }
        }
}