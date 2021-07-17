using Newtonsoft.Json;

namespace AOE2DiscordBot.Models
{
    public class PlayersOnline
    {
        [JsonProperty("app_id")]
        public int AppId { get; set; }
        [JsonProperty("player_stats")]
        public PlayerStats PlayerStats { get; set; }
    }

    public class PlayerStats 
    {
        [JsonProperty("time")]
        public int Time { get; set; }
        [JsonProperty("num_players")]
        public NumPlayers NumPlayers { get; set; }
    }

    public class NumPlayers
    {
        [JsonProperty("steam")]
        public int SteamPlayers { get; set; }
        [JsonProperty("multiplayer")]
        public int Multiplayer { get; set; }
        [JsonProperty("looking")]
        public int LookingForGame { get; set; }

        [JsonProperty("ing_game")]
        public int InGame { get; set; }
        [JsonProperty("multiplayer_1h")]
        public int Multiplayer1h { get; set; }
        [JsonProperty("multiplayer_24")]
        public int Multiplayer24h { get; set; }
    }
}
