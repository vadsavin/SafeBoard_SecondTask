using Microsoft.VisualStudio.TestTools.UnitTesting;
using SafeBoard_ScanService.DirectoryScanner;
using ScanAPI.Contracts;
using System.Linq;
using Tests.ScanTests;

namespace Tests
{
    [TestClass]
    public class ScannerTests : ScannerTestsBase
    {
        [TestMethod]
        public void LockedFileScanTest()
        {
            LockedFileScanTest(BaseScanTest);
        }

        [TestMethod]
        public void DefaultScanTest()
        {
            DefaultScanTest(BaseScanTest);
        } 

        private void BaseScanTest(string directory, ScannerRule[] rules, ReportDescription expected)
        {
            var scanner = new Scanner(rules);
            scanner.Scan(directory);

            foreach (var description in expected.Reports)
            {
                Assert.AreEqual(description.Value, scanner.ReportInfo.GetReportsByType(description.Key).Count(),
                    $"Unexpected {description.Key} reports count");
            }

            var malwareReports = scanner.ReportInfo.GetReportsByType(DetectionReportType.Malvare);

            foreach (var rule in expected.Rules)
            {
                Assert.AreEqual(rule.Value, malwareReports.Count(report => report.Rule.RuleName == rule.Key),
                    $"Unexpected {rule.Key} rull reports count");
            }
        }
    }
}
