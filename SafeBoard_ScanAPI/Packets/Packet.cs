using SimpleSecureChatAPI.Utils;

namespace SafeBoard_ScanAPI.Packets
{
    public abstract class Packet<T> : JsonSerializable<T>, IPacket where T : Packet<T>
    {
        public string Name { get; set; }

        public Packet()
        {
            Name = GetType().Name;
        }
    }

    public sealed class Packet : Packet<Packet>
    {

    }
}
