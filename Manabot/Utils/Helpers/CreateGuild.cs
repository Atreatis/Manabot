using Manabot.Database.Models;
using Manabot.Database;

namespace Manabot.Utils.Helpers;

public abstract class CreateGuild
{
    public static async Task New (ulong guildId)
    {
        await new DbConnect().InsertOneAsync("GuildSettings", new GuildSettings { GuildId =  guildId });   
    }
}