using DiscordBot.Objects.Raffle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DiscordBot.Helpers
{
	public class RaffleHelpers
	{
		public string GetRaffleFilepath(string raffleName)
		{
			return $"{Settings.I.rafflesFolder}/{raffleName}.xml";
		}

		public void CreateUpdateRaffle(Raffle raffle)
		{
			var filepath = GetRaffleFilepath(raffle.name);
			new XmlHelpers().SerializeObject(raffle, filepath);
		}

		public Raffle GetRaffle(string raffleName)
		{
			var filepath = GetRaffleFilepath(raffleName);
			var raffle = new XmlHelpers().DeSerializeObject<Raffle>(filepath);
			return raffle;
		}
	}
}
