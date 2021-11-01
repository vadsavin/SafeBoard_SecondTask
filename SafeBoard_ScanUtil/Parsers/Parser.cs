using SafeBoard_ScanCLI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanCLI.Parsers
{
    public class Parser<T> : BaseParser where T : ICommand, new()
    {
        private ICommand _command = new T();

        public override string CommandName => _command.Name;
        public override string[] CommandRequiredParams => _command.RequiredParams;
        public override string[] CommandOptionalParams => _command.OptionalParams;

        public override bool TryParse(Dictionary<string, string> args, out ICommand command)
        {
            return TryParseBase<T>(args, out command);
        }
    }
}
