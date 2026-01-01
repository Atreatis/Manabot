using Discord.Interactions;
using Discord.WebSocket;

namespace Manabot.Services.Handlers;

/// <summary>
/// InteractionHandlerService registers commands and registers them towards the Discord so the commands are known and
/// Discord can show them within the client of the end user. 
/// </summary>
/// <param name="client"></param>
/// <param name="interactions"></param>
/// <param name="services"></param>

public class InteractionHandlerService(
    DiscordShardedClient client,
    InteractionService interactions,
    IServiceProvider services)
{
    public async Task InitializeAsync()
    {
        await interactions.AddModulesAsync(typeof(Program).Assembly, services);
    }

    public async Task HandleInteraction(SocketInteraction interaction)
    {
        ShardedInteractionContext ctx = new ShardedInteractionContext(client, interaction);
        await interactions.ExecuteCommandAsync(ctx, services);
    }
}