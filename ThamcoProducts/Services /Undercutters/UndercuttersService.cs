using System;
using System.Net;

namespace ThamcoProducts.Services.Undercutters;

public class UndercutterService : IUndercuttersService
{

    private readonly HttpClient _client;

        public UndercutterService(HttpClient client, IConfiguration configuration)
        {
            // FIXME: don't hardcode base URLs
            var baseUrl = configuration["Webservices:Undercutters:BaseURL"] ?? "";
            client.BaseAddress = new System.Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = client;
        }

        public async Task<ProductDto> GetProductAsync(int id)
        {
            var response = await _client.GetAsync("api/product/" + id);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            var product = await response.Content.ReadAsAsync<ProductDto>();
            return product;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var uri = "api/product";
            var response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadAsAsync<IEnumerable<ProductDto >>();
            return products;
        }
}