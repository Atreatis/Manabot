using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Manabot.Database;
using Manabot.Database.Models;
using Manabot.Database.Models.AutoVoice;
using Manabot.Resources;
using Manabot.Utils.Helpers;
using Manabot.Utils.Helpers.Embeds;
using MongoDB.Driver;

namespace Manabot.Modules.Commands.AutoVoice.Setup;

public class AutoVoiceChannelSetupHandler(IEmbedHelper embedHelper) : InteractionModuleBase<ShardedInteractionContext>
{
    [ComponentInteraction("av_setup_sel_*")]
    public async Task AutoVoiceChannelSetupHandlerAsync(string id, ulong[] selectedChannels)
    {
        try
        {
            // Permission Check!
            SocketVoiceChannel voiceChannel = Context.Guild.GetVoiceChannel(selectedChannels.First());
            ChannelPermissions permission = voiceChannel.Guild.CurrentUser.GetPermissions(voiceChannel.Category);
            
            if (!permission.ViewChannel)
            {
                await OnPermissionError(selectedChannels.First(), "View Category, View Channels");
                return;
            }
            
            // Setup: Auto Voice
            FilterDefinition<GuildSettings> guildFilter = Builders<GuildSettings>.Filter.Eq(x => x.GuildId, Context.Guild.Id);
            GuildSettings guildSettings = await new DbConnect().FindOneAsync("GuildSettings", guildFilter);
            
            // Check if Guild Exists
            if (guildSettings?.GuildId == null)
            {
                await CreateGuild.New(Context.Guild.Id);
            }
            
            // Check if Channel Exists
            List<AutoVoiceSettings>? channels = guildSettings?.AutoVoiceSettings;
        
            if (channels?.Find(x => x.VoiceChannelId == selectedChannels.First()) != null)
            {
                await OnChannelExist(selectedChannels.First());
                return;
            }

            // Set new Auto Voice channel
            AutoVoiceSettings autoVoiceChannel = new()
            {
                VoiceChannelId = selectedChannels.First(),
                VoiceChannelTheme = "General",
                VoiceChannelUsers = 0
            };
            
            // Set update definitions and update data in Database
            UpdateDefinition<GuildSettings> update = Builders<GuildSettings>.Update.AddToSet(x => x.AutoVoiceSettings, autoVoiceChannel);
            await new DbConnect().UpdateOneAsync("GuildSettings", guildFilter, update);
            
            // Send out the results
            await OnSuccess(selectedChannels.First());
        }
        
        // Throw exception
        catch (Exception ex)
        {
            await OnException(ex, Context.Client.CurrentUser.Username);
        }
    }

    private async Task OnPermissionError(ulong channelId, string permission)
    {
        Embed permissionErrorEmbed = embedHelper.Error(
            description: string.Format(CommandLocales.autovoice_error_permission, Context.Client.CurrentUser.Username, permission, $"<#{channelId}>")
        );

        await DeferAsync();
        await ModifyOriginalResponseAsync(msgProps =>
        {
            msgProps.Embed = permissionErrorEmbed;
            msgProps.Components = new ComponentBuilder().Build();
        });
    }
    
    private async Task OnSuccess(ulong channelId)
    {
        Embed autoVoiceSetupEmbed = embedHelper.Success(
            string.Format(CommandLocales.autovoice_setup_channel_complete, $"<#{channelId}>"));
            
        await DeferAsync();
        await ModifyOriginalResponseAsync(msgProps =>
        {
            msgProps.Embed = autoVoiceSetupEmbed;
            msgProps.Components = new ComponentBuilder().Build();
        });
    }

    private async Task OnChannelExist(ulong channelId)
    {
        Embed onExistEmbed = embedHelper.Warning(
            description: string.Format(CommandLocales.autovoice_error_channel_exists, $"<#{channelId}>"));
            
        await DeferAsync();
        await ModifyOriginalResponseAsync(msgProps =>
        {
            msgProps.Embed = onExistEmbed;
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