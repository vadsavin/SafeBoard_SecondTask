using Microsoft.VisualStudio.TestTools.UnitTesting;
using SafeBoard_ScanAPI.Contracts;
using ScanAPI.Contracts;
using System.Threading;
using Tests.ScanTests;

namespace Tests.Common
{
    [TestClass]
    public class CommonScanFromCLITests : ScannerTestsBase
    {
        private readonly CLICommands _commands = new CLICommands(new CLIExecutor());

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
            using (new ServiceRunner().Run())
            {
                var returns = _commands.RunScan(directory, rules);

                Assert.IsTrue(returns.Started, returns.Message);
                Assert.IsNotNull(returns.ScanTaskGuid);

                ScanStatus output = null;
                do
                {
                    output = _commands.GetStatus(returns.ScanTaskGuid);
                    Thread.Sleep(1000);
                } 
                while (output.IsRunning);

                foreach (var description in expected.Reports)
                {
                    Assert.AreEqual(description.Value, output.ReportsByType[description.Key],
                        $"Unexpected {description.Key} reports count");
                }

                foreach (var rule in expected.Rules)
                {
                    Assert.AreEqual(rule.Value, output.ReportsByRule[rule.Key],
                        $"Unexpected {rule.Key} rull reports count");
                }
            } 
        }
    }
}
