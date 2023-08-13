using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend;
using Microsoft.AspNetCore.Authorization;
using Backend.Services;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _orderService.GetOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost("createOrder")]
        public IActionResult CreateOrder(int stockId, string userName, float price, int quantity)
        {
            var order = _orderService.CreateOrder(stockId, userName, price, quantity);
            if (order == null)
            {
                Console.WriteLine("456");
                return BadRequest("Failed to create order.");
            }
            return Ok(order);
        }

    }
}
