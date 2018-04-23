using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
	public class Help : ModuleBase<SocketCommandContext>
	{

		[Command("help")]
		public async Task HelpAsync()
		{
			EmbedBuilder embedBuilder = new EmbedBuilder();

			embedBuilder
				.AddField("ab!help", "Displays a list of available commands with examples")
				.AddField("ab!weather London", "Gets the weather information for the specified city")
				.AddField("ab!raffle everyone", "Chooses a random person with the specified role");

			await ReplyAsync("", false, embedBuilder.Build());
		}
	}
}
