using Discord;
using Discord.WebSocket;
using Discord.Interactions;
using Manabot.Services;
using Manabot.Services.Handlers;
using Manabot.Utils.Embeds;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Guild Event Modules
using Manabot.Modules.Events.Voice;

namespace Manabot;

/// <summary>
/// <para>Main startup file of Manabot, this contains the main skeleton for starting up and running the Discord bot with
/// interaction service.</para>
///
/// Service order:<br/>
/// Main Configuration<br/>
/// Interaction Service<br/>
/// Guild Events Service<br/>
/// Event Handler Service<br/>
/// Discord Services: Singleton / Bot Workers
/// </summary>

public class Program
{
    public static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<DiscordShardedClient>(_ =>
                {
                    DiscordSocketConfig config = new()
                    {
                        GatewayIntents =
                            GatewayIntents.AllUnprivileged  |
                            GatewayIntents.Guilds |
                            GatewayIntents.GuildMembers |
                            GatewayIntents.GuildVoiceStates,
                        LogGatewayIntentWarnings = false,
                        TotalShards = null
                    };
                    return new DiscordShardedClient(config);
                });

                // Interaction Service: Singleton
                services.AddSingleton<InteractionService>(sp =>
                {
                    DiscordShardedClient client = sp.GetRequiredService<DiscordShardedClient>();
                    return new InteractionService(client);
                });
                
                // Discord Guild Events: Singleton
                services.AddSingleton<IDiscordEventHandler, VoiceChannelSate>();
                
                // Event Handler: Singleton 
                services.AddSingleton<EventHandlerService>();
                
                // Discord Services: Singleton / Workers
                services.AddSingleton<InteractionHandlerService>();
                services.AddSingleton<IEmbedHelper, EmbedHelper>();
                services.AddHostedService<BotWorker>();
            })
            .Build();

        await host.RunAsync();
    }
}