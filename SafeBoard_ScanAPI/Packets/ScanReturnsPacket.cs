using System;

namespace SafeBoard_ScanAPI.Packets
{
    public sealed class ScanReturnsPacket : Packet<ScanReturnsPacket>
    {
        public Guid ScanTaskGuid { get; set; }
        public string Message { get; set; }
        public bool Started { get; set; }
    }
}
