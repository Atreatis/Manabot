using Discord.WebSocket;

namespace Manabot.Services.Handlers;

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