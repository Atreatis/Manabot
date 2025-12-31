using Discord;
using Discord.Interactions;
using Manabot.Resources;
using Manabot.Utils.Helpers.Embeds;

namespace Manabot.Modules.Commands.AutoVoice.Users;

public class AutoVoiceUsers(IEmbedHelper embedHelper) : InteractionModuleBase<ShardedInteractionContext>
{
    [ComponentInteraction("av_limit_set_*")]
    public async Task AutoVoiceUsersAsync()
    {
        Embed autoVoiceUsersEmbed = embedHelper.Generic(
            title: CommandLocales.autovoice_users_title,
            description: CommandLocales.autovoice_users_description
        );

        SelectMenuBuilder? channelMenuBuilder = new SelectMenuBuilder()
            .WithPlaceholder(CommandLocales.autovoice_users_voicechannel)
            .WithCustomId($"av_limit_sel_{Context.User.Id}")
            .WithMinValues(1)
            .WithMaxValues(1)
            .WithType(ComponentType.ChannelSelect)
            .WithChannelTypes(ChannelType.Voice);

        ComponentBuilder? components = new ComponentBuilder()
            .WithSelectMenu(channelMenuBuilder);

        await DeferAsync();
        await ModifyOriginalResponseAsync(msgProps =>
        {
            msgProps.Embed = autoVoiceUsersEmbed;
            msgProps.Components = components.Build();
        });
    }
}