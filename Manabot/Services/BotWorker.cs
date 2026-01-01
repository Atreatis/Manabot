using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Manabot.Services.Handlers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Manabot.Services;

/// <summary>
/// BotWorker is where the bot process is officially spawned within the program and ShardedClients are spawned from this
/// class. Each shard will be spawned once the bot has joined roughly 1000 Guilds (Discord Servers) and will handle each
/// of their own resources. 
/// </summary>
/// <param name="client"></param>
/// <param name="eventHandler"></param>
/// <param name="interactions"></param>
/// <param name="handlerService"></param>
/// <param name="logger"></param>

public class BotWorker(
    DiscordShardedClient client,
    EventHandlerService eventHandler,
    InteractionService interactions,
    InteractionHandlerService handlerService,
    ILogger<BotWorker> logger)
    : IHostedService
{
    public async Task StartAsync(CancellationToken stoppingToken)
    {
        client.Log += LogAsync;
        interactions.Log += LogAsync;

        client.ShardReady += ShardReadyAsync;
        client.InteractionCreated += handlerService.HandleInteraction;

        string token = Environment.GetEnvironmentVariable("DISCORD_TOKEN") ?? throw new Exception("Bot token not provided in DISCORD_TOKEN environment variable.");
        
        eventHandler.RegisterAll();
        
        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();
        
        await handlerService.InitializeAsync();
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        client.StopAsync();
        return Task.CompletedTask;
    }
    
    private Task LogAsync(LogMessage msg)
    {
        logger.LogInformation("{Source}: {Message}", msg.Source, msg.Message);
        return Task.CompletedTask;
    }

    private Task ShardReadyAsync(DiscordSocketClient shard)
    {
        logger.LogInformation("[{CurrentUserUsername}] Shard {ShardShardId} connected.", shard.CurrentUser.Username, shard.ShardId);
        return interactions.RegisterCommandsGloballyAsync();
    }
}