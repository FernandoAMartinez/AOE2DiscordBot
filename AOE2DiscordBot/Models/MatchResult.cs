using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AOE2DiscordBot.Models
{
    public class LastMatchResult
    {
        [JsonProperty("profile_id")]
        public int? ProfileId { get; set; }
        [JsonProperty("steam_id")]
        public string SteamId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("last_match")]
        public Match LastMatch { get; set; }
    }

    public class Match
    {
        [JsonProperty("match_id")]
        public string MatchId { get; set; }
        [JsonProperty("lobby_id")]
        public string LobbyId { get; set; }
        [JsonProperty("match_uuid")]
        public string MatchUUID { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("num_players")]
        public int? NumPlayers { get; set; }
        [JsonProperty("num_slots")]
        public int? NumSlots { get; set; }
        [JsonProperty("average_rating")]
        public int? AverageRating { get; set; }
        [JsonProperty("cheats")]
        public bool? Cheats { get; set; }
        [JsonProperty("full_tech_tree")]
        public bool? FullTechTree { get; set; }
        [JsonProperty("ending_age")]
        public int? EndingAge { get; set; }
        [JsonProperty("expansion")]
        public int? Expansion { get; set; }
        [JsonProperty("game_type")]
        public int? GameType { get; set; }
        [JsonProperty("has_custom_content")]
        public int? HasCustomContent { get; set; }
        [JsonProperty("has_password")]
        public bool? HasPassword { get; set; }
        [JsonProperty("lock_speed")]
        public bool? LockSpeed { get; set; }
        [JsonProperty("lock_teams")]
        public bool? LockTeams { get; set; }
        [JsonProperty("map_size")]
        public int? MapSize { get; set; }
        [JsonProperty("map_type")]
        public int? MapType { get; set; }
        [JsonProperty("pop")]
        public int? Population { get; set; }
        [JsonProperty("ranked")]
        public bool? Ranked { get; set; }
        [JsonProperty("leaderboard_id")]
        public int? LeaderboardId { get; set; }
        [JsonProperty("rating_type")]
        public int? RatingType { get; set; }
        [JsonProperty("resources")]
        public int? Resources { get; set; }
        [JsonProperty("rms")]
        public string Rms { get; set; }
        [JsonProperty("scenario")]
        public string Scenario { get; set; }
        [JsonProperty("server")]
        public string Server { get; set; }
        [JsonProperty("shared_exploration")]
        public bool? SharedExploration { get; set; }
        [JsonProperty("speed")]
        public int? Speed { get; set; }
        [JsonProperty("starting_age")]
        public int? StartingAge { get; set; }
        [JsonProperty("team_together")]
        public bool? TeamTogether { get; set; }
        [JsonProperty("team_positions")]
        public bool? TeamPositions { get; set; }
        [JsonProperty("treaty_length")]
        public int? TreatyLength { get; set; }
        [JsonProperty("turbo")]
        public bool? Turbo { get; set; }
        [JsonProperty("victory")]
        public int? Victory { get; set; }
        [JsonProperty("victory_time")]
        public int? VictoryTime { get; set; }
        [JsonProperty("visibility")]
        public int? Visibility { get; set; }
        [JsonProperty("opened")]
        public int? Opened { get; set; }
        [JsonProperty("started")]
        public int? Started { get; set; }
        [JsonProperty("finished")]
        public int? Finished { get; set; }
        [JsonProperty("players")]
        public Profile[] Players { get; set; }
    }
}
