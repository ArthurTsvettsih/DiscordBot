using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Objects.Raffle
{
	public class RaffleParticipant
	{
		public string username { get; set; }
		public string nickname { get; set; }
		public ulong discordID { get; set; }
		public DateTime dateAdded { get; set; }
		public bool isWinner { get; set; }
	}
}
