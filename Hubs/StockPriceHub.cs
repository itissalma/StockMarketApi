using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Backend
{
    public class StockPriceHub : Hub
    {
         public async Task SendStockPriceUpdate(int stockId, decimal newPrice)
        {
            //log to the console something
            //Console.WriteLine($"Sending stock price update for {stockId} to {newPrice}");
            await Clients.All.SendAsync("ReceiveStockPriceUpdate", stockId, newPrice);
        }
    }
}
