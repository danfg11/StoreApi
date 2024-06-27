using StoreApi.Models;
using StoreApi.Services;

namespace StoreApi.Endpoints
{
    public static class OrderEndpoints
    {
        public static void MapOrderEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/orders");

            // GET: api/orders
            group.MapGet("/", async (OrderService service) =>
            {
                return Results.Ok(await service.GetOrdersAsync());
            })
            .WithName("GetOrders")
            .WithTags("Orders");

            // GET: api/orders/{id}
            group.MapGet("/{id}", async (int id, OrderService service) =>
            {
                var item = await service.GetOrderByIdAsync(id);
                return item == null ? Results.NotFound() : Results.Ok(item);
            })
            .WithName("GetOrderById")
            .WithTags("Orders");

            // POST: api/orders
            group.MapPost("/", async (Order order, OrderService service) =>
            {
                var createdOrder = await service.CreateOrderAsync(order);
                return Results.Created($"/api/orders/{createdOrder.OrderId}", createdOrder);
            })
            .WithName("CreateOrder")
            .WithTags("Orders");

            // PUT: api/orders/{id}
            group.MapPut("/{id}", async (int id, Order order, OrderService service) =>
            {
                var updatedOrder = await service.UpdateOrderAsync(id, order);
                return updatedOrder == null ? Results.NotFound() : Results.Ok(updatedOrder);
            })
            .WithName("UpdateOrder")
            .WithTags("Orders");

            // DELETE: api/orders/{id}
            group.MapDelete("/{id}", async (int id, OrderService service) =>
            {
                var deleted = await service.DeleteOrderAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteOrder")
            .WithTags("Orders");

            group.MapGet("/paged", async (OrderService service, int pageSize = 10, int page = 0) =>
            {
                var orders = await service.GetOrdersWithPaginationAsync(pageSize, page);
                return orders.Count > 0 ? Results.Ok(orders) : Results.NotFound();
            })
            .WithName("GetPagedOrders")
            .WithTags("Orders");

            group.MapGet("/paged/select", async (OrderService service, int pageSize = 10, int page = 0) =>
            {
                var orders = await service.GetOrdersWithPaginationAndSelectionAsync(pageSize, page);
                return orders.Count > 0 ? Results.Ok(orders) : Results.NotFound();
            })
            .WithName("GetPagedAndSelectedOrders")
            .WithTags("Orders");

            // Endpoint para generar 1,000 órdenes
            group.MapPost("/generate", async (OrderService service) =>
            {
                await service.GenerateOrdersAsync(1000);
                return Results.Ok();
            })
            .WithName("GenerateOrders")
            .WithTags("Orders");

        }
    }
}
