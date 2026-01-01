using Discord.WebSocket;

namespace Manabot.Services.Handlers;

/// <summary>
/// EventHandlerService is responsible to register and handle events within the Discord bot. These events always trigger
/// through DiscordShardedClient. 
/// </summary>
/// <param name="client"></param>
/// <param name="handlers"></param>

public class EventHandlerService(DiscordShardedClient client, IEnumerable<IDiscordEventHandler> handlers)
{
    public void RegisterAll()
    {
        foreach (IDiscordEventHandler handler in handlers)
        {
            handler.Register(client);
        }
    }
}