using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanCLI.Commands
{
    public class ExitCommand : ICommand
    {
        public string Name { get; } = "exit";
        public string[] RequiredParams { get; } = new string[] { };
        public string[] OptionalParams { get; } = new string[] { };

        public void Execute(CLI cli, Dictionary<string, string> args)
        {
            cli.Stop();
        }
    }
}
