using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanAPI.Packets
{
    public class StatusPacket : Packet<StatusPacket>
    {
        public string Guid { get; set; }
        public string Status { get; set; }
    }
}
