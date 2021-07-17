using Newtonsoft.Json;

namespace AOE2DiscordBot.Models
{
    public class Profile
    {
        [JsonProperty("profile_id")]
        public int? ProfileId { get; set; }
        [JsonProperty("rank")]
        public int? Rank { get; set; }
        [JsonProperty("rating")]
        public int? Rating { get; set; }
        [JsonProperty("steam_id")]
        public string SteamId { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("clan")]
        public string Clan { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("previous_rating")]
        public int? PreviousRating { get; set; }
        [JsonProperty("highest_rating")]
        public int? HighestRating { get; set; }
        [JsonProperty("streak")]
        public int? Streak { get; set; }
        [JsonProperty("lowest_streak")]
        public int? LowestStreak { get; set; }
        [JsonProperty("highest_streak")]
        public int? HighestStreak { get; set; }
        [JsonProperty("games")]
        public int? Games { get; set; }
        [JsonProperty("wins")]
        public int? Wins { get; set; }
        [JsonProperty("losses")]
        public int? Losses { get; set; }
        [JsonProperty("drops")]
        public int? Drops { get; set; }
        [JsonProperty("last_match")]
        public int? LastMatch { get; set; }
        [JsonProperty("last_match_time")]
        public int? LastMatchTime { get; set; }

        //Test
        [JsonProperty("slot")]
        public int? Slot { get; set; }
        [JsonProperty("slot_type")]
        public int? SlotType { get; set; }
        [JsonProperty("rating_change")]
        public int? RatingChange { get; set; }
        [JsonProperty("color")]
        public int? Color { get; set; }
        [JsonProperty("team")]
        public int? Team { get; set; }
        [JsonProperty("civ")]
        public int? Civilization { get; set; }
        [JsonProperty("won")]
        public bool? Won { get; set; }
    }
}
