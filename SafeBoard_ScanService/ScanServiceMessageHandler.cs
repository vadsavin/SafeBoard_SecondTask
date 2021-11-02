using SafeBoard_ScanAPI;
using SafeBoard_ScanAPI.Packets;
using SafeBoard_ScanAPI.Server;

namespace SafeBoard_ScanService
{
    public class ScanServiceMessageHandler : MessageHandler
    {
        public ScannerServiceNetworker Service { get; }

        public ScanServiceMessageHandler(ScannerServiceNetworker service, ScanSession session)   
            : base(session)
        {
            Service = service;

            OnRecievedPacket += HandleMessage;
        }

        public void HandleStartScanPacket(StartScanPacket packet)
        {
            var result = Service.StartScan(packet.DirectoryPath, packet.Rules, packet.MaxDegreeOfParallelism);
            SendPacket(result);
        }

        public void HandleStatusRequestPacket(StatusRequestPacket packet)
        {
            var result = Service.GetScanTaskStatus(packet.Guid);
            SendPacket(result);
        }

        private void HandleMessage(object sender, PacketEventArgs e)
        {
            switch (e.Packet)
            {
                case StartScanPacket startScanPacket:
                    HandleStartScanPacket(startScanPacket);
                    break;
                case StatusRequestPacket statusRequestPacket:
                    HandleStatusRequestPacket(statusRequestPacket);
                    break;
            }
        }
    }
}
