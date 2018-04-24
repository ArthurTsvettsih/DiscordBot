using Discord;
using Discord.Commands;
using DiscordBot.Extensions;
using DiscordBot.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Modules.Raffle
{
	public class RaffleCreate : ModuleBase<SocketCommandContext>
	{
		private RaffleHelpers raffleHelpers = new RaffleHelpers();

		[Command("raffle_create")]
		public async Task RaffleCreateAsync([Remainder]string raffleDetails)
		{
			Objects.Raffle.Raffle raffle = CreateRaffle(raffleDetails);
			await HandleCreating(raffle);
		}

		//TODO: Consider different guilds
		private async Task HandleCreating(Objects.Raffle.Raffle raffle)
		{
			var filepath = raffleHelpers.GetRaffleFilepath(raffle.name);

			EmbedBuilder embedBuilder = new EmbedBuilder();

			if (!File.Exists(filepath))
			{
				raffleHelpers.CreateUpdateRaffle(raffle);

				embedBuilder
					.WithTitle(Properties.Resources.TitleLetsDoThis)
					.WithDescription($"{raffle.name} has been successfully created!")
					.WithColor(Color.Green);

				await ReplyAsync("", false, embedBuilder);
			}
			else
			{
				embedBuilder
				.WithTitle(Properties.Resources.TitleHmm)
				.WithDescription($"{raffle.name} already exists")
				.WithColor(Color.Red);

				await ReplyAsync("", false, embedBuilder);
			}
		}

		private Objects.Raffle.Raffle CreateRaffle(string raffleDetails)
		{
			var details = raffleDetails.Split(';');
			var raffle = new Objects.Raffle.Raffle()
			{
				name = details[0].Trim(),
				plannedEndDate = WorkoutEndDate(details[1]),
				reward = details[2].Trim(),
				startDate = DateTime.UtcNow,
				creator = Context.User.ToRaffleParticipant()
			};
			return raffle;
		}

		private DateTime WorkoutEndDate(string duration)
		{
			var seconds = Int32.Parse(duration.Split('-')[0]);
			var endDate = DateTime.UtcNow.AddSeconds(seconds);

			return endDate;
		}

	}
}
