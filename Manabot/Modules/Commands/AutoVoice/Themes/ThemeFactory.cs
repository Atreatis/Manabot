using Manabot.Modules.Commands.AutoVoice.Themes.Channels;

namespace Manabot.Modules.Commands.AutoVoice.Themes
{
    /// <summary>
    /// ThemeFactory is where a new channel theme is being picked upon creation of new Voice Channels. Theme files can
    /// be found within the Channels folder where all the name chery picking magic happens. 
    /// </summary>
    public class ThemeFactory
    {
        private readonly Dictionary<string, ITheme> _themes = new(StringComparer.OrdinalIgnoreCase)
        {
            { "General", new General() },
            { "Pirates", new Pirates() },
            { "Final Fantasy", new FinalFantasy() },
        };

        public ITheme GetTheme(string themeName)
        {
            return _themes.TryGetValue(themeName, out ITheme? theme) ? theme : throw new ArgumentException($"Theme '{themeName}' not found.");
        }
    }
}