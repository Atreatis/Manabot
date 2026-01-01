using Discord.Rest;
using Discord.WebSocket;
using Manabot.Database;
using Manabot.Database.Models;
using Manabot.Database.Models.AutoVoice;
using Manabot.Modules.Commands.AutoVoice.Themes;
using Manabot.Services.Handlers;
using MongoDB.Driver;

namespace Manabot.Modules.Events.Voice;

/// <summary>
/// Event handler for voice channel change states these events will be used in-order to set up a new channel and
/// generate names for Auto Voice channel that is being generated. These voice channels will be randomly made through
/// the theme system within the Auto Voice commands folder.
/// </summary>

public class VoiceChannelSate : IDiscordEventHandler
{
    private DiscordShardedClient _client = null!;
    public void Register(DiscordShardedClient client)
    {
        _client = client;
        _client.UserVoiceStateUpdated += OnVoiceChannelState;
    }

    private async Task OnVoiceChannelState(SocketUser user, SocketVoiceState oldState, SocketVoiceState newState)
    {
        try
        {
            // User joins a channel
            if (newState.VoiceChannel != null)
            {
                SocketGuild guild = _client.GetGuild(newState.VoiceChannel.Guild.Id);

                // Fetch data for AutoVoice Channels
                FilterDefinition<GuildSettings> guildFilter = Builders<GuildSettings>.Filter.Eq(x => x.GuildId, newState.VoiceChannel.Guild.Id);
                GuildSettings guildSettings = await new DbConnect().FindOneAsync("GuildSettings", guildFilter);
                
                // Put Voice Settings into one list
                List<AutoVoiceSettings>? settings = guildSettings.AutoVoiceSettings;
                AutoVoiceSettings? voiceSettings = settings?.Find(x => x.VoiceChannelId == newState.VoiceChannel.Id);
                
                if (voiceSettings != null)
                {
                    ThemeFactory nameFactory = new();
                    if (voiceSettings.VoiceChannelTheme == null) return;
                    ITheme channelName = nameFactory.GetTheme(voiceSettings.VoiceChannelTheme);

                    // Create Voice Channel
                    RestVoiceChannel newChannel = await guild.CreateVoiceChannelAsync(channelName.GetRandom(), props =>
                    {
                        props.UserLimit = voiceSettings.VoiceChannelUsers;
                        props.CategoryId = newState.VoiceChannel.CategoryId;
                    });

                    // Set guild & voice channel Id
                    AutoVoiceChannels voiceChannel = new()
                    {
                        GuildId = guild.Id,
                        VoiceChannelId = newChannel.Id,
                    };

                    // Add Voice Channel to VoiceChannels Array
                    UpdateDefinition<GuildSettings> update = Builders<GuildSettings>.Update.AddToSet(x => x.AutoVoiceChannels, voiceChannel);
                    await new DbConnect().UpdateOneAsync("GuildSettings", guildFilter, update);

                    // Move user safely
                    SocketGuildUser voiceUser = guild.GetUser(user.Id);
                    if (voiceUser != null && voiceUser.VoiceChannel.Id == newState.VoiceChannel.Id)
                    {
                        await voiceUser.ModifyAsync(voice => voice.Channel = newChannel);
                    }
                }
            }

            // User leaves a channel
            if (oldState.VoiceChannel != null)
            {
                // Fetch data for AutoVoice Channels
                FilterDefinition<GuildSettings> guildFilter = Builders<GuildSettings>.Filter.Eq(x => x.GuildId, oldState.VoiceChannel.Guild.Id);
                GuildSettings guildSettings = await new DbConnect().FindOneAsync("GuildSettings", guildFilter);
                
                // Put Voice Settings into one list
                List<AutoVoiceChannels>? channels = guildSettings.AutoVoiceChannels;
                AutoVoiceChannels? voiceChannel = channels?.Find(x => x.VoiceChannelId == oldState.VoiceChannel.Id);
                
                if (voiceChannel?.VoiceChannelId == oldState.VoiceChannel.Id &&
                    oldState.VoiceChannel.ConnectedUsers.Count < 1)
                {
                    // Set update definitions and delete channel from Array
                    FilterDefinition<GuildSettings>? filter = Builders<GuildSettings>.Filter.Where(x => x.GuildId == oldState.VoiceChannel.Guild.Id);
                    UpdateDefinition<GuildSettings>? update = Builders<GuildSettings>.Update.PullFilter(x => x.AutoVoiceChannels, Builders<AutoVoiceChannels>.Filter.Where(x => x.VoiceChannelId == oldState.VoiceChannel.Id));
                    await new DbConnect().UpdateOneAsync("GuildSettings", filter, update);
                    
                    await oldState.VoiceChannel.DeleteAsync();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;  
        }
    }
}