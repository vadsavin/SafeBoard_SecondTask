namespace SafeBoard_ScanCLI.Commands
{
    public class ScanCommand : BaseCommand, ICommand
    {
        public string DirectoryPath { get; }

        public ScanCommand(string directoryPath) : base()
        {
            DirectoryPath = directoryPath;
        } 

        public void Execute()
        {
            var result = _facade.ScanDirectory(DirectoryPath);

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
