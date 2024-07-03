using StoreApi.Data;
using StoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreApi.Services
{
    public class ProductService
    {
        private readonly StoreContext _context;

        public ProductService(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product.ProductGuid = Guid.NewGuid(); // Asignar Guid único
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.SkuNumber = product.SkuNumber;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.RecommendationId = product.RecommendationId;
            existingProduct.Title = product.Title;
            existingProduct.Price = product.Price;
            existingProduct.SalePrice = product.SalePrice;
            existingProduct.ProductArtUrl = product.ProductArtUrl;
            existingProduct.Description = product.Description;
            existingProduct.Created = product.Created;
            existingProduct.ProductDetails = product.ProductDetails;
            existingProduct.Inventory = product.Inventory;
            existingProduct.LeadTime = product.LeadTime;

            await _context.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> GetProductsWithPaginationAsync(int pageSize, int page)
        {
            return await _context.Products
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<dynamic>> GetProductsWithPaginationAndSelectionAsync(int pageSize, int page)
        {
            return await _context.Products
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(product => new { product.ProductId, product.Title })
                .ToListAsync<dynamic>();
        }

        public async Task GenerateProductsAsync(int count)
        {
            var categories = await _context.Categories.ToListAsync();
            var random = new Random();

            if (categories.Count == 0) throw new Exception("No categories available to assign to products.");

            var products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                products.Add(new Product
                {
                    ProductGuid = Guid.NewGuid(), // Asignar Guid único
                    SkuNumber = $"SKU{i}",
                    CategoryId = categories[random.Next(categories.Count)].CategoryId,
                    RecommendationId = i + 1,
                    Title = $"Product {i}",
                    Price = random.Next(10, 500),
                    SalePrice = random.Next(5, 250),
                    ProductArtUrl = $"https://example.com/product{i}.jpg",
                    Description = $"Description for product {i}",
                    Created = DateTime.Now,
                    ProductDetails = $"Details for product {i}",
                    Inventory = random.Next(1, 100),
                    LeadTime = random.Next(1, 30)
                });
            }
            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();
        }
    }
}
