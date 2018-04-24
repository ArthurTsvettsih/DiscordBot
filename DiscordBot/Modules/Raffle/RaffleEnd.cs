using Discord;
using Discord.Commands;
using DiscordBot.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Modules.Raffle
{
	public class RaffleEnd : ModuleBase<SocketCommandContext>
	{
		private RaffleHelpers raffleHelpers = new RaffleHelpers();
		private UserHelpers userHelpers = new UserHelpers();

		[Command("raffle_end")]
		public async Task RaffleAsync([Remainder]string raffleName)
		{
			var raffle = raffleHelpers.GetRaffle(raffleName);
			EmbedBuilder embedBuilder = new EmbedBuilder();

			if (raffle.endDate > DateTime.MinValue && raffle.endDate < DateTime.UtcNow)
			{
				embedBuilder
				.WithColor(Color.Red)
				.WithTitle(Properties.Resources.TitleHmm)
				.WithDescription($"{raffle.name} has ended on {raffle.endDate} UTC")
				.AddInlineField("Winner", userHelpers.GetUserName(userHelpers.GetDiscordUser(raffleHelpers.GetRaffleWinner(raffle).discordID, Context)))
				.AddInlineField("Reward", $"{raffle.reward}");

				await ReplyAsync("", false, embedBuilder);
				return;
			}

			raffle.endDate = DateTime.UtcNow;

			double winChance = 0;
			var winner = raffleHelpers.DetermineWinner(raffle, out winChance);
			var winnerDiscord = userHelpers.GetDiscordUser(winner.discordID, Context);

			raffleHelpers.CreateUpdateRaffle(raffle);
			embedBuilder
				.WithColor(Color.Green)
				.WithTitle($"Congratulations {userHelpers.GetUserName(winnerDiscord)}! :tada:")
				.WithDescription($"{winnerDiscord.Mention} has won the {raffle.name} raffle!")
				.AddField("Reward", $"{raffle.reward}")
				.AddField("Win %", winChance);
			await ReplyAsync("", false, embedBuilder);
		}
	}
}
