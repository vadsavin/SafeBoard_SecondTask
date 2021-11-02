using SafeBoard_ScanCLI.Commands;

namespace SafeBoard_ScanCLI.Parsers
{
    public class ScanParser : IParser
    {
        public string Description { get; } = "scan --d <path>";

        public bool TryParse(CommandArguments args, out ICommand command)
        {
            command = null;

            if (args.CommandName != "scan") return false;

            if (!args.TryGetValue("d", out var directoryPath)) return false;

            command = new ScanCommand(directoryPath);
            
            return true;
        }
    }
}
