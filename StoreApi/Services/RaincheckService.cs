using StoreApi.Data;
using StoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreApi.Services
{
    public class RaincheckService
    {
        private readonly StoreContext _context;

        public RaincheckService(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<Raincheck>> GetRainchecksAsync()
        {
            return await _context.Rainchecks.Include(r => r.Product).Include(r => r.Store).ToListAsync();
        }

        public async Task<Raincheck> GetRaincheckByIdAsync(int id)
        {
            return await _context.Rainchecks.Include(r => r.Product).Include(r => r.Store).FirstOrDefaultAsync(r => r.RaincheckId == id);
        }

        public async Task<Raincheck> CreateRaincheckAsync(Raincheck raincheck)
        {
            _context.Rainchecks.Add(raincheck);
            await _context.SaveChangesAsync();
            return raincheck;
        }

        public async Task<Raincheck> UpdateRaincheckAsync(int id, Raincheck raincheck)
        {
            var existingRaincheck = await _context.Rainchecks.FindAsync(id);
            if (existingRaincheck == null)
            {
                return null;
            }

            existingRaincheck.Name = raincheck.Name;
            existingRaincheck.ProductId = raincheck.ProductId;
            existingRaincheck.Count = raincheck.Count;
            existingRaincheck.SalePrice = raincheck.SalePrice;
            existingRaincheck.StoreId = raincheck.StoreId;

            await _context.SaveChangesAsync();
            return existingRaincheck;
        }

        public async Task<bool> DeleteRaincheckAsync(int id)
        {
            var raincheck = await _context.Rainchecks.FindAsync(id);
            if (raincheck == null)
            {
                return false;
            }

            _context.Rainchecks.Remove(raincheck);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Raincheck>> GetRainchecksWithPaginationAsync(int pageSize, int page)
        {
            return await _context.Rainchecks
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<dynamic>> GetRainchecksWithPaginationAndSelectionAsync(int pageSize, int page)
        {
            return await _context.Rainchecks
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(raincheck => new { raincheck.RaincheckId, raincheck.ProductId, raincheck.StoreId })
                .ToListAsync<dynamic>();
        }

        public async Task<List<dynamic>> GetRainchecksWithPaginationAndSelectionAsync2(int pageSize, int page)
        {
            return await _context.Rainchecks
                .Include(r => r.Store)
                .Include(r => r.Product)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(raincheck => new
                {
                    StoreName = raincheck.Store.Name,
                    ProductName = raincheck.Product.Title
                })
                .ToListAsync<dynamic>();
        }

        public async Task GenerateRainchecksAsync(int count)
        {
            var stores = await _context.Stores.ToListAsync();
            var products = await _context.Products.ToListAsync();
            if (stores.Count == 0) throw new Exception("No stores available to assign to rainchecks.");
            if (products.Count == 0) throw new Exception("No products available to assign to rainchecks.");

            var rainchecks = new List<Raincheck>();
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                rainchecks.Add(new Raincheck
                {
                    StoreId = stores[random.Next(stores.Count)].StoreId,
                    ProductId = products[random.Next(products.Count)].ProductId,
                    Count = random.Next(1, 5),
                    SalePrice = random.Next(5, 250),
                    Name = $"Raincheck {i}"
                });
            }
            _context.Rainchecks.AddRange(rainchecks);
            await _context.SaveChangesAsync();
        }


    }
}
