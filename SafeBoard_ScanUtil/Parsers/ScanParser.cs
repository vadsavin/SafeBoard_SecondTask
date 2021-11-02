using SafeBoard_ScanCLI.Commands;

namespace SafeBoard_ScanCLI.Parsers
{
    public class ScanParser : IParser
    {
        public string Description { get; } = "scan --d <path> [--r <rulesFilePath>] [--n <maxDegreeOfParallelism>]";

        public bool TryParse(CommandArguments args, out ICommand command)
        {
            command = null;

            if (args.CommandName != "scan") return false;

            if (!args.TryGetValue("d", out var directoryPath)) return false;

            args.TryGetValue("r", out var rulesPath);
            args.TryGetValue("n", out var maxDegreeOfParallelism);
            
            command = new ScanCommand(directoryPath, rulesPath, maxDegreeOfParallelism);
            
            return true;
        }
    }
}
