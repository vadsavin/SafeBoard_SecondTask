using System.Collections.Generic;
using System.Linq;

namespace SafeBoard_ScanCLI
{
    public sealed class ArgumentParser
    {
        public static CommandArguments Parse(string[] args)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            if (args.Length < 1) return null;

            string commandName = args[0];

            args = args.Skip(1).ToArray();

            string key = null;
            foreach (string arg in args)
            {
                if(arg.StartsWith("--") && key == null)
                {
                    key = arg;
                }
                else if(!arg.StartsWith("--") && key != null)
                {
                    arguments[key[2..]] = arg;
                    key = null;
                }
            }

            return new CommandArguments(commandName, arguments);
        }
    }
}
