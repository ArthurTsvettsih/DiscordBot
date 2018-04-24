using Discord.WebSocket;
using DiscordBot.Objects.Raffle;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Extensions
{
	public static class SocketUserExtensions
	{
		public static RaffleParticipant ToRaffleParticipant(this SocketUser socketUser)
		{
			return new RaffleParticipant()
			{
				username = socketUser.Username,
				discordID = socketUser.Id,
				dateAdded = DateTime.UtcNow,
				isWinner = false,
				noofTickets = 0
			};
		}
	}
}
