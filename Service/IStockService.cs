using System.Collections.Generic;
using Backend.Models;

namespace Backend.Services
{
    public interface IStockService
    {
        List<Stock> GetStocks();
        List<string> GetStockNames();
        Stock GetStockById(int id);
    }
}
