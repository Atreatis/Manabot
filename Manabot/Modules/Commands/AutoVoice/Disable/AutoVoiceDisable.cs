using Discord;
using Discord.Interactions;
using Manabot.Resources;
using Manabot.Utils.Helpers.Embeds;

namespace Manabot.Modules.Commands.AutoVoice.Disable;

public class AutoVoiceDisable(IEmbedHelper embedHelper) : InteractionModuleBase<ShardedInteractionContext>
{
    [ComponentInteraction("av_disable_chan_*")]
    public async Task AutoVoiceDisableAsync()
    {
        Embed autoVoiceDisableEmbed = embedHelper.Generic(
            title: CommandLocales.autovoice_disable_title,
            description: CommandLocales.autovoice_disable_description);

        SelectMenuBuilder channelMenuBuilder = new SelectMenuBuilder()
            .WithPlaceholder(CommandLocales.autovoice_disable_channel_select)
            .WithCustomId($"av_disable_save_{Context.User.Id}")
            .WithMinValues(1)
            .WithMaxValues(1)
            .WithType(ComponentType.ChannelSelect)
            .WithChannelTypes(ChannelType.Voice);

        ComponentBuilder components = new ComponentBuilder()
            .WithSelectMenu(channelMenuBuilder);

        await DeferAsync();
        await ModifyOriginalResponseAsync(msgProps =>
        {
            msgProps.Embed = autoVoiceDisableEmbed;
            msgProps.Components = components.Build();
        });
    }
}