using Newtonsoft.Json;
namespace AOE2DiscordBot.Models
{
    public class GeneralData
    {
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("age")]
        public Age[] Ages { get; set; }
        [JsonProperty("civ")]
        public Civilization[] Civilizations { get; set; }
        [JsonProperty("game_type")]
        public GameType[] GameTypes { get; set; }
        [JsonProperty("leaderboard")]
        public Leaderboard[] Leaderboards { get; set; }
        [JsonProperty("map_size")]
        public MapSize[] MapSizes { get; set; }
        [JsonProperty("map_type")]
        public MapType[] MapTypes { get; set; }
        [JsonProperty("rating_type")]
        public RatingType[] RatingTypes { get; set; }
        [JsonProperty("resources")]
        public Resources[] Resources { get; set; }
        [JsonProperty("speed")]
        public Speed[] Speeds { get; set; }
        [JsonProperty("victory")]
        public Victory[] Victories { get; set; }
        [JsonProperty("visibility")]
        public Visibility[] Visibilities { get; set; }
    }
    public class Age
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("String")]
        public string Name { get; set; }
    }
    public class Civilization
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("String")]
        public string Name { get; set; }
    }
    public class GameType
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("String")]
        public string Name { get; set; }
    }
    public class Leaderboard
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("String")]
        public string Name { get; set; }
    }
    public class MapSize
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("String")]
        public string Name { get; set; }
    }
    public class MapType
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("String")]
        public string Name { get; set; }
    }
    public class RatingType
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("String")]
        public string Name { get; set; }
    }
    public class Resources
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("String")]
        public string Name { get; set; }
    }
    public class Speed
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("String")]
        public string Name { get; set; }
    }
    public class Victory
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("String")]
        public string Name { get; set; }
    }
    public class Visibility
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("String")]
        public string Name { get; set; }
    }
}
