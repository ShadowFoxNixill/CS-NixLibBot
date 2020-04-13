using System.Text.RegularExpressions;
using System;
using Nixill.NixBot.Commands;
using DSharpPlus.EventArgs;
using System.Threading.Tasks;

namespace Nixill.NixBot {
  public class MessageHandler {
    // This regex checks for three things:
    // 1. Does it mention anyone? If so, who?
    // -. Does it start with a bang(!)?
    // 2. Is that bang followed by a word char or more? If so, what?
    // 3. Is there a space and then things afterwards? If so, what are the things afterwards?
    private static Regex Command = new Regex(@"^((?:\<\@\!?\d{17,19}\> )*)!(\w+)(?: (.+))?$");

    public static async Task Handle(MessageCreateEventArgs e) {
      // What does the message actually say?
      string msg = e.Message.Content;
      Console.WriteLine(msg);
      // Does it match to our regex?
      Match mtc = Command.Match(msg);
      if (mtc.Success) {
        // What are the mentions at the start? (This'll return a value even if that value is empty.)
        string mentions = mtc.Groups[1].Value;
        // Do those mentions include us?
        bool mentioned = mentions.Contains(BotMain.discord.CurrentUser.Mention);
        // If the command mentions anyone, it must mention us. Otherwise, stop caring.
        if (!mentioned && (mentions != "")) return;

        // We still here? Great. Let's see what the actual command used was then.
        string cmd = mtc.Groups[2].Value.ToLower();
        string args = mtc.Groups[3]?.Value;

        if (cmd == "ping") {
          await PingCommand.Respond(e);
        }
      }
    }
  }
}