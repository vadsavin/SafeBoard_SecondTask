using SafeBoard_ScanCLI.Commands;

namespace SafeBoard_ScanCLI.Parsers
{
    public interface IParser
    {
        public string Description { get; }

        public bool TryParse(CommandArguments args, out ICommand command);
    }
}
