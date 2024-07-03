using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using StoreWebApp.Models;
using System.Text.Json.Serialization;

namespace StoreWebApp.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private List<Product> _cart = new List<Product>();
        private const int PageSize = 10;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetProductsAsync(int page)
        {
            var url = $"api/products/paged/select?page={page}";
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() },
                AllowTrailingCommas = true
            };

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<Product>>(content, options);
            return products ?? new List<Product>();
        }

        public Task AddProductToCart(Product product)
        {
            _cart.Add(product);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Product>> GetCartProductsAsync()
        {
            return Task.FromResult<IEnumerable<Product>>(_cart);
        }
    }
}
