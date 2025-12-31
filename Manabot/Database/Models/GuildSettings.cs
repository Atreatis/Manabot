using Manabot.Database.Models.AutoVoice;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Manabot.Database.Models
{
    public class GuildSettings
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public ulong? GuildId {  get; set; }
        public List<AutoVoiceSettings>? AutoVoiceSettings { get; set; } = [];
        public List<AutoVoiceChannels>? AutoVoiceChannels { get; set; } = [];
    }
}