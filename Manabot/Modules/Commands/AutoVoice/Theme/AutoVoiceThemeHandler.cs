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
/// Auto voice: Theme Handler<br/>
/// Set and/or update the selected theme within the Database and enforce new themes will be used on newly created
/// channels within the Discord server.
/// </summary>
/// <param name="embedHelper"></param>
public class AutoVoiceThemeSelectHandler(IEmbedHelper embedHelper) : InteractionModuleBase<ShardedInteractionContext>
{
    [ComponentInteraction("av_theme_save_*")]
    public async Task AutoVoiceThemeSelectHandlerAsync(string id, string[] choices)
    {
        try
        {
            // Split choices into several parts
            List<string> options = choices.First().Split(", ").ToList();

            // Parse for ULONG Value for Channel ID
            if (!ulong.TryParse(options[0], out ulong voiceChannelId))
            {
                await OnParseError();
                return;
            }

            // Initialize options
            string themeSetting = options[1];
            string themeName = options[2];

            // Fetch data for AutoVoice Channels
            FilterDefinition<GuildSettings> guildFilter = Builders<GuildSettings>.Filter.Eq(x => x.GuildId, Context.Guild.Id);
            GuildSettings guildSettings = await new DbConnect().FindOneAsync("GuildSettings", guildFilter);
            
            // Put Voice Settings into one list
            List<AutoVoiceSettings>? settings = guildSettings.AutoVoiceSettings;
            AutoVoiceSettings? voiceChannel = settings?.Find(x => x.VoiceChannelId == voiceChannelId);
            
            // Check for duplicate settings
            if (voiceChannel?.VoiceChannelTheme == themeName)
            {
                await OnDuplicateSetting();
                return;
            }
            
            // Set update definitions and update channel theme in Array
            FilterDefinition<GuildSettings>? filter = Builders<GuildSettings>.Filter.Eq(x => x.GuildId, Context.Guild.Id) &
                                                      Builders<GuildSettings>.Filter.Eq("AutoVoiceSettings.VoiceChannelId", voiceChannel!.VoiceChannelId);
            UpdateDefinition<GuildSettings>? update = Builders<GuildSettings>.Update.Set("AutoVoiceSettings.$.VoiceChannelTheme", themeName);
            await new DbConnect().UpdateOneAsync("GuildSettings", filter, update);

            await OnChannelUpdated(voiceChannelId, themeName);
        }

        catch (Exception ex)
        {
            await OnException(ex, Context.User.Username);
        }
    }

    private async Task OnParseError()
    {
        Embed parseErrorEmbed = embedHelper.Error(
            description: CommandLocales.parse_error
        );

        await DeferAsync();
        await ModifyOriginalResponseAsync(msgProps =>
        {
            msgProps.Embed = parseErrorEmbed;
            msgProps.Components = new ComponentBuilder().Build();
        });
    }

    private async Task OnDuplicateSetting()
    {
        Embed duplicateThemeEmbed = embedHelper.Warning(
            title: CommandLocales.error_duplicate_setting_title,
            description: CommandLocales.error_duplicate_setting_description
        );

        await DeferAsync();
        await ModifyOriginalResponseAsync(msgProps =>
        {
            msgProps.Embed = duplicateThemeEmbed;
            msgProps.Components = new ComponentBuilder().Build();
        });
    }

    private async Task OnChannelUpdated(ulong channelId, string themeName)
    {
        Embed autoVoiceThemeSetEmbed = embedHelper.Success(
            title: CommandLocales.autovoice_theme_updated_title,
            description: string.Format(CommandLocales.autovoice_theme_updated_description, $"<#{channelId}>", themeName)
        );

        await DeferAsync();
        await ModifyOriginalResponseAsync(msgProps =>
        {
            msgProps.Embed = autoVoiceThemeSetEmbed;
            msgProps.Components = new ComponentBuilder().Build();
        });
    }
    
    private async Task OnException(Exception ex, string username)
    {
        Embed exceptionEmbed = embedHelper.Error(
            description: string.Format(CommandLocales.exception_error, ex?.Message ?? ex?.InnerException?.Message, username, Environment.GetEnvironmentVariable("SUPPORT_SERVER"))
        );

        await DeferAsync();
        await ModifyOriginalResponseAsync(props =>
        {
            props.Embed = exceptionEmbed;
            props.Components = new ComponentBuilder().Build();
        });
    }
}