using Discord;
using Discord.Interactions;
using Manabot.Resources;
using Manabot.Utils.Helpers.Embeds;

namespace Manabot.Modules.Commands.AutoVoice;

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