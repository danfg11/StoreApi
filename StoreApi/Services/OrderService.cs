using StoreApi.Data;
using StoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreApi.Services
{
    public class OrderService
    {
        private readonly StoreContext _context;

        public OrderService(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product).ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product).FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrderAsync(int id, Order order)
        {
            var existingOrder = await _context.Orders.FindAsync(id);
            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.Username = order.Username;
            existingOrder.Name = order.Name;
            existingOrder.Address = order.Address;
            existingOrder.City = order.City;
            existingOrder.State = order.State;
            existingOrder.PostalCode = order.PostalCode;
            existingOrder.Country = order.Country;
            existingOrder.Phone = order.Phone;
            existingOrder.Email = order.Email;
            existingOrder.Total = order.Total;

            await _context.SaveChangesAsync();
            return existingOrder;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Order>> GetOrdersWithPaginationAsync(int pageSize, int page)
        {
            return await _context.Orders
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<dynamic>> GetOrdersWithPaginationAndSelectionAsync(int pageSize, int page)
        {
            return await _context.Orders
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(order => new { order.OrderId, order.Username })
                .ToListAsync<dynamic>();
        }

        public async Task GenerateOrdersAsync(int count)
        {
            var random = new Random();
            var orders = new List<Order>();

            for (int i = 0; i < count; i++)
            {
                orders.Add(new Order
                {
                    OrderDate = DateTime.Now,
                    Username = $"user{i}",
                    Name = $"Name {i}",
                    Address = $"Address {i}",
                    City = $"City {i}",
                    State = $"State {i}",
                    PostalCode = $"PC{i.ToString("D4")}",
                    Country = $"Country {i}",
                    Phone = $"12345678{i}",
                    Email = $"user{i}@example.com",
                    Total = random.Next(100, 1000)
                });
            }
            _context.Orders.AddRange(orders);
            await _context.SaveChangesAsync();
        }

    }
}
