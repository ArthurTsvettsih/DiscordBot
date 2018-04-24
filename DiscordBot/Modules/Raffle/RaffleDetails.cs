using Discord;
using Discord.Commands;
using DiscordBot.Helpers;
using DiscordBot.Objects.Raffle;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Modules.Raffle
{
	public class RaffleDetails : ModuleBase<SocketCommandContext>
	{
		private RaffleHelpers raffleHelpers = new RaffleHelpers();
		private UserHelpers userHelpers = new UserHelpers();

		[Command("raffle_details")]
		public async Task RaffleAsync([Remainder]string raffleName)
		{
			var raffle = raffleHelpers.GetRaffle(raffleName);

			EmbedBuilder embedBuilder = new EmbedBuilder();
			embedBuilder
				.WithColor(Color.Gold)
				.AddField("Name", raffle.name)
				.AddField("Reward", raffle.reward)
				.AddField("Creator", userHelpers.GetUserName(userHelpers.GetDiscordUser(raffle.creator.discordID, Context)))
				.AddField("Participants #", raffle.participants.Count)
				.AddInlineField("Start", raffle.startDate)
				.AddInlineField("Planned End", raffle.endDate);

			if (raffle.endDate > DateTime.MinValue)
			{
				embedBuilder
					.AddField("Winner", userHelpers.GetUserName(userHelpers.GetDiscordUser(raffleHelpers.GetRaffleWinner(raffle).discordID, Context)))
					.AddField("Ended", raffle.endDate);
			}

			await ReplyAsync("", false, embedBuilder);
			embedBuilder = new EmbedBuilder();
			embedBuilder
				.WithColor(Color.Gold)
				.WithTitle("Participants");

			foreach (var participant in raffle.participants)
			{
				embedBuilder
					.AddField("Name", userHelpers.GetUserName(userHelpers.GetDiscordUser(participant.discordID, Context)))
					.AddInlineField("Added", participant.dateAdded)
					.AddInlineField("Tickets #", participant.noofTickets);
			}
			await ReplyAsync("", false, embedBuilder);
		}
	}
}
