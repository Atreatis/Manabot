using Discord.WebSocket;

namespace Manabot.Services.Handlers;

public interface IDiscordEventHandler
{
    void Register(DiscordShardedClient client);
}