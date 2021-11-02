using ScanAPI.Contracts;
using System.Collections.Generic;

namespace Tests.ScanTests
{
    public class ReportDescription
    {
        public Dictionary<DetectionReportType, int> Reports { get; set; }
        public Dictionary<string, int> Rules { get; set; }
    }
}
