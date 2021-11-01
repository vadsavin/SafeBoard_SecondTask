using SafeBoard_ScanCLI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanCLI.Parsers
{
    /// <summary>
    /// Основа для парсера команд.
    /// </summary>
    public abstract class BaseParser
    {
        public virtual string CommandName { get; }
        public virtual string[] CommandRequiredParams { get; }
        public virtual string[] CommandOptionalParams { get; }

        public abstract bool TryParse(Dictionary<string, string> args, out ICommand command);

        protected virtual bool TryParseBase<T>(Dictionary<string, string> args, out ICommand command) where T : ICommand, new()
        {
            var tempCommand = new T();
            if (CheckCommandName(args) && CheckRequiredParams(args) && CheckOptionalParams(args))
            {
                command = new T();
                return true;
            }

            command = null;
            return false;
        }

        protected bool CheckRequiredParams(Dictionary<string, string> args)
        {
            foreach (string param in CommandRequiredParams)
            {
                if (!args.ContainsKey(param))
                {
                    return false;
                }
            }

            return true;
        }

        protected bool CheckOptionalParams(Dictionary<string, string> args)
        {
            foreach (string param in args.Keys)
            {
                if (!CommandOptionalParams.Contains(param) && !CommandRequiredParams.Contains(param) && param != "name")
                {
                    return false;
                }
            }

            return true;
        }

        protected bool CheckCommandName(Dictionary<string, string> args)
        {
            if (!args.TryGetValue("name", out string ArgsCommandName) || ArgsCommandName != CommandName)
            {
                return false;
            }

            return true;
        }
    }
}
