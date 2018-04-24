using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Objects.Raffle
{
	public class Raffle
	{
		public string name { get; set; }
		public DateTime plannedEndDate { get; set; }
		public string reward { get; set; }
		public List<RaffleParticipant> participants { get; set; }
		public DateTime endDate { get; set; }
	}
}
