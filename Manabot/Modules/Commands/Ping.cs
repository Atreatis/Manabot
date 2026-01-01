using Discord;
using Discord.Interactions;
using Manabot.Resources;
using Manabot.Utils.Embeds;

namespace Manabot.Modules.Commands;

/// <summary>
/// A simple ping command that tells the bot its latency, shard and some basic information towards the end users.
/// </summary>
/// <param name="embedHelper"></param>

public class Ping(IEmbedHelper embedHelper) : InteractionModuleBase<ShardedInteractionContext>
{
    [CommandContextType(InteractionContextType.Guild)]
    [SlashCommand("ping", "Returns shard number and current bot latency.")]
    public async Task PingAsync()
    {
        Embed pingEmbed = embedHelper.Success(
            title: CommandLocales.ping_title,
            description: string.Format(CommandLocales.ping_description, Context.Client.Shards.Count, Context.Client.GetShardFor(Context.Guild.Id).ShardId, Context.Client.GetShardFor(Context.Guild.Id).Latency)
        );

        await RespondAsync(embed: pingEmbed, ephemeral: true);
    }
}