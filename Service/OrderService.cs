using System;
using System.Linq;
using Backend.Models;
using Backend.Data.Repositories;

namespace Backend.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(IOrderRepository orderRepository, IStockRepository stockRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _stockRepository = stockRepository;
            _userRepository = userRepository;
        }

        public List<Order> GetOrders()
        {
            return _orderRepository.GetOrders();
        }

        public Order GetOrderById(int id)
        {
            return _orderRepository.GetOrderById(id);
        }

        public Order CreateOrder(int stockId, string userName, float price, int quantity)
        {
            var stock = _stockRepository.GetStockById(stockId);
            var user = _userRepository.GetUserByUserName(userName);

            if (stock == null || user == null)
            {
                if(stock == null)
                    Console.WriteLine("stock null");
                if(user == null)
                    Console.WriteLine("user null");
                Console.WriteLine("null");
                return null;
            }

            var order = new Order
            {
                StockId = stockId,
                UserName = userName,
                Price = price,
                Quantity = quantity
            };
            return _orderRepository.CreateOrder(order);
        }

    }
}
