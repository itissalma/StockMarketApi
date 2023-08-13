using System.Collections.Generic;
using Backend.Models;

namespace Backend.Data.Repositories
{
    public interface IStockRepository
    {
        List<Stock> GetStocks();
        List<string> GetStockNames();
        Stock GetStockById(int id);
    }
}
