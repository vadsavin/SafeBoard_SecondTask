namespace ScanAPI.Contracts
{
    /// <summary>
    /// Хранение и доступ к результату сканирования.
    /// </summary>
    public class DetectionReport
    {
        public string File { get; }

        public DetectionReportType ReportType { get; }

        public string Message { get; }

        public ScannerRule Rule { get; }

        public long BytesRead { get; }


        public DetectionReport(string file, DetectionReportType type)
        {
            File = file;
            ReportType = type;
        }

        public DetectionReport(string file, DetectionReportType type, long bytesRead) 
            : this(file, type)
        {
            BytesRead = bytesRead;
        }

        public DetectionReport(string file, DetectionReportType type, ScannerRule rule, long bytesRead)
            : this(file, type, bytesRead)
        {
            Rule = rule;
        }

        public DetectionReport(string file, DetectionReportType type, string message)
            : this(file, type)
        {
            Message = message;
        }
    }
}
