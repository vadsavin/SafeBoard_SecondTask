using System;

namespace SafeBoard_ScanAPI.Packets
{
    public sealed class StartScanPacket : Packet<StartScanPacket>
    {
        public string DirectoryPath { get; set; }
    }
}
