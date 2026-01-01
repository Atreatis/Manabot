namespace Manabot.Modules.Commands.AutoVoice.Themes.Channels;

/// <summary>
/// Final Fantasy: Related Channel Names
/// </summary>
public class FinalFantasy : ITheme
{
    public string GetRandom() => List[new Random().Next(List.Count)];
    private static List<string> List =>
    [
        "Crystal Chamber",
        "Chocobo Stable",
        "Moogle Mail",
        "Cid’s Workshop",
        "Airship Dock",
        "Materia Vault",
        "Echoes of Light",
        "Limit Break",
        "Summoner’s Call",
        "Phoenix Down",
        "Potion Shop",
        "Inn at Rest",
        "Job Board",
        "Weapon Smithy",
        "Guild Hall",
        "Training Grounds",
        "Blue Magic",
        "Eorzea Tavern",
        "World Map",
        "Victory Fanfare",
        "Active Time Battle",
        "Guardian Force",
        "Sphere Grid",
        "Aurora Sanctuary",
        "Final Fantasy",
        "Crystal Core",
        "Chocobo Crossing",
        "Carbuncle’s Den",
        "Tonberry Lane",
        "Cactuar Field",
        "Limit Gauge",
        "Esper’s Haven",
        "Mystic Ruins",
        "The Gold Saucer",
        "Hunter’s Guild",
        "Celestial Path",
        "Dragoons’ Roost",
        "Black Mage Tower",
        "White Mage Chapel",
        "Warrior’s Rest",
        "Scholar’s Study",
        "Time Mage Spire",
        "Behemoth’s Lair",
        "Mythril Mine",
        "Adamantite Forge",
        "Zanarkand Dreams",
        "Balamb Garden",
        "Cosmo Canyon",
        "Crystal Prelude",
        "Echoes of Eternity"
    ];
}