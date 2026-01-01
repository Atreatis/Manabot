using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Manabot;

/// <summary>
/// Start Worker with ILogger Background Service to produce log output in-case error handling has to
/// happen.
/// </summary>

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("[Manabot] running at: {time}", DateTimeOffset.Now);
                logger.LogWarning("[Manabot] warning at: {time}", DateTimeOffset.Now);
                logger.LogError("[Manabot] error at: {time}", DateTimeOffset.Now);
                logger.LogCritical("[Manabot] critical at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}