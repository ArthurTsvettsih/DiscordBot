using DiscordBot.Objects.Raffle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

		public List<Raffle> GetAllRaffles()
		{
			var raffles = Directory.GetFiles(Settings.I.rafflesFolder);
			var deserialisedRaffles = new List<Raffle>();
			foreach (var raffle in raffles)
			{
				deserialisedRaffles.Add(new XmlHelpers().DeSerializeObject<Raffle>(raffle));
			}

			return deserialisedRaffles;
		}

		public RaffleParticipant DetermineWinner(Raffle raffle, out double winChance)
		{
			List<RaffleParticipant> weightedList = new List<RaffleParticipant>();

			foreach (var participant in raffle.participants)
			{
				for (int i = 0; i < participant.noofTickets; i++)
				{
					weightedList.Add(participant);
				}
			}

			var random = new Random();
			var winnerIndex = random.Next(0, weightedList.Count);
			var winner = weightedList[winnerIndex];
			winner.isWinner = true;

			winChance = CalculateWinChance(winner.noofTickets, weightedList.Count);

			return winner;
		}

		public RaffleParticipant GetRaffleWinner(Raffle raffle)
		{
			return raffle.participants.FirstOrDefault(participant => participant.isWinner);
		}

		private double CalculateWinChance(int noofTickets, int totalTickets)
		{
			return (noofTickets * 100) / totalTickets;
		}
	}
}
