using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscordBot.Helpers
{
	public class UserHelpers : ModuleBase<SocketCommandContext>
	{
		public string GetUserName(SocketGuildUser user)
		{
			return String.IsNullOrWhiteSpace(user.Nickname) ? user.Username : user.Nickname;
		}

		public SocketGuildUser GetDiscordUser(ulong discordID, SocketCommandContext context)
		{
			return context.Guild.Users.Where(user => user.Id == discordID).FirstOrDefault();
		}
	}
}
