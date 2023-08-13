using System.Collections.Generic;
using System.Linq;
using Backend.Models;

namespace Backend.Data.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly StockMarketContext _context;

        public StockRepository(StockMarketContext context)
        {
            _context = context;
        }

        public List<Stock> GetStocks()
        {
            return _context.Stocks.ToList();
        }

        public List<string> GetStockNames()
        {
            return _context.Stocks.Join(_context.Orders, s => s.StockId, o => o.StockId, (s, o) => s.StockName).ToList();
        }


        public Stock GetStockById(int id)
        {
            return _context.Stocks.FirstOrDefault(s => s.StockId == id);
        }
    }
}
