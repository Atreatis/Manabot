using Discord;
using Discord.WebSocket;
using Manabot.Utils.Helpers.Embeds;

namespace Manabot.Utils.Helpers.Embeds;

public class EmbedHelper(DiscordShardedClient client) : IEmbedHelper
{
    private Embed BuildEmbed(string title,
        string description,
        Color color,
        string? image = null,
        List<(string Name, string Value, bool Inline)>? fields = null)
    {
        EmbedBuilder builder = new EmbedBuilder()
            .WithTitle(title)
            .WithDescription(description)
            .WithColor(color)
            .WithAuthor(client.CurrentUser.Username, client.CurrentUser.GetAvatarUrl() ?? client.CurrentUser.GetDefaultAvatarUrl())
            .WithFooter($"Powered by {client.CurrentUser.Username}")
            .WithImageUrl(image)
            .WithCurrentTimestamp();

        if (fields == null) return builder.Build();
        foreach ((string name, string value, bool inline) in fields)
            builder.AddField(name, value, inline);

        return builder.Build();
    }

    public Embed Success(string description, string title = "✅ Success", string? image = null, List<(string, string, bool)>? fields = null)
        => BuildEmbed(title, description, Color.Green, image, fields);
    
    public Embed Generic(string description, string title = "▶️ Generic", string? image = null, List<(string, string, bool)>? fields = null)
        => BuildEmbed(title, description, Color.Purple, image, fields);

    public Embed Error(string description, string title = "❌ Error", string? image = null, List<(string, string, bool)>? fields = null)
        => BuildEmbed(title, description, Color.Red, image, fields);

    public Embed Warning(string description, string title = "⚠️ Warning", string? image = null, List<(string, string, bool)>? fields = null)
        => BuildEmbed(title, description, Color.Orange, image, fields);

    public Embed Info(string description, string title = "ℹ️ Info", string? image = null, List<(string, string, bool)>? fields = null)
        => BuildEmbed(title, description, Color.Blue, image, fields);
}