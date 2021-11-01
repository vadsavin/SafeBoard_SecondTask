using SafeBoard_ScanCLI;
using SafeBoard_ScanCLI.Commands;
using SafeBoard_ScanCLI.Parsers;
using System;

namespace SafeBoard_ScanUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            var cli = new CLI();
            cli.InitializeParser<ExitCommand>();
            cli.InitializeParser<StatusCommand>();
            cli.InitializeParser<ScanCommand>();
            cli.InitializeParser<HelpCommand>();

            cli.Run();

            while (cli.IsRunning)
            {
                var arguments = ArgumentParser.Parse(Console.ReadLine().Split(" "));
                var command = CommonParser.TryGetCommand(arguments);
                if (command != null)
                {
                    command.Execute(cli, arguments);
                }
                else
                {
                    Console.WriteLine("Invalid args or command name.");
                }
            }
        }
    }
}
