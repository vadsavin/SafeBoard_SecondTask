using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScanAPI.Contracts;
using SafeBoard_ScanService.DirectoryScanner;
using System;

namespace Tests.ScanTests
{
    public class ScannerTestsBase
    {
        public void LockedFileScanTest(Action<string, ScannerRule[], ReportDescription> baseTestMethod)
        {
            var rules = new ScannerRule[]
            {
                TestsEnviroment.JsRule,
                TestsEnviroment.RmRule,
                TestsEnviroment.Rundll32Rule
            };

            var description = new ReportDescription();

            description.Reports = new Dictionary<DetectionReportType, int>()
            {
                { DetectionReportType.Clean, 2 },
                { DetectionReportType.Skipped, 0 },
                { DetectionReportType.Malvare, 2 },
                { DetectionReportType.NoAccess, 0 },
                { DetectionReportType.Error, 2 },
                { DetectionReportType.FileNotExists, 0 },
            };
            description.Rules = new Dictionary<string, int>()
            {
                { TestsEnviroment.JsRule.RuleName, 0 },
                { TestsEnviroment.RmRule.RuleName, 1 },
                { TestsEnviroment.Rundll32Rule.RuleName, 1 }
            };

            using (var jsFile = File.OpenRead(TestsEnviroment.JsMalvareFilePath))
            using (var cleanFile = File.OpenRead(TestsEnviroment.CleanFilePath))
            {
                jsFile.Lock(0, 1);
                cleanFile.Lock(0, 1);

                baseTestMethod(TestsEnviroment.AffectedDirectory, rules, description);
            }
        }

        public void DefaultScanTest(Action<string, ScannerRule[], ReportDescription> baseTestMethod)
        {
            var rules = new ScannerRule[]
            {
                TestsEnviroment.JsRule,
                TestsEnviroment.RmRule,
                TestsEnviroment.Rundll32Rule
            };

            var description = new ReportDescription();

            description.Reports = new Dictionary<DetectionReportType, int>()
            {
                { DetectionReportType.Clean, 3 },
                { DetectionReportType.Skipped, 0 },
                { DetectionReportType.Malvare, 3 },
                { DetectionReportType.NoAccess, 0 },
                { DetectionReportType.Error, 0 },
                { DetectionReportType.FileNotExists, 0 },
            };
            description.Rules = new Dictionary<string, int>()
            {
                { TestsEnviroment.JsRule.RuleName, 1 },
                { TestsEnviroment.RmRule.RuleName, 1 },
                { TestsEnviroment.Rundll32Rule.RuleName, 1 }
            };

            baseTestMethod(TestsEnviroment.AffectedDirectory, rules, description);
        }

    }
}
