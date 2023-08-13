using System.Collections.Generic;
using Backend.Models;

namespace Backend.Services
{
    public interface IOrderService
    {
        List<Order> GetOrders();
        Order GetOrderById(int id);
        Order CreateOrder(int stockId, string userName, float price, int quantity);
    }
}
