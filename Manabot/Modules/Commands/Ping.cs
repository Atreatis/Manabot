using Discord;
using Discord.Interactions;
using Manabot.Resources;
using Manabot.Utils.Helpers.Embeds;

namespace Manabot.Modules.Commands;

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