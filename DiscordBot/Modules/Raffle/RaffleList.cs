using Discord;
using Discord.Commands;
using DiscordBot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules.Raffle
{
	public class RaffleList : ModuleBase<SocketCommandContext>
	{
		[Command("raffle_list")]
		public async Task RaffleCreateAsync([Remainder]string type = "current")
		{
			var parsedType = type.ToLower().Trim(' ');
			var raffles = new RaffleHelpers().GetAllRaffles();

			switch (parsedType)
			{
				case "current":
					{
						raffles = raffles.Where(raffle => raffle.plannedEndDate >= DateTime.UtcNow).ToList();
						break;
					}
				case "previous":
					{
						raffles = raffles.Where(raffle => raffle.plannedEndDate < DateTime.UtcNow).ToList();
						break;
					}
				default:
					{
						break;
					}
			}

			foreach (var raffle in raffles)
			{
				EmbedBuilder embedBuilder = new EmbedBuilder();
				embedBuilder
					.AddField("Name", raffle.name)
					.AddInlineField("End Date", raffle.plannedEndDate)
					.AddInlineField("Particiants", raffle.participants.Count)
					.AddInlineField("Price", raffle.reward)
					.WithColor(Color.Blue);
				await ReplyAsync("", false, embedBuilder);
			}
		}
	}
}
