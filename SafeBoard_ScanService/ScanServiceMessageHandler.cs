using SafeBoard_ScanAPI;
using SafeBoard_ScanAPI.Packets;
using SafeBoard_ScanAPI.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanService
{
    public class ScanServiceMessageHandler : MessageHandler
    {
        public ScannerServiceNetworker Server { get; }

        public ScanServiceMessageHandler(ScannerServiceNetworker server, ScanSession session)   
            : base(session)
        {
            Server = server;
        }

        public override void HandleExecutionPacket(ExecutionPacket packet)
        {
            Server.Handle(packet, this);
        }

        public override void HandleStatusPacket(StatusPacket packet)
        {
            Server.Handle(packet, this);
        }
    }
}
