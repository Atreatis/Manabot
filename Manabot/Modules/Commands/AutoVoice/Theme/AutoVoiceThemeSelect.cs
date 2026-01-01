using Discord;
using Discord.Interactions;
using Manabot.Database;
using Manabot.Database.Models;
using Manabot.Database.Models.AutoVoice;
using Manabot.Resources;
using Manabot.Utils.Embeds;
using MongoDB.Driver;

namespace Manabot.Modules.Commands.AutoVoice.Theme;

/// <summary>
/// Auto Voice: Theme Selection<br/>
/// Select the theme that should be applied to newly generated channels, the new names will only be generated upon
/// newly created channels within Discord.
/// </summary>
/// <param name="embedHelper"></param>
public class AutoVoiceThemeSelect(IEmbedHelper embedHelper) : InteractionModuleBase<ShardedInteractionContext>
{
    [ComponentInteraction("av_theme_sel_*")]
    public async Task AutoVoiceThemeSelectAsync(string id, ulong[] selectedChannels)
    {
        FilterDefinition<GuildSettings> guildFilter = Builders<GuildSettings>.Filter.Eq(x => x.GuildId, Context.Guild.Id); 
        GuildSettings guildSettings = await new DbConnect().FindOneAsync("GuildSettings", guildFilter);

        // Check if channel exists
        List<AutoVoiceSettings>? channels = guildSettings.AutoVoiceSettings;
        
        if (channels?.Find(x => x.VoiceChannelId == selectedChannels.First()) == null)
        {
            await OnChannelNotFound();
            return;
        }

        Embed autoVoiceThemeSelectEmbed = embedHelper.Generic(
            title: CommandLocales.autovoice_theme_select_title,
            description: CommandLocales.autovoice_theme_select_description);

        SelectMenuBuilder autoVoiceThemeSelectMenu = new SelectMenuBuilder()
            .WithPlaceholder(CommandLocales.autovoice_theme_select_placeholder)
            .WithCustomId($"av_theme_save_{Context.User.Id}")
            .WithMinValues(1)
            .WithMaxValues(1)
            .AddOption(CommandLocales.autovoice_theme_select_general, $"{selectedChannels.First()}, General, General",
                string.Format(CommandLocales.autovoice_theme_select_theme_description, CommandLocales.autovoice_theme_select_general))
            .AddOption(CommandLocales.autovoice_theme_select_pirates, $"{selectedChannels.First()}, Pirates, Pirates",
                string.Format(CommandLocales.autovoice_theme_select_theme_description, CommandLocales.autovoice_theme_select_pirates))
            .AddOption(CommandLocales.autovoice_theme_select_finalfantasy, $"{selectedChannels.First()}, FinalFantasy, Final Fantasy",
                string.Format(CommandLocales.autovoice_theme_select_theme_description, CommandLocales.autovoice_theme_select_finalfantasy));

        ComponentBuilder components = new ComponentBuilder()
            .WithSelectMenu(autoVoiceThemeSelectMenu);

        await DeferAsync();
        await ModifyOriginalResponseAsync(msgProps =>
        {
            msgProps.Embed = autoVoiceThemeSelectEmbed;
            msgProps.Components = components.Build();
        });
    }

    private async Task OnChannelNotFound()
    {
        Embed noAutoVoiceChannelEmbed = embedHelper.Warning(
            description: CommandLocales.autovoice_channel_notfound);
            
        await DeferAsync();
        await ModifyOriginalResponseAsync(msgProps =>
        {
            msgProps.Embed = noAutoVoiceChannelEmbed;
            msgProps.Components = new ComponentBuilder().Build();
        });
    }
}