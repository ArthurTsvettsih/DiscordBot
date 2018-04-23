using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Modules.Raffle
{
	public class RaffleAdd : ModuleBase<SocketCommandContext>
	{

		//Verify that is a valid user

		//[Command("raffle_add")]
		public async Task RaffleAsync([Remainder]string userToAdd)
		{
			var csvContent = System.IO.File.ReadAllText(Settings.I.raffleParticipantFilepath);
			var participants = csvContent.Split(',').ToList();

			//if(participants.Any)

			await ReplyAsync("a");
		}
	}
}
