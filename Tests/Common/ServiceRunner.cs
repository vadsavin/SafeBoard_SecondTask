using System;
using System.Diagnostics;
using System.IO;

namespace Tests.Common
{
    public class ServiceRunner : IDisposable
    {
        private static readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "SafeBoard_ScanService.exe");

        private Process _process;

        public ServiceRunner Run()
        {
            _process = Process.Start(_filePath);
            return this;
        }

        public void Dispose()
        {
            _process?.Kill();
        }
    }
}
