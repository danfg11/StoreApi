using StoreApi.Models;
using StoreApi.Services;

namespace StoreApi.Endpoints
{
    public static class CategoryEndpoints
    {
        public static void MapCategoryEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/categories");

            // GET: api/categories
            group.MapGet("/", async (CategoryService service) =>
            {
                return Results.Ok(await service.GetCategoriesAsync());
            })
            .WithName("GetCategories")
            .WithTags("Categories");

            // GET: api/categories/{id}
            group.MapGet("/{id}", async (int id, CategoryService service) =>
            {
                var item = await service.GetCategoryByIdAsync(id);
                return item == null ? Results.NotFound() : Results.Ok(item);
            })
            .WithName("GetCategoryById")
            .WithTags("Categories");

            // POST: api/categories
            group.MapPost("/", async (Category category, CategoryService service) =>
            {
                var createdCategory = await service.CreateCategoryAsync(category);
                return Results.Created($"/api/categories/{createdCategory.CategoryId}", createdCategory);
            })
            .WithName("CreateCategory")
            .WithTags("Categories");

            // PUT: api/categories/{id}
            group.MapPut("/{id}", async (int id, Category category, CategoryService service) =>
            {
                var updatedCategory = await service.UpdateCategoryAsync(id, category);
                return updatedCategory == null ? Results.NotFound() : Results.Ok(updatedCategory);
            })
            .WithName("UpdateCategory")
            .WithTags("Categories");

            // DELETE: api/categories/{id}
            group.MapDelete("/{id}", async (int id, CategoryService service) =>
            {
                var deleted = await service.DeleteCategoryAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteCategory")
            .WithTags("Categories");

            group.MapGet("/paged", async (CategoryService service, int pageSize = 10, int page = 0) =>
            {
                var categories = await service.GetCategoriesWithPaginationAsync(pageSize, page);
                return categories.Count > 0 ? Results.Ok(categories) : Results.NotFound();
            })
            .WithName("GetPagedCategories")
            .WithTags("Categories");

            group.MapGet("/paged/select", async (CategoryService service, int pageSize = 10, int page = 0) =>
            {
                var categories = await service.GetCategoriesWithPaginationAndSelectionAsync(pageSize, page);
                return categories.Count > 0 ? Results.Ok(categories) : Results.NotFound();
            })
            .WithName("GetPagedAndSelectedCategories")
            .WithTags("Categories");

            group.MapPost("/generate", async (CategoryService service) =>
            {
                await service.GenerateCategoriesAsync(10000);
                return Results.Ok();
            })
            .WithName("GenerateCategories")
            .WithTags("Categories");
        }
    }
}
