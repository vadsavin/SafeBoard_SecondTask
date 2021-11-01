using SafeBoard_ScanCLI.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanCLI.Commands
{
    public class HelpCommand : ICommand
    {
        public string Name { get; } = "help";
        public string[] RequiredParams { get; } = new string[] { };
        public string[] OptionalParams { get; } = new string[] { };

        public void Execute(CLI cli, Dictionary<string, string> args)
        {
            var sb = new StringBuilder("These commands are available: \n");
            foreach (var parser in CommonParser.Parsers)
            {
                string info = $"---> {parser.CommandName} " +
                    $"{string.Join(" ", parser.CommandRequiredParams.Select(param => "--" + param))} " +
                    $"{string.Join(" ", parser.CommandOptionalParams.Select(param => "[--"+param+"]"))}";

                sb.AppendLine(info);
            }
            cli.SendMessageToConsole(sb.ToString());
        }
    }
}
