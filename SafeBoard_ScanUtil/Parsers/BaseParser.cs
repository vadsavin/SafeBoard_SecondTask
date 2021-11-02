using SafeBoard_ScanCLI.Commands;
using System.Linq;

namespace SafeBoard_ScanCLI.Parsers
{
    public class BaseParser : IParser
    {
        public string Description { get; } = string.Join("\n\n", _parsers.Select(parser => parser.Description));

        public bool TryParse(CommandArguments args, out ICommand command)
        {
            command = null;

            foreach (var parser in _parsers)
            {
                if (parser.TryParse(args, out command))
                {
                    return true;
                }
            }

            return false;
        }

        private static readonly IParser[] _parsers = new IParser[] 
        { 
            new HelpParser(),
            new StatusParser(),
            new ScanParser()
        };
    }
}
