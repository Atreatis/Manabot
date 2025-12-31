using System.ComponentModel.DataAnnotations;

namespace Manabot.Database.Models.AutoVoice
{
    public class AutoVoiceSettings
    {
        public ulong? VoiceChannelId { get; set; }
        
        [StringLength(24)]
        public string? VoiceChannelTheme { get; set; }
        public int VoiceChannelUsers { get; set; }
    }   
}