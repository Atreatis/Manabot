namespace Manabot.Modules.Commands.AutoVoice.Themes
{
    /// <summary>
    /// Pick a random channel name from one of the theme files within the Channels folder.
    /// </summary>
    public interface ITheme
    {
        string GetRandom();
    }
}