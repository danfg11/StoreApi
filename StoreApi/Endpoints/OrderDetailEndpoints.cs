using StoreApi.Models;
using StoreApi.Services;

namespace StoreApi.Endpoints
{
    public static class OrderDetailEndpoints
    {
        public static void MapOrderDetailEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/orderdetails");

            // GET: api/orderdetails
            group.MapGet("/", async (OrderDetailService service) =>
            {
                return Results.Ok(await service.GetOrderDetailsAsync());
            })
            .WithName("GetOrderDetails")
            .WithTags("Order Details");

            // GET: api/orderdetails/{id}
            group.MapGet("/{id}", async (int id, OrderDetailService service) =>
            {
                var item = await service.GetOrderDetailByIdAsync(id);
                return item == null ? Results.NotFound() : Results.Ok(item);
            })
            .WithName("GetOrderDetailById")
            .WithTags("Order Details");

            // POST: api/orderdetails
            group.MapPost("/", async (OrderDetail orderDetail, OrderDetailService service) =>
            {
                var createdOrderDetail = await service.CreateOrderDetailAsync(orderDetail);
                return Results.Created($"/api/orderdetails/{createdOrderDetail.OrderDetailId}", createdOrderDetail);
            })
            .WithName("CreateOrderDetail")
            .WithTags("Order Details");

            // PUT: api/orderdetails/{id}
            group.MapPut("/{id}", async (int id, OrderDetail orderDetail, OrderDetailService service) =>
            {
                var updatedOrderDetail = await service.UpdateOrderDetailAsync(id, orderDetail);
                return updatedOrderDetail == null ? Results.NotFound() : Results.Ok(updatedOrderDetail);
            })
            .WithName("UpdateOrderDetail")
            .WithTags("Order Details");

            // DELETE: api/orderdetails/{id}
            group.MapDelete("/{id}", async (int id, OrderDetailService service) =>
            {
                var deleted = await service.DeleteOrderDetailAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteOrderDetail")
            .WithTags("Order Details");

            group.MapGet("/paged", async (OrderDetailService service, int pageSize = 10, int page = 0) =>
            {
                var orderDetails = await service.GetOrderDetailsWithPaginationAsync(pageSize, page);
                return orderDetails.Count > 0 ? Results.Ok(orderDetails) : Results.NotFound();
            })
            .WithName("GetPagedOrderDetails")
            .WithTags("Order Details");

            group.MapGet("/paged/select", async (OrderDetailService service, int pageSize = 10, int page = 0) =>
            {
                var orderDetails = await service.GetOrderDetailsWithPaginationAndSelectionAsync(pageSize, page);
                return orderDetails.Count > 0 ? Results.Ok(orderDetails) : Results.NotFound();
            })
            .WithName("GetPagedAndSelectedOrderDetails")
            .WithTags("Order Details");

            group.MapPost("/generate", async (OrderDetailService service) =>
            {
                await service.GenerateOrderDetailsAsync(1000);
                return Results.Ok();
            })
            .WithName("GenerateOrderDetails")
            .WithTags("Order Details");
        }
    }
}
