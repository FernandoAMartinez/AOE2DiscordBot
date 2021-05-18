using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AOE2DiscordBot.Modules
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        // ~say hello world -> hello world
        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder][Summary("The text to echo")] string echo)
            => ReplyAsync(echo);

        // ReplyAsync is a method on ModuleBase 
    }

    public class UserRankModule : ModuleBase<SocketCommandContext>
    {
        private HttpClient client;

        public UserRankModule()
        {
            client = new HttpClient();
        }

        [Command("RM")]
        [Summary("Gets the leaderboard rank for an user")]
        public async Task RandomMapAsync([Remainder][Summary("User to get rank")] string username)
        {
            var jsonResult = await client.GetStringAsync($"https://aoe2.net/api/leaderboard?game=aoe2de&leaderboard_id=3&start=1&count=1&search={username}");
            JObject result = JObject.Parse(jsonResult);
            string rankResult = $"1v1 Random Map: #{(string)result["leaderboard"][0]["rank"]} - {username} - {(string)result["leaderboard"][0]["rating"]}";
            await ReplyAsync(rankResult);
        }

        [Command("TG")]
        [Summary("Gets the leaderboard rank for an user")]
        public async Task TeamRandomMapAsync([Remainder][Summary("User to get rank")] string username)
        {
            var jsonResult = await client.GetStringAsync($"https://aoe2.net/api/leaderboard?game=aoe2de&leaderboard_id=4&start=1&count=1&search={username}");
            JObject result = JObject.Parse(jsonResult);
            string rankResult = $"Team Random Map: #{(string)result["leaderboard"][0]["rank"]} - {username} - {(string)result["leaderboard"][0]["rating"]}";
            await ReplyAsync(rankResult);
        }


    }
}
