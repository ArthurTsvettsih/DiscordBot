using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Modules.Raffle
{
	public class RaffleRole : ModuleBase<SocketCommandContext>
	{
		//[Command("raffle_role")]
		public async Task RaffleAsync([Remainder]string roleInput = "everyone")
		{
			var parsedRoleInput = roleInput.ToLower().Replace("@", String.Empty);
			var raffleRole = Context.Guild.Roles.Where(role => role.Name.ToLower().Replace("@", String.Empty) == parsedRoleInput).FirstOrDefault();
			var applicableUsers = Context.Guild.Users.Where(user => user.Roles.Any(userRoles => userRoles == raffleRole) && !user.IsBot && !user.IsWebhook).ToList();

			var randomiser = new Random();
			var winnerIndex = randomiser.Next(0, applicableUsers.Count()); //max value is exlusive i.e. Next() will maxValue -1 by itself
			Console.WriteLine($"{applicableUsers.Count} - {winnerIndex}");
			await ReplyAsync(applicableUsers[winnerIndex].Mention);
		}
	}
}
