using Discord;
using Discord.Interactions;
using Manabot.Database;
using Manabot.Database.Models;
using Manabot.Database.Models.AutoVoice;
using Manabot.Resources;
using Manabot.Utils.Helpers.Embeds;
using MongoDB.Driver;

namespace Manabot.Modules.Commands.AutoVoice.Users;

public class AutoVoiceUsersSelect(IEmbedHelper embedHelper) : InteractionModuleBase<ShardedInteractionContext>
{
    [ComponentInteraction("av_limit_sel_*")]
    public async Task AutoVoiceUsersSelectAsync(string id, ulong[] selectedChannels)
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
        
        Embed autoVoiceUsersEmbed = embedHelper.Generic(
                title: CommandLocales.autovoice_users_select_title,
                description: CommandLocales.autovoice_users_select_description);

        SelectMenuBuilder userLimit = new SelectMenuBuilder()
            .WithPlaceholder(CommandLocales.autovoice_users_select_limit)
            .WithCustomId($"av_users_save_{Context.User.Id}")
            .WithMinValues(1)
            .WithMaxValues(1)
            .AddOption(CommandLocales.autovoice_users_select_limit_default, $"{selectedChannels.First()}, 0")
            .AddOption($"2 {CommandLocales.autovoice_users_select_limit_users}", $"{selectedChannels.First()}, 2")
            .AddOption($"3 {CommandLocales.autovoice_users_select_limit_users}", $"{selectedChannels.First()}, 3")
            .AddOption($"4 {CommandLocales.autovoice_users_select_limit_users}", $"{selectedChannels.First()}, 4")
            .AddOption($"5 {CommandLocales.autovoice_users_select_limit_users}", $"{selectedChannels.First()}, 5")
            .AddOption($"6 {CommandLocales.autovoice_users_select_limit_users}", $"{selectedChannels.First()}, 6")
            .AddOption($"8 {CommandLocales.autovoice_users_select_limit_users}", $"{selectedChannels.First()}, 8")
            .AddOption($"12 {CommandLocales.autovoice_users_select_limit_users}", $"{selectedChannels.First()}, 12")
            .AddOption($"24 {CommandLocales.autovoice_users_select_limit_users}", $"{selectedChannels.First()}, 24");

        ComponentBuilder components = new ComponentBuilder()
            .WithSelectMenu(userLimit);

        await DeferAsync();
        await ModifyOriginalResponseAsync(msgProps =>
        {
            msgProps.Embed = autoVoiceUsersEmbed;
            msgProps.Components = components.Build();
        });
    }

    private async Task OnChannelNotFound()
    {
        Embed noAutoVoiceChannelEmbed = embedHelper.Error(
            description: CommandLocales.autovoice_channel_notfound);
            
        await DeferAsync();
        await ModifyOriginalResponseAsync(msgProps =>
        {
            msgProps.Embed = noAutoVoiceChannelEmbed;
            msgProps.Components = new ComponentBuilder().Build();
        });
    }
}