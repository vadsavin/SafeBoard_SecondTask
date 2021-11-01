using SafeBoard_SecondTask.DirectoryScanner.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanAPI.Packets
{
    public class ExecutionPacket : Packet<ExecutionPacket>
    {
        public string Directory { get; set; }
        public string Guid { get; set; }
    }
}
