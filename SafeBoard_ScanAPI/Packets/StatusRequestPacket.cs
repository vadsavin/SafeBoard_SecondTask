using System;

namespace SafeBoard_ScanAPI.Packets
{
    public sealed class StatusRequestPacket : Packet<StatusRequestPacket>
    {
        public Guid Guid { get; set; }
    }
}
