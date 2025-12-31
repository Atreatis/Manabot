using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Manabot;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("[Manabot] running at: {time}", DateTimeOffset.Now);
                _logger.LogWarning("[Manabot] warning at: {time}", DateTimeOffset.Now);
                _logger.LogError("[Manabot] error at: {time}", DateTimeOffset.Now);
                _logger.LogCritical("[Manabot] critical at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}