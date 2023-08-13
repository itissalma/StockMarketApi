using System.Collections.Generic;
using Backend.Models;

namespace Backend.Data.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetOrders();
        Order GetOrderById(int id);
        Order CreateOrder(Order order);
    }
}
