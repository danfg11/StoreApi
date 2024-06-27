using StoreApi.Models;
using StoreApi.Services;
using StoreApi.Data;
using System;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StoreApi.Endpoints
{
    public static class RaincheckEndpoints
    {
        public static void MapRaincheckEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/rainchecks");

            // GET: api/rainchecks
            group.MapGet("/", async (RaincheckService service) =>
            {
                return Results.Ok(await service.GetRainchecksAsync());
            })
            .WithName("GetRainchecks")
            .WithTags("Rainchecks");

            // GET: api/rainchecks/{id}
            group.MapGet("/{id}", async (int id, RaincheckService service) =>
            {
                var item = await service.GetRaincheckByIdAsync(id);
                return item == null ? Results.NotFound() : Results.Ok(item);
            })
            .WithName("GetRaincheckById")
            .WithTags("Rainchecks");

            // POST: api/rainchecks
            group.MapPost("/", async (Raincheck raincheck, RaincheckService service) =>
            {
                var createdRaincheck = await service.CreateRaincheckAsync(raincheck);
                return Results.Created($"/api/rainchecks/{createdRaincheck.RaincheckId}", createdRaincheck);
            })
            .WithName("CreateRaincheck")
            .WithTags("Rainchecks");

            // PUT: api/rainchecks/{id}
            group.MapPut("/{id}", async (int id, Raincheck raincheck, RaincheckService service) =>
            {
                var updatedRaincheck = await service.UpdateRaincheckAsync(id, raincheck);
                return updatedRaincheck == null ? Results.NotFound() : Results.Ok(updatedRaincheck);
            })
            .WithName("UpdateRaincheck")
            .WithTags("Rainchecks");

            // DELETE: api/rainchecks/{id}
            group.MapDelete("/{id}", async (int id, RaincheckService service) =>
            {
                var deleted = await service.DeleteRaincheckAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteRaincheck")
            .WithTags("Rainchecks");

            group.MapGet("/paged", async (RaincheckService service, int pageSize = 10, int page = 0) =>
            {
                var rainchecks = await service.GetRainchecksWithPaginationAsync(pageSize, page);
                return rainchecks.Count > 0 ? Results.Ok(rainchecks) : Results.NotFound();
            })
            .WithName("GetPagedRainchecks")
            .WithTags("Rainchecks");

            group.MapGet("/paged/select", async (RaincheckService service, int pageSize = 10, int page = 0) =>
            {
                var rainchecks = await service.GetRainchecksWithPaginationAndSelectionAsync(pageSize, page);
                return rainchecks.Count > 0 ? Results.Ok(rainchecks) : Results.NotFound();
            })
            .WithName("GetPagedAndSelectedRainchecks")
            .WithTags("Rainchecks");

            group.MapGet("/paged/select2", async (RaincheckService service, int pageSize = 10, int page = 0) =>
            {
                var rainchecks = await service.GetRainchecksWithPaginationAndSelectionAsync2(pageSize, page);
                return rainchecks.Count > 0 ? Results.Ok(rainchecks) : Results.NotFound();
            })
            .WithName("GetPagedAndSelectedRainchecks2")
            .WithTags("Rainchecks");

            group.MapGet("/paged/select3", async (StoreContext db, int pageSize = 10, int page = 0) =>
            {
                var data = await db.Rainchecks
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .Include(s => s.Product)
                    .Include(s => s.Store)
                    .Select(r => new
                    {
                        StoreName = r.Store.Name,
                        ProductName = r.Product.Title
                    })
                    .ToListAsync();

                return data.Any() ? Results.Json(data, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                }) : Results.NotFound();
            })
            .WithName("GetPagedAndSelectedRainchecks3")
            .WithTags("Rainchecks");

            group.MapGet("/paged/select4", async (StoreContext db, int pageSize = 10, int page = 0) =>
            {
                var data = await db.Rainchecks
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .Include(r => r.Product)
                    .Include(r => r.Store)
                    .Select(r => new
                    {
                        Raincheck = r,
                        Product = r.Product,
                        Store = r.Store
                    })
                    .ToListAsync();

                return data.Any() ? Results.Json(data, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                }) : Results.NotFound();
            })
            .WithName("GetPagedAndSelectedRainchecks4")
            .WithTags("Rainchecks");

            group.MapGet("/paged/selectJorge", async (StoreContext db, int pageSize = 10, int page = 0) =>
            {
                var data = await db.Rainchecks
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .Include(r => r.Product)
                    .Include(r => r.Store)
                    .Select(x => new
                    {
                        Name = x.Name,
                        Count = x.Count,
                        SalePrice = x.SalePrice,
                        Store = new
                        {
                            Name = x.Store.Name,
                        },
                        Product = new
                        {
                            Name = x.Product.Title,
                            Category = new
                            {
                                Name = x.Product.Category.Name
                            }
                        }
                    })
                    .ToListAsync();

                return data.Any() ? Results.Json(data, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                }) : Results.NotFound();
            })
            .WithName("GetPagedAndSelectedRainchecksJorge")
            .WithTags("Rainchecks");

            group.MapPost("/generate", async (RaincheckService service) =>
            {
                await service.GenerateRainchecksAsync(1000);
                return Results.Ok();
            })
            .WithName("GenerateRainchecks")
            .WithTags("Rainchecks");

        }
    }
}
