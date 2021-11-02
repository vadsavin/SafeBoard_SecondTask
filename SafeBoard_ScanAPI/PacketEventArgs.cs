using SafeBoard_ScanAPI.Packets;
using System;

namespace SafeBoard_ScanAPI
{
    public class PacketEventArgs : EventArgs
    {
        public IPacket Packet { get; set; }

        public PacketEventArgs(IPacket packet)
        {
            Packet = packet;
        }
    }
}
