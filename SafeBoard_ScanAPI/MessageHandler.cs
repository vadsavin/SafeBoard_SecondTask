using SafeBoard_ScanAPI.Packets;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SafeBoard_ScanAPI
{
    public class MessageHandler
    {
        public ISession Session { get; }

        public MessageHandler(ISession session)
        {
            Session = session;
        }

        public event EventHandler<PacketEventArgs> OnRecievedPacket;

        public bool Handle(string message)
        {
            if (!TryDeserializePacket(message, out IPacket packet)) return false;

            OnRecievedPacket?.Invoke(this, new PacketEventArgs(packet));

            return false;
        }

        private readonly Dictionary<string, Func<string, IPacket>> _packetFactory = new Dictionary<string, Func<string, IPacket>>()
        {
            { nameof(StatusRequestPacket),  packetString => StatusRequestPacket.Deserialize(packetString) },
            { nameof(StatusPacket),         packetString => StatusPacket.Deserialize(packetString) },
            { nameof(StartScanPacket),      packetString => StartScanPacket.Deserialize(packetString) },
            { nameof(ScanReturnsPacket),    packetString => ScanReturnsPacket.Deserialize(packetString) },
        };

        private bool TryDeserializePacket(string packetString, out IPacket packet)
        {
            packet = null;

            var name = Packet.Deserialize(packetString).Name;
            if (!_packetFactory.TryGetValue(name, out var factory)) return false;

            packet = factory.Invoke(packetString);
            return true;
        }

        public async Task<T> SendAndWaitAsync<T>(IPacket packet, double timeout = 60) where T : Packet<T>
        {
            T value = null;

            CancellationTokenSource source = new CancellationTokenSource();

            EventHandler<PacketEventArgs> getMessageEvent = (_, e) =>
            {
                if (typeof(T) == e.Packet.GetType())
                {
                    value = (T)e.Packet;
                    source.Cancel();
                }
            };

            OnRecievedPacket += getMessageEvent;

            SendPacket(packet);

            try
            {
                await Task.Delay((int)(timeout * 1000), source.Token);
            }
            catch { }
            finally
            {
                OnRecievedPacket -= getMessageEvent;
            }

            return value;
        }

        public void SendPacket(IPacket packet)
        {
            Session.Send(packet.Serialize());
        }
    }
}
