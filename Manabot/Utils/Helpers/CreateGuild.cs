using Manabot.Database.Models;
using Manabot.Database;

namespace Manabot.Utils.Helpers;

/// <summary>
/// Create a new GuildSettings entry within the database before actual settings are able to store within the
/// database. This function should always be called when a check is being executed if GuildSettings exist of the
/// Discord server.
/// </summary>

public abstract class CreateGuild
{
    public static async Task New (ulong guildId)
    {
        await new DbConnect().InsertOneAsync("GuildSettings", new GuildSettings { GuildId =  guildId });   
    }
}