using ScanAPI.Contracts;
using System;
using System.Collections.Generic;

namespace SafeBoard_ScanAPI.Contracts
{
    public class ScanStatus
    {
        public bool IsRunning { get; set; }

        public string DirectoryPath { get; set; }

        public Dictionary<DetectionReportType, int> ReportsByType { get; set; }

        public Dictionary<string, int> ReportsByRule { get; set; }

        public TimeSpan ScanningTime { get; set; }

        public long BytesRead { get; set; }

        public int FilesProcessed { get; set; }
    }
}
