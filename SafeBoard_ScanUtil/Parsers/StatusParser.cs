using SafeBoard_ScanCLI.Commands;
using System;

namespace SafeBoard_ScanCLI.Parsers
{
    public class StatusParser : IParser
    {
        public string Description { get; } = "status --guid <guid>";

        public bool TryParse(CommandArguments args, out ICommand command)
        {
            command = null;

            if (args.CommandName != "status") return false;

            if (!args.TryGetValue("guid", out string guidString)) return false;

            if (!Guid.TryParse(guidString, out Guid guid)) return false;

            command = new StatusCommand(guid);

            return true;
        }
    }
}
