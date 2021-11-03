using SafeBoard_ScanAPI.Contracts;
using System;

namespace SafeBoard_ScanAPI.Packets
{
    public sealed class ScanReturnsPacket : Packet<ScanReturnsPacket>
    {
        public ScanReturns Returns { get; set; }
    }
}
