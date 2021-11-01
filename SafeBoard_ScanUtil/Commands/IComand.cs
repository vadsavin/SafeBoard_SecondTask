using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanCLI.Commands
{
    /// <summary>
    /// Основа для создания команды.
    /// </summary>
    public interface ICommand
    {
        public string Name { get; }
        public string[] RequiredParams { get; }
        public string[] OptionalParams { get; }

        public void Execute(CLI cli, Dictionary<string, string> args);
    }
}
