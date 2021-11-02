using NetCoreServer;
using SafeBoard_ScanAPI;
using SafeBoard_ScanAPI.Packets;
using ScanAPI.Contracts;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanCLI.Client
{
    public class ScanClient : TcpClient, ISession
    {
        private readonly MessageHandler _messageHandler;

        public ScanClient(string address, int port)
            : base(address, port)
        {
            _messageHandler = new MessageHandler(this);
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);

            _messageHandler.Handle(message);
        }

        public async Task<ScanReturnsPacket> SendScanDirectory(string directory, ScannerRule[] rules = null, int? maxDegreeOfParallelism = null)
        {
            StartScanPacket packet = new StartScanPacket();
            packet.DirectoryPath = directory;
            packet.Rules = rules;
            packet.MaxDegreeOfParallelism = maxDegreeOfParallelism;

            return await _messageHandler.SendAndWaitAsync<ScanReturnsPacket>(packet);
        }

        public async Task<StatusPacket> SendGetStatus(Guid guid)
        {
            StatusRequestPacket packet = new StatusRequestPacket();
            packet.Guid = guid;

            return await _messageHandler.SendAndWaitAsync<StatusPacket>(packet);
        }
    }
}
