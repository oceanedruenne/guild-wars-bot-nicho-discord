using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace NicholasLeVoyageur
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
  

    

        private DiscordSocketClient client;
        private CommandService commande;
        private IServiceProvider service;

        public async Task RunBotAsync()
        {
            client = new DiscordSocketClient();
            commande = new CommandService();

            service = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(commande)
                .BuildServiceProvider();

            string token = "OTUxNDc2NDU1Mzc0NzMzMzMy.YioBgA.IZa_1sKkNnyuHDG8xqr_hwTE8iU";

            client.Log += Client_Log;

            await RegisterCommandsAsync();

            await client.LoginAsync(TokenType.Bot, token);

            await client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            client.MessageReceived += HandleCommandAsync;
            await commande.AddModulesAsync(Assembly.GetEntryAssembly(), service);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(client, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix("!", ref argPos))
            {
                var result = await commande.ExecuteAsync(context, argPos, service);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }
        }
    }
}
