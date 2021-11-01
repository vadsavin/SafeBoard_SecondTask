using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanService.Utils
{
    public class DefaultMessages
    {
        public static string NoSuchScannerTaskId    => "There's no such task id.";
        public static string ScannerTaskInProgress  => "Current task is still running.";
        public static string InvalidGuidFormat      => "Invalid Guid format.";
    }
}
