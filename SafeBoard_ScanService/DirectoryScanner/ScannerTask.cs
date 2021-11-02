using ScanAPI.Contracts;
using System;
using System.Threading.Tasks;

namespace SafeBoard_ScanService.DirectoryScanner
{
    public class ScannerTask
    {    
        public Scanner Scanner { get; }

        public string DirectoryToScan { get; }

        public Task Task { get; private set; }

        public Guid Guid { get; }

        public ScannerTask(string directory, ScannerRule[] rules, int maxDegreeOfParallelism ,Guid id)
        {
            Scanner = new Scanner(rules);
            Scanner.MaxParallelScanningFiles = maxDegreeOfParallelism;
            DirectoryToScan = directory;
            Guid = id;
        }

        public void Run()
        {
            Scanner.Scan(DirectoryToScan);
        }

        public Task RunAsync()
        {
            return Task = Scanner.ScanAsync(DirectoryToScan);
        }
    }
}
