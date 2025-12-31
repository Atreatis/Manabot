using Discord;
using Discord.Interactions;
using Manabot.Resources;
using Manabot.Utils.Helpers.Embeds;

namespace Manabot.Modules.Commands.AutoVoice.Setup;

public class AutoVoiceChannelSetup(IEmbedHelper embedHelper) : InteractionModuleBase<ShardedInteractionContext>
{
    [ComponentInteraction("av_setup_chan_*")]
    public async Task ChannelSetupAsync()
    {

        Embed autoVoiceSetupEmbed = embedHelper.Generic(
            title: CommandLocales.autovoice_setup_channel_title,
            description: CommandLocales.autovoice_setup_channel_description);
            
        SelectMenuBuilder? channelMenuBuilder = new SelectMenuBuilder()
            .WithPlaceholder(CommandLocales.autovoice_setup_channel_voicechannel)
            .WithCustomId($"av_setup_sel_{Context.User.Id}")
            .WithMinValues(1)
            .WithMaxValues(1)
            .WithType(ComponentType.ChannelSelect)
            .WithChannelTypes(ChannelType.Voice);

        ComponentBuilder? components = new ComponentBuilder()
            .WithSelectMenu(channelMenuBuilder);

        await DeferAsync();
        await ModifyOriginalResponseAsync(msgProps =>
        {
            msgProps.Embed = autoVoiceSetupEmbed;
            msgProps.Components = components.Build();
        });
    }
}