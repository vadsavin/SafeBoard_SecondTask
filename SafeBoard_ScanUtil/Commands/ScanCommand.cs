using ScanAPI.Contracts;
using System;

namespace SafeBoard_ScanCLI.Commands
{
    public class ScanCommand : BaseCommand, ICommand
    {
        public string DirectoryPath { get; }
        public ScannerRule[] Rules { get; }
        public int? MaxDegreeOfParallelism { get; }

        public ScanCommand(string directoryPath, string rulesPath = null, string maxDegreeOfParallelism = null) : base()
        {
            DirectoryPath = directoryPath;
            Rules = JsonFileReader.Read<ScannerRule[]>(rulesPath);
            try
            {
                MaxDegreeOfParallelism = Int32.Parse(maxDegreeOfParallelism);
            }
            catch { }
            
        } 

        public void Execute()
        {
            var result = _facade.ScanDirectory(DirectoryPath, Rules, MaxDegreeOfParallelism);

            _facade.SendOutput(nameof(result.Started), result.Started.ToString());
            if (result.Started)
            {
                _facade.SendOutput(nameof(result.ScanTaskGuid), result.ScanTaskGuid.ToString());
            }
            else
            {
                _facade.SendOutput(nameof(result.Message), result.Message);
            }
        }
    }
}
