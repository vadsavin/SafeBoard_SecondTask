using ScanAPI.Contracts;

namespace SafeBoard_ScanAPI.Packets
{
    public sealed class StartScanPacket : Packet<StartScanPacket>
    {
        public string DirectoryPath { get; set; }
        public ScannerRule[] Rules { get; set; }
        public int? MaxDegreeOfParallelism { get; set; }
    }
}
