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
				.WithColor(Color.Gold)
				.AddField("ab!help", "Displays a list of available commands with examples")
				.AddField("ab!weather London", "Gets the weather information for the specified city")

				//.AddField("ab!raffle_role everyone", "Chooses a random person with the specified role") //Disabled for now
				.AddField("ab!raffle_add @Username", "Add a person to a raffle");

			await ReplyAsync("", false, embedBuilder.Build());
		}
	}
}
