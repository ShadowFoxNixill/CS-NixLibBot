using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Nixill.Objects;
using NodaTime;

namespace Nixill.NixBot.Commands {
  public class PingCommand {
    public static async Task Respond(MessageCreateEventArgs e) {
      DiscordChannel chan = e.Channel;
      DiscordMessage msg = await chan.SendMessageAsync("Pong! (getting time...)");

      Snowflake startFlake = new Snowflake((long)e.Message.Id);
      Instant startTime = startFlake.Time;
      Console.WriteLine(startTime);
      Snowflake endFlake = new Snowflake((long)msg.Id);
      Instant endTime = endFlake.Time;
      Console.WriteLine(endTime);
      Duration passedTime = endTime - startTime;

      await msg.ModifyAsync("Pong! (" + passedTime.TotalMilliseconds + " ms)");
    }
  }
}