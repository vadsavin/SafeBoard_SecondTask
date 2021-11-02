using SafeBoard_ScanCLI.Commands;

namespace SafeBoard_ScanCLI.Parsers
{
    public class HelpParser : IParser
    {
        public string Description { get; } = "help";

        public bool TryParse(CommandArguments args, out ICommand command)
        {
            command = null;
            return false;
        }
    }
}
