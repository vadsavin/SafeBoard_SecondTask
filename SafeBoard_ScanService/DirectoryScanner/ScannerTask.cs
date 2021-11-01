using SafeBoard_SecondTask.DirectoryScanner.Contacts;
using SafeBoard_SecondTask.DirectoryScanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeBoard_SecondTask
{
    public class ScannerTask
    {    
        public Scanner Scanner { get; }

        public string DirectoryToScan { get; }

        public Task Task { get; private set; }

        public Guid Guid { get; }

        public ScannerTask(string directory, ScannerRule[] rules, Guid id)
        {
            Scanner = new Scanner(rules);
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
