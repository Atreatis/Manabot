using Discord;

namespace Manabot.Utils.Helpers.Embeds;

public interface IEmbedHelper
{
    Embed Success(string description, string title = "✅ Success", string? image = null, List<(string, string, bool)>? fields = null);
    Embed Generic(string description, string title = "▶️ Generic", string? image = null, List<(string, string, bool)>? fields = null);
    Embed Error(string description, string title = "❌ Error", string? image = null, List<(string, string, bool)>? fields = null);
    Embed Warning(string description, string title = "⚠️ Warning", string? image = null, List<(string, string, bool)>? fields = null);
    Embed Info(string description, string title = "ℹ️ Info", string? image = null, List<(string, string, bool)>? fields = null);
}