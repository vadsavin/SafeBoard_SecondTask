using SafeBoard_ScanCLI;
using SafeBoard_ScanCLI.Parsers;
using System;

namespace SafeBoard_ScanUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            var arguments = ArgumentParser.Parse(args);

            var parser = new BaseParser();
            if(parser.TryParse(arguments, out var command))
            {
                command.Execute();
            }
            else
            {
                Console.WriteLine(parser.Description);
            }
            
        }
    }
}
