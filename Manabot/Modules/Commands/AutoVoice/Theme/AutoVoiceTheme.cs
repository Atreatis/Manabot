using Discord;
using Discord.Interactions;
using Manabot.Resources;
using Manabot.Utils.Embeds;

namespace Manabot.Modules.Commands.AutoVoice.Theme;

/// <summary>
/// Auto Voice: Theme Picker<br/>
/// Pick and choose a different theme for your Auto Voice Generated channels. These channels will be generated with the
/// new theme names when a new channel has to be made from Join to Create Lobby. 
/// </summary>
/// <param name="embedHelper"></param>
public class AutoVoiceTheme(IEmbedHelper embedHelper) : InteractionModuleBase<ShardedInteractionContext>
{
    [ComponentInteraction("av_theme_set_*")]
    public async Task AutoVoiceThemeAsync()
    {
        Embed autoVoiceThemeEmbed = embedHelper.Generic(
            title: CommandLocales.autovoice_theme_channel_title,
            description: CommandLocales.autovoice_theme_channel_description);
        
        SelectMenuBuilder channelMenuBuilder = new SelectMenuBuilder()
            .WithPlaceholder(CommandLocales.autovoice_theme_channel_select)
            .WithCustomId($"av_theme_sel_{Context.User.Id}")
            .WithMinValues(1)
            .WithMaxValues(1)
            .WithType(ComponentType.ChannelSelect)
            .WithChannelTypes(ChannelType.Voice);

        ComponentBuilder components = new ComponentBuilder()
            .WithSelectMenu(channelMenuBuilder);

        await DeferAsync();
        await ModifyOriginalResponseAsync(msgProps =>
        {
            msgProps.Embed = autoVoiceThemeEmbed;
            msgProps.Components = components.Build();
        });
    }
}