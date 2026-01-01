using Discord;

namespace Manabot.Utils.Embeds;

/// <summary>
/// IEmbed Helper are pre-designed embeds that can be used for any kinds of output, these embeds should still be
/// re-designed for custom titles to provide better coverage over future generic embeds within the Discord bot.
/// </summary>

public interface IEmbedHelper
{
    Embed Success(string description, string title = "✅ Success", string? image = null, List<(string, string, bool)>? fields = null);
    Embed Generic(string description, string title = "▶️ Generic", string? image = null, List<(string, string, bool)>? fields = null);
    Embed Error(string description, string title = "❌ Error", string? image = null, List<(string, string, bool)>? fields = null);
    Embed Warning(string description, string title = "⚠️ Warning", string? image = null, List<(string, string, bool)>? fields = null);
    Embed Info(string description, string title = "ℹ️ Info", string? image = null, List<(string, string, bool)>? fields = null);
}