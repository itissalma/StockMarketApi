using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StockMarketContext _context;

        public OrderRepository(StockMarketContext context)
        {
            _context = context;
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.Include(o => o.User).ToList();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == id);
        }

        public Order CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            
            try
            {
                _context.SaveChanges();
                return order;
            }
            catch
            {
                return null;
            }
        }

    }
}
