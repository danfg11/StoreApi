using Microsoft.EntityFrameworkCore;
using StoreApi.Data;
using StoreApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreApi.Services
{
    public class StoreService
    {
        private readonly StoreContext _context;

        public StoreService(StoreContext context)
        {
            _context = context;
        }

        // Methods for Store
        public async Task<List<Store>> GetStoresAsync()
        {
            return await _context.Stores.Include(s => s.Rainchecks).ToListAsync();
        }

        public async Task<Store> GetStoreByIdAsync(int id)
        {
            return await _context.Stores.Include(s => s.Rainchecks).FirstOrDefaultAsync(s => s.StoreId == id);
        }

        public async Task<Store> CreateStoreAsync(Store store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task<Store> UpdateStoreAsync(int id, Store store)
        {
            var existingStore = await _context.Stores.FindAsync(id);
            if (existingStore == null)
            {
                return null;
            }

            existingStore.Name = store.Name;
            // Actualizar otras propiedades según sea necesario

            await _context.SaveChangesAsync();
            return existingStore;
        }

        public async Task<bool> DeleteStoreAsync(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return false;
            }

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return true;
        }

        // Additional methods for pagination and selection
        public async Task<List<Store>> GetStoresWithPaginationAsync(int pageSize, int page)
        {
            return await _context.Stores
                .Include(s => s.Rainchecks)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<dynamic>> GetStoresWithPaginationAndSelectionAsync(int pageSize, int page)
        {
            return await _context.Stores
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(store => new { store.StoreId, store.Name })
                .ToListAsync<dynamic>();
        }

        public async Task GenerateStoresAsync(int count)
        {
            var stores = new List<Store>();
            for (int i = 0; i < count; i++)
            {
                stores.Add(new Store { Name = $"Store {i}" });
            }
            _context.Stores.AddRange(stores);
            await _context.SaveChangesAsync();
        }

    }
}
