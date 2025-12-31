namespace Manabot.Modules.Commands.AutoVoice.Themes
{
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