using Discord.Commands;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace AOE2DiscordBot.Modules
{
    //public class InfoModule : ModuleBase<SocketCommandContext>
    //{
    //    // ~say hello world -> hello world
    //    [Command("say")]
    //    [Summary("Echoes a message.")]
    //    public Task SayAsync([Remainder][Summary("The text to echo")] string echo)
    //        => ReplyAsync(echo);

    //    // ReplyAsync is a method on ModuleBase 
    //}

    public class UserRankModule : ModuleBase<SocketCommandContext>
    {
        private readonly HttpClient client;
        public UserRankModule()
        {
            client = new HttpClient();
        }

        [Command("RandomMap")]
        [Summary("Gets the leaderboard rank for an user for 1v1 Random Map")]
        public async Task RandomMapAsync([Remainder][Summary("User to get rank")] string username)
        {

            var jsonResult = await client.GetStringAsync($"https://aoe2.net/api/leaderboard?game=aoe2de&leaderboard_id=3&start=1&count=1&search={username}");
            JObject result = JObject.Parse(jsonResult);
            string rankResult = $"1v1 Random Map: #{(string)result["leaderboard"][0]["rank"]} - {username} - {(string)result["leaderboard"][0]["rating"]}";

            var builder = new Discord.EmbedBuilder();
            builder.WithTitle("Current AOE2 Rating");
            builder.Fields.Add(new Discord.EmbedFieldBuilder() { IsInline = true, Name = "User", Value = username });
            builder.Fields.Add(new Discord.EmbedFieldBuilder() { IsInline = true, Name = "Rank", Value = (string)result["leaderboard"][0]["rank"] });
            builder.Fields.Add(new Discord.EmbedFieldBuilder() { IsInline = true, Name = "Rating", Value = (string)result["leaderboard"][0]["rating"] });
            builder.WithColor(Discord.Color.Red);
            builder.WithImageUrl("https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Google_2015_logo.svg/272px-Google_2015_logo.svg.png");

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("Teamgame")]
        [Summary("Gets the leaderboard rank for an user")]
        public async Task TeamRandomMapAsync([Remainder][Summary("User to get rank")] string username)
        {
            var jsonResult = await client.GetStringAsync($"https://aoe2.net/api/leaderboard?game=aoe2de&leaderboard_id=4&start=1&count=1&search={username}");
            JObject result = JObject.Parse(jsonResult);
            //string rankResult = $"Team Random Map: #{(string)result["leaderboard"][0]["rank"]} - {username} - {(string)result["leaderboard"][0]["rating"]}";

            var builder = new Discord.EmbedBuilder();
            builder.WithTitle("Current AOE2 Rating");
            builder.Fields.Add(new Discord.EmbedFieldBuilder() { IsInline = true, Name = "User", Value = username });
            builder.Fields.Add(new Discord.EmbedFieldBuilder() { IsInline = true, Name = "Rank", Value = (string)result["leaderboard"][0]["rank"] });
            builder.Fields.Add(new Discord.EmbedFieldBuilder() { IsInline = true, Name = "Rating", Value = (string)result["leaderboard"][0]["rating"] });
            builder.WithColor(Discord.Color.Red);

            await Context.Channel.SendMessageAsync("", false, builder.Build());

            //await ReplyAsync(rankResult);
        }

        [Command("LastMatch")]
        [Summary("Gets the latest match of the user")]
        public async Task LastMatchAsync([Remainder][Summary("User to get match")] string username)
        {
            var userResult = await client.GetStringAsync($"https://aoe2.net/api/leaderboard?game=aoe2de&leaderboard_id=3&start=1&count=1&search={username}");
            JObject result1 = JObject.Parse(userResult);
            var profileId = (string)result1[0]["profile_id"];
            var jsonResult = await client.GetStringAsync($"https://aoe2.net/api/player/lastmatch?game=aoe2de&profile_id={profileId}");
            JObject result = JObject.Parse(jsonResult);

            var builder = new Discord.EmbedBuilder();
            builder.WithTitle("Current AOE2 Rating");
            builder.Fields.Add(new Discord.EmbedFieldBuilder() { IsInline = true, Name = "User", Value = username });
            builder.Fields.Add(new Discord.EmbedFieldBuilder() { IsInline = true, Name = "Rank", Value = (string)result["last_match"][0]["match_uuid"] });
            builder.Fields.Add(new Discord.EmbedFieldBuilder() { IsInline = true, Name = "Rating", Value = (string)result["last_match"][0]["name"] });
            var players = result["last_match"][0]["players"];
            builder.Fields.Add(new Discord.EmbedFieldBuilder() { IsInline = true, Name = "Rating", Value = (string)players[0]["name"] });

            builder.WithColor(Discord.Color.Red);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
    }
}
