using SafeBoard_ScanAPI.Contracts;
using SafeBoard_ScanCLI.Client;
using ScanAPI.Contracts;
using System;

namespace SafeBoard_ScanCLI
{
    /// <summary>
    /// Реализует взаимодействие с сетевым клиентом.
    /// </summary>
    public class ScanFacade
    {
        private readonly ScanClient _client;

        public ScanFacade()
        {
            _client = new ScanClient("127.0.0.1", 2021);
        }

        public ScanReturns ScanDirectory(string directory, ScannerRule[] rules = null, int? maxDegreeOfParallelism = null)
        {
            return _client.SendScanDirectory(directory, rules, maxDegreeOfParallelism).Result;
        }

        public ScanStatus GetStatus(Guid guid)
        {
            return _client.SendGetStatus(guid).Result;
        }

        public void Run()
        {
            _client.ConnectAsync();
        }

        public void SendOutput(string name, string value)
        {
            Console.WriteLine($"#{name}: {value}");
        }
    }
}
