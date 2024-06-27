using StoreApi.Data;
using StoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreApi.Services
{
    public class OrderDetailService
    {
        private readonly StoreContext _context;

        public OrderDetailService(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDetail>> GetOrderDetailsAsync()
        {
            return await _context.OrderDetails.Include(od => od.Product).Include(od => od.Order).ToListAsync();
        }

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int id)
        {
            return await _context.OrderDetails.Include(od => od.Order).Include(od => od.Product).FirstOrDefaultAsync(od => od.OrderDetailId == id);
        }

        public async Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
            return orderDetail;
        }

        public async Task<OrderDetail> UpdateOrderDetailAsync(int id, OrderDetail orderDetail)
        {
            var existingOrderDetail = await _context.OrderDetails.FindAsync(id);
            if (existingOrderDetail == null)
            {
                return null;
            }

            existingOrderDetail.OrderId = orderDetail.OrderId;
            existingOrderDetail.ProductId = orderDetail.ProductId;
            existingOrderDetail.Count = orderDetail.Count;
            existingOrderDetail.UnitPrice = orderDetail.UnitPrice;

            await _context.SaveChangesAsync();
            return existingOrderDetail;
        }

        public async Task<bool> DeleteOrderDetailAsync(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return false;
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<OrderDetail>> GetOrderDetailsWithPaginationAsync(int pageSize, int page)
        {
            return await _context.OrderDetails
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<dynamic>> GetOrderDetailsWithPaginationAndSelectionAsync(int pageSize, int page)
        {
            return await _context.OrderDetails
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(orderDetail => new { orderDetail.OrderDetailId, orderDetail.OrderId, orderDetail.ProductId })
                .ToListAsync<dynamic>();
        }

        public async Task GenerateOrderDetailsAsync(int count)
        {
            var orders = await _context.Orders.ToListAsync();
            var products = await _context.Products.ToListAsync();
            if (orders.Count == 0) throw new Exception("No orders available to assign to order details.");
            if (products.Count == 0) throw new Exception("No products available to assign to order details.");

            var orderDetails = new List<OrderDetail>();
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                orderDetails.Add(new OrderDetail
                {
                    OrderId = orders[random.Next(orders.Count)].OrderId,
                    ProductId = products[random.Next(products.Count)].ProductId,
                    Count = random.Next(1, 10),
                    UnitPrice = random.Next(10, 100)
                });
            }
            _context.OrderDetails.AddRange(orderDetails);
            await _context.SaveChangesAsync();
        }


    }
}
