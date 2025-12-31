using Discord.Interactions;
using Discord.WebSocket;

namespace Manabot.Services.Handlers;

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