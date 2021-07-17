using Newtonsoft.Json;

namespace AOE2DiscordBot.Models
{
    public class LeaderboardResult
    {
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("leaderboard_id")]
        public int LeaderboardId { get; set; }
        [JsonProperty("start")]
        public int StartValue { get; set; }
        [JsonProperty("count")]
        public int CountResult { get; set; }
        [JsonProperty("leaderboard")]
        public Profile[] Leaderboard{ get; set; }
    }
}
