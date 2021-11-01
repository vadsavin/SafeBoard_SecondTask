using SafeBoard_ScanService;
using SafeBoard_ScanService.Utils;
using SafeBoard_SecondTask.DirectoryScanner;
using SafeBoard_SecondTask.DirectoryScanner.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
