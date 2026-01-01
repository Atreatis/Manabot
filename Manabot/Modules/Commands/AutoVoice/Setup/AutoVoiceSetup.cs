using Discord;
using Discord.Interactions;
using Manabot.Resources;
using Manabot.Utils.Embeds;

namespace Manabot.Modules.Commands.AutoVoice.Setup;

/// <summary>
/// Auto Voice: Setup<br/>
/// Set up a channel for Auto Voice in-order to create join to create channels through the bot. These channels will
/// trigger the bot to create a new voice channel or plainly ignore it when the channel is not within the Database.
/// </summary>
/// <param name="embedHelper"></param>
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