using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend;
using Microsoft.AspNetCore.Authorization;
using Backend.Services;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public IActionResult GetStocks()
        {
            var stocks = _stockService.GetStocks();
            return Ok(stocks);
        }

        [HttpGet("GetStockName")]
        public IActionResult GetStockName()
        {
            var stockNames = _stockService.GetStockNames();
            return Ok(stockNames);
        }

        [HttpGet("{id}")]
        public IActionResult GetStockById(int id)
        {
            var stock = _stockService.GetStockById(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }
    }
}
