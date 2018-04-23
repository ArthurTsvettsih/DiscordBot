using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
	public class Ping : ModuleBase<SocketCommandContext>
	{
		
		[Command("ping")]
		public async Task PingAsync()
		{
			EmbedBuilder embedBuilder = new EmbedBuilder();

			embedBuilder.AddField("Filed 1", "test1")
				.AddInlineField("Field 2", "test2")
				.AddInlineField("Field 3", "test3")
				.AddInlineField("Field 4", "test4");

			await ReplyAsync("", false, embedBuilder.Build());
			//await ReplyAsync("Lee is shit" , true);
		}
	}
}
