using Discord;
using Discord.Interactions;
using Manabot.Database;
using Manabot.Database.Models;
using Manabot.Database.Models.AutoVoice;
using Manabot.Resources;
using Manabot.Utils.Embeds;
using MongoDB.Driver;

namespace Manabot.Modules.Commands.AutoVoice.Users;

/// <summary>
/// Auto Voice: Users Handler<br/>
/// Set and/or update the user limitation within the Database and enforce new user limits upon newly created channels
/// within the Discord server.
/// </summary>
/// <param name="embedHelper"></param>

public class AutoVoiceUsersHandler(IEmbedHelper embedHelper) : InteractionModuleBase<ShardedInteractionContext>
{
    [ComponentInteraction("av_users_save_*")]
    public async Task AutoVoiceUsersHandlerAsync(string id, string[] choices)
    {
        try
        {
            List<string> options = choices.First().Split(", ").ToList();

            if (!ulong.TryParse(options[0], out ulong voiceChannelId) || !int.TryParse(options[1], out int userLimit))
            {
                await OnParseError();
                return;
            }

            // Fetch data for AutoVoice Channels
            FilterDefinition<GuildSettings> guildFilter = Builders<GuildSettings>.Filter.Eq(x => x.GuildId, Context.Guild.Id);
            GuildSettings guildSettings = await new DbConnect().FindOneAsync("GuildSettings", guildFilter);
            
            // Put Voice Settings into one list
            List<AutoVoiceSettings>? settings = guildSettings.AutoVoiceSettings;
            AutoVoiceSettings? voiceChannel = settings?.Find(x => x.VoiceChannelId == voiceChannelId);

            // Check for duplicate settings
            if (voiceChannel?.VoiceChannelUsers == userLimit)
            {
                await OnDuplicateSetting();
                return;
            }
            
            // Set update definitions and update user limit in Array
            FilterDefinition<GuildSettings>? filter = Builders<GuildSettings>.Filter.Eq(x => x.GuildId, Context.Guild.Id) &
                                                      Builders<GuildSettings>.Filter.Eq("AutoVoiceSettings.VoiceChannelId", voiceChannel!.VoiceChannelId);
            UpdateDefinition<GuildSettings>? update = Builders<GuildSettings>.Update.Set("AutoVoiceSettings.$.VoiceChannelUsers", userLimit);
            await new DbConnect().UpdateOneAsync("GuildSettings", filter, update);

            await OnSuccess(voiceChannelId, userLimit);
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

    private async Task OnSuccess(ulong channelId, int userLimit)
    {
        Embed channelLimitSetEmbed = embedHelper.Success(
            title: CommandLocales.autovoice_users_select_limit_set_title,
            description: string.Format(CommandLocales.autovoice_users_select_limit_set_description, $"<#{channelId}>", userLimit)
        );

        await DeferAsync();
        await ModifyOriginalResponseAsync(msgProps =>
        {
            msgProps.Embed = channelLimitSetEmbed;
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