using Microsoft.EntityFrameworkCore;
using StoreApi.Data;
using StoreApi.Models;

namespace StoreApi.Services
{
    public class CategoryService
    {
        private readonly StoreContext _context;

        public CategoryService(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            category.CategoryGuid = Guid.NewGuid(); // Asignar Guid único
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.ImageUrl = category.ImageUrl;

            await _context.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Category>> GetCategoriesWithPaginationAsync(int pageSize, int page)
        {
            return await _context.Categories
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<dynamic>> GetCategoriesWithPaginationAndSelectionAsync(int pageSize, int page)
        {
            return await _context.Categories
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(category => new { category.CategoryId, category.Name })
                .ToListAsync<dynamic>();
        }

        public async Task GenerateCategoriesAsync(int count)
        {
            var categories = new List<Category>();
            for (int i = 0; i < count; i++)
            {
                categories.Add(new Category
                {
                    CategoryGuid = Guid.NewGuid(), // Asignar Guid único
                    Name = $"Category {i}",
                    Description = $"Description {i}"
                });
            }
            _context.Categories.AddRange(categories);
            await _context.SaveChangesAsync();
        }
    }
}
