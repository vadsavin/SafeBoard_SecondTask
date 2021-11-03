using System;

namespace SafeBoard_ScanAPI.Contracts
{
    public sealed class ScanReturns
    {
        public Guid ScanTaskGuid { get; set; }
        public string Message { get; set; }
        public bool Started { get; set; }
    }
}
