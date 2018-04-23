using Discord;
using Discord.Commands;
using DiscordBot.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Modules.Raffle
{
	public class RaffleCreate : ModuleBase<SocketCommandContext>
	{
		[Command("raffle_create")]
		public async Task RaffleCreateAsync([Remainder]string raffleDetails)
		{
			Objects.Raffle.Raffle raffle = CreateRaffle(raffleDetails);

			var xml = new Xml();
			var filepath = $"{Settings.I.rafflesFolder}/{raffle.name}/Raffle.xml";

			EmbedBuilder embedBuilder = new EmbedBuilder();

			if (!Directory.Exists($"{Settings.I.rafflesFolder}/{raffle.name}"))
			{
				Directory.CreateDirectory($"{Settings.I.rafflesFolder}/{raffle.name}");
				xml.SerializeObject(raffle, filepath);

				embedBuilder
					.WithTitle("Let's do this! :sunglasses:")
					.WithDescription($"{raffle.name} has been successfully created!");

				await ReplyAsync("", false, embedBuilder);
			}
			else
			{
				embedBuilder
				.WithTitle("Hmmm :thinking:")
				.WithDescription($"{raffle.name} already exists");

				await ReplyAsync("", false, embedBuilder);
			}
		}

		private Objects.Raffle.Raffle CreateRaffle(string raffleDetails)
		{
			var details = raffleDetails.Split(';');
			var raffle = new Objects.Raffle.Raffle()
			{
				name = details[0].Trim(),
				endDate = WorkoutEndDate(details[1]),
				reward = details[0].Trim()
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
