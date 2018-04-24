using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordBot
{
	class Program
	{
		private DiscordSocketClient _client;
		private CommandService _commands;
		private IServiceProvider _services;

		static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

		public async Task RunBotAsync()
		{
			_client = new DiscordSocketClient();
			_commands = new CommandService();
			_services = new ServiceCollection()
				.AddSingleton(_client)
				.AddSingleton(_commands)
				.BuildServiceProvider();

			string botToken = Settings.I.botKey;

			_client.Log += Log;

			await RegisterCommandsAsync();
			await _client.LoginAsync(TokenType.Bot, botToken);
			await _client.StartAsync();
			await Task.Delay(-1); //Keep the client alive forever
		}

		private Task Log(LogMessage message)
		{
			Console.WriteLine(message);

			return Task.CompletedTask;
		}

		public async Task RegisterCommandsAsync()
		{
			_client.MessageReceived += HandleCommandAsync;

			await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
		}

		private async Task HandleCommandAsync(SocketMessage arg)
		{
			var message = arg as SocketUserMessage;

			if (message is null || message.Author.IsBot) { return; }

			int argumentPosition = 0;

			if (message.HasStringPrefix(Settings.I.botKeyWord, ref argumentPosition) || message.HasMentionPrefix(_client.CurrentUser, ref argumentPosition))
			{
				var context = new SocketCommandContext(_client, message);

				var result = await _commands.ExecuteAsync(context, argumentPosition, _services);

				if (!result.IsSuccess)
				{
					Console.WriteLine(result.ErrorReason);
				}
			}
		}
	}
}
