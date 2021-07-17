using Newtonsoft.Json;


namespace AOE2DiscordBot.Models
{
    public class RatingHistory
    {

        [JsonProperty]
        public Rating[] Ratings { get; set; }
    }

    public class Rating 
    {

        [JsonProperty("rating")]
        public int RatingValue { get; set; }
        [JsonProperty("num_wins")]
        public int NumWins { get; set; }
        [JsonProperty("num_losses")]
        public int NumLosses { get; set; }
        [JsonProperty("streak")]
        public int Streak { get; set; }
        [JsonProperty("drops")]
        public int Drops { get; set; }
        [JsonProperty("timestamp")]
        public int TimeStamp { get; set; }
    }
}
