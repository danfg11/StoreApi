using StoreWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreWebApp.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync(int page);
        Task AddProductToCart(Product product);
        Task<IEnumerable<Product>> GetCartProductsAsync();
    }
}
