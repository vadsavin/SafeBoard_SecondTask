using SafeBoard_ScanAPI;
using SafeBoard_ScanAPI.Packets;
using SafeBoard_ScanAPI.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanCLI
{
    class ScanCLIMessageHandler : MessageHandler
    {
        public CLI CLI { get; }

        public ScanCLIMessageHandler(CLI cli, ScanSession session)
            : base(session)
        {
            CLI = cli;
        }

        public override void HandleExecutionPacket(ExecutionPacket packet)
        {
            CLI.Handle(packet);
        }

        public override void HandleStatusPacket(StatusPacket packet)
        {
            CLI.Handle(packet);
        }
    }
}
