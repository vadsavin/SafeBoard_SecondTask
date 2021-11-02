using SafeBoard_ScanAPI.Contracts;

namespace SafeBoard_ScanAPI.Packets
{
    public sealed class StatusPacket : Packet<StatusPacket>
    {
        public ScanStatus Status { get; set; }
    }
}
