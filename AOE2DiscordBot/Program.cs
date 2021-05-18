using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using AOE2DiscordBot.Services;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Reflection;

namespace AOE2DiscordBot
{
    class Program
    {
        //public static void Main(string[] args)
        //=> new Program().MainAsync().GetAwaiter().GetResult();
        static void Main(string[] args)
        {
            // Call the Program constructor, followed by the 
            // MainAsync method and wait until it finishes (which should be never).
            new Program().MainAsync().GetAwaiter().GetResult();
        }
        private readonly DiscordSocketClient client;
        private readonly CommandService command;
        private readonly IServiceProvider service;

        private Program()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });

            command = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Info,
                CaseSensitiveCommands = false
            });

            client.Log += Log;
            command.Log += Log;

            // Setup your DI container.
            service = ConfigureServices();
        }

        private IServiceProvider ConfigureServices()
        {
            var map = new ServiceCollection()
            // Repeat this for all the service classes
            // and other dependencies that your commands might need.
            //.AddSingleton(new SomeServiceClass());
            .AddSingleton(new CommandHandler(client, command));

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

            //await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DiscordToken"));
            //await client.StartAsync();

            //client.MessageUpdated += MessageUpdated;
            //client.Ready += () =>
            //{
            //    Console.WriteLine("Bot is connected!");
            //    return Task.CompletedTask;
            //};


            //await Task.Delay(-1);

            // Centralize the logic for commands into a separate method.
            await InitCommands();

            // Login and connect.
            await client.LoginAsync(TokenType.Bot,
                // < DO NOT HARDCODE YOUR TOKEN >
                Environment.GetEnvironmentVariable("DiscordToken"));
            await client.StartAsync();

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
            client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            // Bail out if it's a System Message.
            var msg = arg as SocketUserMessage;
            if (msg == null) return;

            // We don't want the bot to respond to itself or other bots.
            if (msg.Author.Id == client.CurrentUser.Id || msg.Author.IsBot) return;

            // Create a number to track where the prefix ends and the command begins
            int pos = 0;
            // Replace the '!' with whatever character
            // you want to prefix your commands with.
            // Uncomment the second half if you also want
            // commands to be invoked by mentioning the bot instead.
            if (msg.HasCharPrefix('!', ref pos) /* || msg.HasMentionPrefix(_client.CurrentUser, ref pos) */)
            {
                // Create a Command Context.
                var context = new SocketCommandContext(client, msg);

                // Execute the command. (result does not indicate a return value, 
                // rather an object stating if the command executed successfully).
                var result = await command.ExecuteAsync(context, pos, service);

                // Uncomment the following lines if you want the bot
                // to send a message if it failed.
                // This does not catch errors from commands with 'RunMode.Async',
                // subscribe a handler for '_commands.CommandExecuted' to see those.
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                    await msg.Channel.SendMessageAsync(result.ErrorReason);
            }
        }

        //private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        //{
        //    // If the message was not in the cache, downloading it will result in getting a copy of `after`.
        //    var message = await before.GetOrDownloadAsync();
        //    Console.WriteLine($"{message} -> {after}");
        //}

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
