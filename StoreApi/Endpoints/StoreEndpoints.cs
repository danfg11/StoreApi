using StoreApi.Models;
using StoreApi.Services;

public static class StoreEndpoints
{
    public static void MapStoreEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/stores");

        // GET: api/stores
        group.MapGet("/", async (StoreService service) =>
        {
            return Results.Ok(await service.GetStoresAsync());
        })
        .WithName("GetStores")
        .WithTags("Stores");

        // GET: api/stores/{id}
        group.MapGet("/{id}", async (int id, StoreService service) =>
        {
            var store = await service.GetStoreByIdAsync(id);
            return store == null ? Results.NotFound() : Results.Ok(store);
        })
        .WithName("GetStoreById")
        .WithTags("Stores");

        // POST: api/stores
        group.MapPost("/", async (Store store, StoreService service) =>
        {
            var createdStore = await service.CreateStoreAsync(store);
            return Results.Created($"/api/stores/{createdStore.StoreId}", createdStore);
        })
        .WithName("CreateStore")
        .WithTags("Stores");

        // PUT: api/stores/{id}
        group.MapPut("/{id}", async (int id, Store store, StoreService service) =>
        {
            var updatedStore = await service.UpdateStoreAsync(id, store);
            return updatedStore == null ? Results.NotFound() : Results.Ok(updatedStore);
        })
        .WithName("UpdateStore")
        .WithTags("Stores");

        // DELETE: api/stores/{id}
        group.MapDelete("/{id}", async (int id, StoreService service) =>
        {
            var deleted = await service.DeleteStoreAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteStore")
        .WithTags("Stores");

        // Additional endpoints for pagination and selection
        // Endpoint para obtener todas las tiendas con paginación
        group.MapGet("/paged", async (StoreService service, int pageSize = 10, int page = 0) =>
        {
            var stores = await service.GetStoresWithPaginationAsync(pageSize, page);
            return stores.Count > 0 ? Results.Ok(stores) : Results.NotFound();
        })
        .WithName("GetPagedStores")
        .WithTags("Stores");

        // Endpoint para obtener tiendas con paginación y selección
        group.MapGet("/paged/select", async (StoreService service, int pageSize = 10, int page = 0) =>
        {
            var stores = await service.GetStoresWithPaginationAndSelectionAsync(pageSize, page);
            return stores.Count > 0 ? Results.Ok(stores) : Results.NotFound();
        })
        .WithName("GetPagedAndSelectedStores")
        .WithTags("Stores");

        // Endpoint para generar 10,000 Stores
        group.MapPost("/generate", async (StoreService service) =>
        {
            await service.GenerateStoresAsync(10000);
            return Results.Ok();
        })
        .WithName("GenerateStores")
        .WithTags("Stores");
    }
}
