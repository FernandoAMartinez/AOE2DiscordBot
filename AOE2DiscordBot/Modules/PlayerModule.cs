using AOE2DiscordBot.Models;
using Discord.Commands;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace AOE2DiscordBot.Modules
{
    public class PlayerModule : ModuleBase<SocketCommandContext>
    {
        private readonly HttpClient client;
        private string endpoint;

        public PlayerModule()
        {
            client = new HttpClient();
        }

        [Command("RandomMap")]
        [Summary("Gets the leaderboard rank for an user for 1v1 Random Map")]
        public async Task<Match> GetPlayersLastMatchAsync(string steam_id, string profile_id)
        {

            string jsonResult;
            endpoint = "https://aoe2.net/api/player/lastmatch";
            endpoint += $"?game=aoe2de";
            if (steam_id == null && profile_id != null) { endpoint += string.Format($"&profile_id={ profile_id }"); }
            else if (profile_id == null && steam_id != null) { endpoint += string.Format($"&steam_id={ steam_id }"); }
            else if (steam_id != null && profile_id != null) { endpoint += string.Format($"&profile_id={ profile_id }"); }
            else return null;


            jsonResult = await client.GetStringAsync(endpoint);
            return JsonConvert.DeserializeObject<Match>(jsonResult);

        }

        [Command("match-history")]
        [Summary("Retrieves the match history of a given player")]
        public async Task<Match[]> GetMatchHistoryAsync(int? start, int? count, string steam_id, string profile_id, string[] steam_ids, string[] profile_ids)
        {
            string jsonResult;
            endpoint = "https://aoe2.net/api/player/matches";
            endpoint += $"?game=aoe2de&start={start}&count={count}";

            if (steam_id != null) { endpoint += string.Format($"&steam_id={ steam_id }"); }
            if (profile_id != null) { endpoint += string.Format($"&profile_id={ profile_id }"); }
            if (steam_ids != null) { endpoint += string.Format($"&profile_ids={ steam_ids }"); }
            if (profile_ids != null) { endpoint += string.Format($"&profile_ids={ profile_ids }"); }

            jsonResult = await client.GetStringAsync(endpoint);
            return JsonConvert.DeserializeObject<Match[]>(jsonResult);

        }

        [Command("rating-history")]
        [Summary("Retrieves the match history of a given player")]
        public async Task<RatingHistory> GetRatingHistoryAsync(int leaderboard_id, int? start, int? count, string steam_id, string profile_id)
        {

            string jsonResult;
            endpoint = "https://aoe2.net/api/player/ratinghistory";
            endpoint += $"?game=aoe2de&leaderboard_id={leaderboard_id}";
            //string endpoint = $"{_config["PlayerRatingHistoryAPI"]}?game={game}&leaderboard_id={leaderboard_id}&start={start}&count={count}";

            if (start == null) { endpoint += string.Format($"&start={ start }"); }
            if (count == null) { endpoint += string.Format($"&count={ count }"); }
            if (steam_id == null && profile_id != null) { endpoint += string.Format($"&profile_id={ profile_id }"); }
            else if (profile_id == null && steam_id != null) { endpoint += string.Format($"&steam_id={ steam_id }"); }
            else if (steam_id != null && profile_id != null) { endpoint += string.Format($"&profile_id={ profile_id }"); }

            else return null;


            jsonResult = await client.GetStringAsync(endpoint);
            return JsonConvert.DeserializeObject<RatingHistory>(jsonResult);

        }
        [Command("online-players")]
        public async Task<PlayersOnline> GetPlayersOnlineAsync()
        {
            string jsonResult;
            endpoint = "https://aoe2.net/api/stats/players";
            endpoint = $"?game=aoe2de";

            jsonResult = await client.GetStringAsync(endpoint);
            return JsonConvert.DeserializeObject<PlayersOnline>(jsonResult);

        }
    }
}
