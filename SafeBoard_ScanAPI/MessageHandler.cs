using SafeBoard_ScanAPI.Packets;
using SafeBoard_ScanAPI.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanAPI
{
    public abstract class MessageHandler
    {
        public ScanSession Session { get; }

        public MessageHandler()
        {

        }

        public MessageHandler(ScanSession session)
        {
            Session = session;
        }

        public bool Handle(string message)
        {
            try
            {
                ExecutionPacket packet = ExecutionPacket.Deserialize(message);
                HandleExecutionPacket(packet);
                return true;
            }
            catch
            {

            }

            try
            {
                StatusPacket packet = StatusPacket.Deserialize(message);
                HandleStatusPacket(packet);
                return true;
            }
            catch
            {

            }

            return false;
        }

        public abstract void HandleExecutionPacket(ExecutionPacket packet);

        public abstract void HandleStatusPacket(StatusPacket packet);

        public void SendPacket<T>(Packet<T> packet) where T : Packet<T>
        {
            Session.Send(packet.Serialize());
        }
    }
}
