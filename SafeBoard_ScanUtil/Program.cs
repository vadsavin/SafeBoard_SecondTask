using SafeBoard_ScanCLI;
using SafeBoard_ScanCLI.Parsers;
using System;

namespace SafeBoard_ScanUtil
{
    class Program
    {
        static int Main(string[] args)
        {
            var arguments = ArgumentParser.Parse(args);

            var parser = new BaseParser();
            if(parser.TryParse(arguments, out var command))
            {
                command.Execute();
                return 0;
            }
            else
            {
                Console.WriteLine(parser.Description);
                return -1;
            }
        }
    }
}
