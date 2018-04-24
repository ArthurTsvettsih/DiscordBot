using Discord;
using Discord.Commands;
using DiscordBot.Extensions;
using DiscordBot.Helpers;
using DiscordBot.Objects.Raffle;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Modules.Raffle
{
	public class RaffleAdd : ModuleBase<SocketCommandContext>
	{
		private RaffleHelpers raffleHelpers = new RaffleHelpers();
		private UserHelpers userHelpers = new UserHelpers();

		[Command("raffle_add")]
		public async Task RaffleAsync([Remainder]string raffleDetails)
		{
			var userID = Convert.ToUInt64(raffleDetails.Split(';').FirstOrDefault().Trim(' ', '<', '>', '@'));
			var raffleName = raffleDetails.Split(';')[1].Trim(' ');
			var userToAdd = Context.Guild.Users.Where(user => user.Id == userID).FirstOrDefault();
			var noofTickets = Int32.Parse(raffleDetails.Split(';').LastOrDefault().Trim(' '));
			var raffleFilepath = raffleHelpers.GetRaffleFilepath(raffleName);
			EmbedBuilder embedBuilder = new EmbedBuilder();
			
			if (!File.Exists(raffleFilepath))
			{
				embedBuilder
				.WithTitle(Properties.Resources.TitleUhOh)
				.WithDescription($"{raffleName} does not exist!")
				.WithColor(Color.Red);
				await ReplyAsync("", false, embedBuilder);
				return;
			}
			var raffle = raffleHelpers.GetRaffle(raffleName);

			if (raffle.endDate > DateTime.MinValue)
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

			if (raffle.participants.Any(participant => participant.discordID == userToAdd.Id))
			{
				embedBuilder
				.WithTitle(Properties.Resources.TitleHmm)
				.WithDescription($"{userHelpers.GetUserName(userToAdd)} is already participating!")
				.WithColor(Color.Red);
				await ReplyAsync("", false, embedBuilder);
				return;
			}

			var newParticipant = userToAdd.ToRaffleParticipant();
			raffle.participants.Add(newParticipant);
			raffleHelpers.CreateUpdateRaffle(raffle);

			embedBuilder
			.WithTitle(Properties.Resources.TitleYay)
			.WithDescription($"{userHelpers.GetUserName(userToAdd)} has been added to {raffle.name}!")
			.WithColor(Color.Green);

			await ReplyAsync("", false, embedBuilder);
		}
	}
}
