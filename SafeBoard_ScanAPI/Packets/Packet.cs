using SimpleSecureChatAPI.Utils;

namespace SafeBoard_ScanAPI.Packets
{
    public abstract class Packet<T> : JsonSerializable<T> where T : Packet<T>
    {

    }
}
