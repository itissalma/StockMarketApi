using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.Data.Repositories;

namespace Backend.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public List<Stock> GetStocks()
        {
            return _stockRepository.GetStocks();
        }

        public List<string> GetStockNames()
        {
            return _stockRepository.GetStockNames();
        }

        public Stock GetStockById(int id)
        {
            return _stockRepository.GetStockById(id);
        }
    }
}
