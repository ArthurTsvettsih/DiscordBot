using Discord;
using Discord.Commands;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
	public class Weather : ModuleBase<SocketCommandContext>
	{
		[Command("weather")]
		public async Task WeatherAsync([Remainder]string city = "London")
		{
			JToken data = GetYahooData(city);
			EmbedBuilder response = BuildResponse(data);

			await ReplyAsync("", false, response);
		}

		private EmbedBuilder BuildResponse(JToken data)
		{
			EmbedBuilder embedBuilder = new EmbedBuilder();
			var locaiton = data["location"];
			var forecast = data["item"]["forecast"][0];
			embedBuilder
				.WithColor(Color.Gold)
				.AddField("City", $"{locaiton["city"]}, {locaiton["country"]}")
				.AddInlineField("Temperature - High", $"{forecast["high"]}F / {FarenheitToCelcius((double)forecast["high"])}C")
				.AddInlineField("Temperature - Low", $"{forecast["low"]}F / {FarenheitToCelcius((double)forecast["low"])}C")
				.AddField("Sky", forecast["text"]);
			return embedBuilder;
		}

		private static JToken GetYahooData(string city)
		{
			var url = new StringBuilder();
			url.Append(@"https://query.yahooapis.com/v1/public/yql?q=");
			url.Append(UrlEncoder.Default.Encode($"select * from weather.forecast where woeid in (select woeid from geo.places(1) where text=\"{city}\")"));
			url.Append("&format=json&diagnostics=false");

			var result = "";
			using (var client = new WebClient())
			{
				result = client.DownloadString(url.ToString());
			}

			var dataObject = JObject.Parse(result);
			var data = dataObject["query"]["results"]["channel"];
			return data;
		}

		private double FarenheitToCelcius(double farenheit)
		{
			double result = (farenheit - 32) / 1.8;
			return Math.Round(result, 1);
		}
	}
}
