using AOE2DiscordBot.Models;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace AOE2DiscordBot.Modules
{
    public class LeaderboardModule : ModuleBase<SocketCommandContext>
    {
        //private readonly IConfiguration configuration;
        private readonly HttpClient client;
        private string endpoint;

        public LeaderboardModule()
        {
            client = new HttpClient();
            //configuration = config;
            //endpoint = configuration["LeaderboardAPI"];
        }

        [Command("ranking")]
        [Summary("Gets the current ranking of matchmaking.")]
        public async Task GetRankingAsync(int leaderboard_id, int start, int count)
        {
            endpoint = "https://aoe2.net/api/leaderboard";
            endpoint += $"?game=aoe2de&leaderboard_id={leaderboard_id}&start={start}&count={count}";
            string jsonResult = await client.GetStringAsync(endpoint);
            var leaderboard = JsonConvert.DeserializeObject<LeaderboardResult>(jsonResult);

            //Use here the object result
            //var builder = new EmbedBuilder();
            //builder.Title = "Current Leaderboard for: 1v1 - Random Map";

            //foreach (var profile in leaderboard.Leaderboard)
            //{
            //    builder.Fields.Add(new EmbedFieldBuilder() { IsInline = true, Name = "Rank", Value = profile.Rank });
            //    builder.Fields.Add(new EmbedFieldBuilder() { IsInline = true, Name = "Rating", Value = profile.Rating });
            //    builder.Fields.Add(new EmbedFieldBuilder() { IsInline = true, Name = "Name", Value = profile.Name });
            //    //builder.Fields.Add(new EmbedFieldBuilder() { IsInline = true, Name = "Wins", Value = profile.Wins });
            //    //builder.Fields.Add(new EmbedFieldBuilder() { IsInline = true, Name = "Losses", Value = profile.Losses });
            //}

            //builder.ThumbnailUrl = Context.User.GetAvatarUrl();
            //builder.Color = Color.DarkGreen;
            //await Context.Channel.SendMessageAsync("", false, builder.Build());

            foreach (var profile in leaderboard.Leaderboard)
            {
                var builder = new EmbedBuilder()
                    .WithTitle("Random map leaderboard")
                    .AddField("Rank", profile.Rank, true)
                    .AddField("Name", profile.Name, true)
                    .AddField("Rating", profile.Rating, true)
                    .AddField("Wins", profile.Wins, true)
                    .AddField("Losses", profile.Losses, true)
                    .Build();

                await Context.Channel.SendMessageAsync("", false, builder);
            }
        }
    }
}
