using Discord;
using Discord.Interactions;
using Manabot.Resources;
using Manabot.Utils.Embeds;

namespace Manabot.Modules.Commands.AutoVoice;

/// <summary>
/// Auto Voice: Main Command<br/>
/// This command is used to start a series of embeds in-order to set up, theme, limit or disable a channel for join to
/// create channels.
/// </summary>
/// <param name="embedHelper"></param>

public class AutoVoice(IEmbedHelper embedHelper) : InteractionModuleBase<ShardedInteractionContext>
{
    [CommandContextType(InteractionContextType.Guild)]
    [DefaultMemberPermissions(GuildPermission.ManageGuild | GuildPermission.ManageChannels)]
    [SlashCommand("autovoice", "Setup auto voice (join to create) channels for your Discord server.")]
    public async Task AutoVoiceAsync()
    {
        ComponentBuilder buttonBuilder  = new ComponentBuilder()
            .WithButton(CommandLocales.autovoice_button_setup, $"av_setup_chan_{Context.User.Id}")
            .WithButton(CommandLocales.autovoice_button_theme, $"av_theme_set_{Context.User.Id}", ButtonStyle.Secondary)
            .WithButton(CommandLocales.autovoice_button_limit, $"av_limit_set_{Context.User.Id}", ButtonStyle.Success)
            .WithButton(CommandLocales.autovoice_button_disable, $"av_disable_chan_{Context.User.Id}", ButtonStyle.Danger);

        Embed autoVoiceEmbed = embedHelper.Success(
            title: CommandLocales.autovoice_title,
            description: CommandLocales.autovoice_description);
        
        await RespondAsync(embed: autoVoiceEmbed, components: buttonBuilder.Build(), ephemeral: true);
    }
}