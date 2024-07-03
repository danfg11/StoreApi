using StoreApi.Data;
using StoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreApi.Services
{
    public class CartItemService
    {
        private readonly StoreContext _context;

        public CartItemService(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            return await _context.CartItems.ToListAsync();
        }

        public async Task<CartItem> GetCartItemByIdAsync(int id)
        {
            return await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartItemId == id);
        }

        public async Task<CartItem> CreateCartItemAsync(CartItem cartItem)
        {
            cartItem.CartItemGuid = Guid.NewGuid(); // Asignar Guid único
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<CartItem> UpdateCartItemAsync(int id, CartItem cartItem)
        {
            var existingCartItem = await _context.CartItems.FindAsync(id);
            if (existingCartItem == null)
            {
                return null;
            }

            existingCartItem.CartId = cartItem.CartId;
            existingCartItem.ProductId = cartItem.ProductId;
            existingCartItem.Count = cartItem.Count;
            existingCartItem.DateCreated = cartItem.DateCreated;

            await _context.SaveChangesAsync();
            return existingCartItem;
        }

        public async Task<bool> DeleteCartItemAsync(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return false;
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CartItem>> GetCartItemsWithPaginationAsync(int pageSize, int page)
        {
            return await _context.CartItems
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<dynamic>> GetCartItemsWithPaginationAndSelectionAsync(int pageSize, int page)
        {
            return await _context.CartItems
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(cartItem => new { cartItem.CartItemId, cartItem.ProductId })
                .ToListAsync<dynamic>();
        }

        public async Task GenerateCartItemsAsync(int count)
        {
            var products = await _context.Products.ToListAsync();
            if (products.Count == 0) throw new Exception("No products available to assign to cart items.");

            var cartItems = new List<CartItem>();
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                cartItems.Add(new CartItem
                {
                    CartItemGuid = Guid.NewGuid(), // Asignar Guid único
                    CartId = $"Cart{i}",
                    ProductId = products[random.Next(products.Count)].ProductId,
                    Count = random.Next(1, 5),
                    DateCreated = DateTime.Now
                });
            }
            _context.CartItems.AddRange(cartItems);
            await _context.SaveChangesAsync();
        }
    }
}
