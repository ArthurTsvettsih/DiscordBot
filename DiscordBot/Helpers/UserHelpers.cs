using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Helpers
{
	public class UserHelpers
	{
		public string GetUserName(SocketGuildUser user)
		{
			return String.IsNullOrWhiteSpace(user.Nickname) ? user.Username : user.Nickname;
		}
	}
}
