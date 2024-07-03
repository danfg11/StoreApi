using StoreApi.Models;
using StoreApi.Services;

namespace StoreApi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/products");

            // GET: api/products
            group.MapGet("/", async (ProductService service) =>
            {
                return Results.Ok(await service.GetProductsAsync());
            })
            .WithName("GetProducts")
            .WithTags("Products");

            // GET: api/products/{id}
            group.MapGet("/{id}", async (int id, ProductService service) =>
            {
                var item = await service.GetProductByIdAsync(id);
                return item == null ? Results.NotFound() : Results.Ok(item);
            })
            .WithName("GetProductById")
            .WithTags("Products");

            // POST: api/products
            group.MapPost("/", async (Product product, ProductService service) =>
            {
                var createdProduct = await service.CreateProductAsync(product);
                return Results.Created($"/api/products/{createdProduct.ProductId}", createdProduct);
            })
            .WithName("CreateProduct")
            .WithTags("Products");

            // PUT: api/products/{id}
            group.MapPut("/{id}", async (int id, Product product, ProductService service) =>
            {
                var updatedProduct = await service.UpdateProductAsync(id, product);
                return updatedProduct == null ? Results.NotFound() : Results.Ok(updatedProduct);
            })
            .WithName("UpdateProduct")
            .WithTags("Products");

            // DELETE: api/products/{id}
            group.MapDelete("/{id}", async (int id, ProductService service) =>
            {
                var deleted = await service.DeleteProductAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteProduct")
            .WithTags("Products");

            //group.MapGet("/paged", async (ProductService service, int pageSize = 10, int page = 0) =>
            //{
            //    var products = await service.GetProductsWithPaginationAsync(pageSize, page);
            //    return products.Count > 0 ? Results.Ok(products) : Results.NotFound();
            //})
            //.WithName("GetPagedProducts")
            //.WithTags("Products");

            group.MapGet("/paged/select", async (ProductService service, int page = 0) =>
            {
                var products = await service.GetProductsWithPaginationAsync(page);
                var productDtos = products.Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductGuid = p.ProductGuid,
                    SkuNumber = p.SkuNumber,
                    CategoryId = p.CategoryId,
                    RecommendationId = p.RecommendationId,
                    Title = p.Title,
                    Price = p.Price,
                    SalePrice = p.SalePrice,
                    ProductArtUrl = p.ProductArtUrl,
                    Description = p.Description,
                    Created = p.Created,
                    ProductDetails = p.ProductDetails,
                    Inventory = p.Inventory,
                    LeadTime = p.LeadTime
                }).ToList();
                return Results.Ok(productDtos);
            })
            .WithName("GetPagedProducts")
            .WithTags("Products");

            // Endpoint para generar 1,000 productos
            group.MapPost("/generate", async (ProductService service) =>
            {
                await service.GenerateProductsAsync(1000);
                return Results.Ok();
            })
            .WithName("GenerateProducts")
            .WithTags("Products");
        }
    }
}
