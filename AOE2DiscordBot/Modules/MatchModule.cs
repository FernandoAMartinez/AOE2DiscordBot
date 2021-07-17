using AOE2DiscordBot.Models;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace AOE2DiscordBot.Modules
{
    public class MatchModule : ModuleBase<SocketCommandContext>
    {
        //private readonly IConfiguration configuration;
        private readonly HttpClient client;
        private string endpoint;

        public MatchModule()
        {
            client = new HttpClient();
        }

        [Command("matches")]
        [Summary("Gets the current ranking of matchmaking.")]
        public async Task GetLastMatchAsync( int count,  int? since)
        {
            endpoint = "https://aoe2.net/api/matches";
            endpoint += $"?game=aoe2de&count={count}";
            if (since != null) { endpoint += string.Format($"&since={ since }"); }
            //endpoint += $"?game=aoe2de&leaderboard_id={leaderboard_id}&start={start}&count={count}";
            string jsonResult = await client.GetStringAsync(endpoint);
            var matches = JsonConvert.DeserializeObject<Match[]>(jsonResult);

            foreach (var match in matches)
            {
                var builder = new EmbedBuilder()
                    .WithTitle("Random map leaderboard")
                    .AddField("Match Name", match.Name, false)
                    .AddField("Ranked", match.Ranked, true)
                    .Build();

                await Context.Channel.SendMessageAsync("", false, builder);
            }
        }

        [Command("match-detail")]
        [Summary("Gets the detail of a given match by its Match Id")]
        public async Task<Match> GetMatchDetailAsync( string uuid,  string match_id)
        {
            string jsonResult;
            endpoint = "https://aoe2.net/api/matches";
            endpoint += $"?game=aoe2de&uuid={uuid}";
            if (!string.IsNullOrEmpty(match_id)) { endpoint += string.Format($"&match_id={ match_id }"); }


            jsonResult = await client.GetStringAsync(endpoint);
            return JsonConvert.DeserializeObject<Match>(jsonResult);

        }
    }
}
