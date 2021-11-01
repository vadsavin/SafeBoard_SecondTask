using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanCLI.Commands
{
    public class StatusCommand : ICommand
    {
        public string Name { get; } = "status";
        public string[] RequiredParams { get; } = new string[] { "guid" };
        public string[] OptionalParams { get; } = new string[] { };

        public void Execute(CLI cli, Dictionary<string, string> args)
        {
            if (args.TryGetValue("guid", out string guid))
            {
                cli.GetStatus(guid);
            }
        }
    }
}
