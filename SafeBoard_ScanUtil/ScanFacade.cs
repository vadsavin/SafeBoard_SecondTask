using SafeBoard_ScanAPI;
using SafeBoard_ScanAPI.Packets;
using SafeBoard_ScanCLI.Client;
using System;

namespace SafeBoard_ScanCLI
{
    public class ScanFacade
    {
        private readonly ScanClient _client;

        public ScanFacade()
        {
            _client = new ScanClient("127.0.0.1", 2021);
        }

        public ScanReturnsPacket ScanDirectory(string directory)
        {
            return _client.SendScanDirectory(directory).Result;
        }

        public StatusPacket GetStatus(Guid guid)
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
