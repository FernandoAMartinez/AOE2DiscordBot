using AOE2DiscordBot.Services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AOE2DiscordBot
{
    class Program
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService command;
        private readonly IServiceProvider service;

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
        private Program()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });

            command = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Verbose,
                DefaultRunMode = RunMode.Async,
                CaseSensitiveCommands = false
            });

            //client.Log += Log;
            //command.Log += Log;

            // Setup your DI container.
            service = ConfigureServices();
        }

        private IServiceProvider ConfigureServices()
        {
            var map = new ServiceCollection()
            // Repeat this for all the service classes
            // and other dependencies that your commands might need.
            //.AddSingleton(new SomeServiceClass());
            .AddSingleton(new CommandHandler(client, command, service))
            .AddSingleton(new LoggingService(client, command))
            .AddScoped<StringServices>();
            //.AddScoped<IStringServices, StringServices>()
            //.AddScoped<ILeaderboardService, LeaderboardServices>()
            //.AddScoped<IPlayerServices, PlayerServices>()
            //.AddScoped<IMatchServices, MatchServices>();

            // When all your required services are in the collection, build the container.
            // Tip: There's an overload taking in a 'validateScopes' bool to make sure
            // you haven't made any mistakes in your dependency graph.
            return map.BuildServiceProvider();
        }



        public async Task MainAsync()
        {
            //// When working with events that have Cacheable<IMessage, ulong> parameters,
            //// you must enable the message cache in your config settings if you plan to
            //// use the cached message entity. 
            //var _config = new DiscordSocketConfig { MessageCacheSize = 100 };
            //client = new DiscordSocketClient(_config);

            // Centralize the logic for commands into a separate method.
            await InitCommands();

            // Login and connect.
            await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DiscordToken"));

            //client.Ready += async () => 
            await client.StartAsync();

            client.Ready += () =>
            {
                Console.WriteLine($"[{DateTime.Now}]: {client.CurrentUser} is {client.ConnectionState} at {client.Latency}ms");
                return Task.CompletedTask;
            };

            //await Task.Delay(-1);

            // Wait infinitely so your bot actually stays connected.
            await Task.Delay(Timeout.Infinite);
        }

        private async Task InitCommands()
        {
            // Either search the program and add all Module classes that can be found.
            // Module classes MUST be marked 'public' or they will be ignored.
            // You also need to pass your 'IServiceProvider' instance now,
            // so make sure that's done before you get here.
            await command.AddModulesAsync(Assembly.GetEntryAssembly(), service);
            // Or add Modules manually if you prefer to be a little more explicit:
            //await command.AddModuleAsync<Modules.InfoModule>(service);
            // Note that the first one is 'Modules' (plural) and the second is 'Module' (singular).

            // Subscribe a handler to see if a message invokes a command.
            //client.MessageReceived += HandleCommandAsync;
            await service.GetRequiredService<CommandHandler>()
                .InstallCommandsAsync();
        }

        //private async Task HandleCommandAsync(SocketMessage arg)
        //{
        //    var msg = arg as SocketUserMessage;
        //    if (msg == null) return;

        //    if (msg.Author.Id == client.CurrentUser.Id || msg.Author.IsBot) return;

        //    int pos = 0;
        //    if (msg.HasCharPrefix('!', ref pos) /* || msg.HasMentionPrefix(_client.CurrentUser, ref pos) */)
        //    {
        //        var context = new SocketCommandContext(client, msg);

        //        var result = await command.ExecuteAsync(context, pos, service);

        //        //if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
        //        //    await msg.Channel.SendMessageAsync(result.ErrorReason);
        //    }
        //}

        private Task Log(LogMessage msg)
        {
            Console.WriteLine($"[{DateTime.Now}][{msg.Severity}]: {msg.Message}");
            return Task.CompletedTask;
        }
    }
}
