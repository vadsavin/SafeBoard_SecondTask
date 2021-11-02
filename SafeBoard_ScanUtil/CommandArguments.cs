using System.Collections.Generic;

namespace SafeBoard_ScanCLI
{
    /// <summary>
    /// Хранение имя команды и словаря аргументов.
    /// </summary>
    public class CommandArguments
    {
        private Dictionary<string, string> _args;

        public string CommandName { get; }

        public CommandArguments(string commandName, Dictionary<string, string> args)
        {
            CommandName = commandName;
            _args = args;
        }

        public bool Contains(string arg)
        {
            return _args.ContainsKey(arg);
        }

        public bool TryGetValue(string key, out string value)
        {
            return _args.TryGetValue(key, out value) && !string.IsNullOrEmpty(value);
        } 
    }
}
