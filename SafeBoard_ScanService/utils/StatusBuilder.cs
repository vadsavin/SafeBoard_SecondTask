using SafeBoard_ScanService.DirectoryScanner;
using SafeBoard_ScanService.Utils;

namespace SafeBoard_SecondTask
{
    public class StatusBuilder
    {
        public static string GenereteStatus(ReportInfo info)
        {
            if (info.ScanInProgress)
            {
                return DefaultMessages.ScannerTaskInProgress;
            }
            else
            {
                return ReportBuilder.GenereteReport(info);
            }
        }
    }
}
