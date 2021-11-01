using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanCLI.Commands
{
    public class ScanCommand : ICommand
    {
        public string Name { get; } = "scan";
        public string[] RequiredParams { get; } = new string[] { "d" };
        public string[] OptionalParams { get; } = new string[] { };

        public void Execute(CLI cli, Dictionary<string, string> args)
        {
            if (args.TryGetValue("d", out var directory))
            {
                cli.ScanDirectory(directory);
            }
        }
    }
}
