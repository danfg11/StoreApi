using StoreApi.Models;
using StoreApi.Services;

namespace StoreApi.Endpoints
{
    public static class CartItemEndpoints
    {
        public static void MapCartItemEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/cartitems");

            // GET: api/cartitems
            group.MapGet("/", async (CartItemService service) =>
            {
                return Results.Ok(await service.GetCartItemsAsync());
            })
            .WithName("GetCartItems")
            .WithTags("Cart Items");

            // GET: api/cartitems/{id}
            group.MapGet("/{id}", async (int id, CartItemService service) =>
            {
                var item = await service.GetCartItemByIdAsync(id);
                return item == null ? Results.NotFound() : Results.Ok(item);
            })
            .WithName("GetCartItemById")
            .WithTags("Cart Items");

            // POST: api/cartitems
            group.MapPost("/", async (CartItem cartItem, CartItemService service) =>
            {
                var createdCartItem = await service.CreateCartItemAsync(cartItem);
                return Results.Created($"/api/cartitems/{createdCartItem.CartItemId}", createdCartItem);
            })
            .WithName("CreateCartItem")
            .WithTags("Cart Items");

            // PUT: api/cartitems/{id}
            group.MapPut("/{id}", async (int id, CartItem cartItem, CartItemService service) =>
            {
                var updatedCartItem = await service.UpdateCartItemAsync(id, cartItem);
                return updatedCartItem == null ? Results.NotFound() : Results.Ok(updatedCartItem);
            })
            .WithName("UpdateCartItem")
            .WithTags("Cart Items");

            // DELETE: api/cartitems/{id}
            group.MapDelete("/{id}", async (int id, CartItemService service) =>
            {
                var deleted = await service.DeleteCartItemAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteCartItem")
            .WithTags("Cart Items");

            group.MapGet("/paged", async (CartItemService service, int pageSize = 10, int page = 0) =>
            {
                var cartItems = await service.GetCartItemsWithPaginationAsync(pageSize, page);
                return cartItems.Count > 0 ? Results.Ok(cartItems) : Results.NotFound();
            })
            .WithName("GetPagedCartItems")
            .WithTags("Cart Items");

            group.MapGet("/paged/select", async (CartItemService service, int pageSize = 10, int page = 0) =>
            {
                var cartItems = await service.GetCartItemsWithPaginationAndSelectionAsync(pageSize, page);
                return cartItems.Count > 0 ? Results.Ok(cartItems) : Results.NotFound();
            })
            .WithName("GetPagedAndSelectedCartItems")
            .WithTags("Cart Items");

            group.MapPost("/generate", async (CartItemService service) =>
            {
                await service.GenerateCartItemsAsync(1000);
                return Results.Ok();
            })
            .WithName("GenerateCartItems")
            .WithTags("Cart Items");
        }
    }
}
