using Discord.WebSocket;

namespace Manabot.Services.Handlers;

/// <summary>
/// IDiscordEventHandler creates the logic between EventHandlerService and Events that are being used by the Discord
/// bot process. These events are being called through DiscordShardedClient and are registered by EventHandlerService.
/// </summary>

public interface IDiscordEventHandler
{
    void Register(DiscordShardedClient client);
}