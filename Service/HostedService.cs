using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore; // Make sure to include the correct namespace for your context
using Backend.Models; 
using Backend;

public class TimedHostedService : IHostedService, IDisposable
{
    private int executionCount = 0;
    private readonly ILogger<TimedHostedService> _logger;
    private Timer? _timer = null;
    private readonly Random _random = new Random();
    private readonly IHubContext<StockPriceHub> _hubContext;
    private readonly StockMarketContext _context; // Replace with your actual context type

    public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory factory)
    {
        _logger = logger;
        _context = factory.CreateScope().ServiceProvider.GetRequiredService<StockMarketContext>();
        _hubContext = factory.CreateScope().ServiceProvider.GetRequiredService<IHubContext<StockPriceHub>>();
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        //_logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        var stocks = await _context.Stocks.ToListAsync();

        foreach (var stock in stocks)
        {
            var newPrice = _random.NextDouble() * 100;

            stock.Price = (float)newPrice;
            await _context.SaveChangesAsync();

            //_logger.LogInformation($"Updated {stock.StockId} price to {stock.Price}");

            await _hubContext.Clients.All.SendAsync("ReceiveStockPriceUpdate", stock.StockId, stock.Price);
        }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
       // _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
