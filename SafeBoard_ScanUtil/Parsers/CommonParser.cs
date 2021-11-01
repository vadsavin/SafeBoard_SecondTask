using SafeBoard_ScanCLI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanCLI.Parsers
{
    /// <summary>
    /// Содержит в себе парсеры команд и даёт возможность подобрать подходящий парсер автоматически.
    /// </summary>
    public class CommonParser
    {
        public static List<BaseParser> Parsers { get; } = new List<BaseParser>();

        public static void AddParser(BaseParser parser)
        {
            Parsers.Add(parser);
        }

        public static void RemoveParser(BaseParser parser)
        {
            Parsers.Remove(parser);
        }

        public static ICommand TryGetCommand(Dictionary<string, string> args)
        {
            foreach (BaseParser parser in Parsers)
            {
                if (parser.TryParse(args, out var command))
                {
                    return command;
                }
            }

            return null;
        }
    }
}
