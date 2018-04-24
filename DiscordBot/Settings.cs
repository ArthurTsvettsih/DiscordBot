using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot
{
	class Settings
	{
		public string botKey;
		public string raffleParticipantFilepath;
		public string rafflesFolder;
		public string botKeyWord;

		private const string _settingsFilePath = "C:/DiscordBot/Settings.txt";
		
		private void SetSettings(JObject settings)
		{
			botKey = settings["BotKey"].ToString();
			raffleParticipantFilepath = settings["RaffleParticipantFilepath"].ToString();
			rafflesFolder = settings["RafflesFolder"].ToString();
			botKeyWord = settings["BotKeyWord"].ToString();
		}

		#region Singleton Implementation
		private static Settings instance;

		private Settings()
		{
			string json = System.IO.File.ReadAllText(_settingsFilePath);
			var jObject = JObject.Parse(json);
			SetSettings(jObject);
		}

		public static Settings I
		{
			get
			{
				if (instance == null)
				{
					instance = new Settings();
				}
				return instance;
			}
		}
		#endregion
	}
}
