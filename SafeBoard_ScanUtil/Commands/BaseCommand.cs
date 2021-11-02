namespace SafeBoard_ScanCLI.Commands
{
    /// <summary>
    /// Базовая начинка команды.
    /// </summary>
    public class BaseCommand
    {
        protected readonly ScanFacade _facade;

        public BaseCommand()
        {
            _facade = new ScanFacade();
            _facade.Run();
        }
    }
}
