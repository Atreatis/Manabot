using Discord;
using Discord.Interactions;
using Manabot.Database;
using Manabot.Database.Models;
using Manabot.Database.Models.AutoVoice;
using Manabot.Resources;
using Manabot.Utils.Helpers.Embeds;
using MongoDB.Driver;

namespace Manabot.Modules.Commands.AutoVoice.Disable;

public class AutoVoiceDisableHandler(IEmbedHelper embedHelper) : InteractionModuleBase<ShardedInteractionContext>
{
    [ComponentInteraction("av_disable_save_*")]
    public async Task AutoVoiceDisableHandlerAsync(string id, ulong[] selectedChannels)
    {
        try
        {
            // Fetch data for AutoVoice Channels
            FilterDefinition<GuildSettings> guildFilter = Builders<GuildSettings>.Filter.Eq(x => x.GuildId, Context.Guild.Id);
            GuildSettings guildSettings = await new DbConnect().FindOneAsync("GuildSettings", guildFilter);
            
            // Put Voice Settings into one list
            List<AutoVoiceSettings>? settings = guildSettings.AutoVoiceSettings;
            AutoVoiceSettings? voiceSettings = settings?.Find(x => x.VoiceChannelId == selectedChannels.First());
            
            if (voiceSettings == null)
            {
                await OnChanelNotFound();
                return;
            }
            
            // Set update definitions and delete channel from Array
            FilterDefinition<GuildSettings>? filter = Builders<GuildSettings>.Filter.Where(x => x.GuildId == Context.Guild.Id);
            UpdateDefinition<GuildSettings>? update = Builders<GuildSettings>.Update.PullFilter(x => x.AutoVoiceSettings, Builders<AutoVoiceSettings>.Filter.Where(x => x.VoiceChannelId == selectedChannels.First()));
            await new DbConnect().UpdateOneAsync("GuildSettings", filter, update);
            
            await OnChannelDisabled($"<#{selectedChannels.First()}>");
        }

        catch (Exception ex)
        {
            await OnException(ex, Context.User.Username);
        }
    }

    private async Task OnChanelNotFound()
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

    private async Task OnChannelDisabled(string channel)
    {
        Embed autoVoiceDisabledEmbed = embedHelper.Success(
            description: string.Format(CommandLocales.autovoice_disable_channel_success, channel));

        await DeferAsync();
        await ModifyOriginalResponseAsync(msgProps =>
        {
            msgProps.Embed = autoVoiceDisabledEmbed;
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