using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using Newtonsoft.Json;
using Nixill.Objects;

namespace Nixill.NixBot {
  public class BotMain {
    internal static DiscordClient discord;

    static void Main(string[] args) {
      Config cfg = JsonConvert.DeserializeObject<Config>(File.ReadAllText("settings.json"));

      MainAsync(cfg).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    static async Task MainAsync(Config cfg) {
      Snowflake.DefaultEpoch = Snowflake.DiscordEpoch;

      discord = new DiscordClient(new DiscordConfiguration {
        Token = cfg.Token,
        TokenType = TokenType.Bot
      });

      discord.MessageCreated += async e => await MessageHandler.Handle(e);

      await discord.ConnectAsync();
      await Task.Delay(-1);
    }
  }

  public class Config {
    public string Token;
  }
}