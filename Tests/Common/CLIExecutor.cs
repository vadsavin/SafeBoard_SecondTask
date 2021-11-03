using System.Diagnostics;
using System.IO;
using System.Text;

namespace Tests.Common
{
    public class CLIExecutor
    {
        private static readonly string _pathToCli = Path.Combine(Directory.GetCurrentDirectory(), "SafeBoard_ScanCLI.exe");
        private const int _timeOut = 60000;

        public int Execute(string argsString, out string result)
        {
            Process cliProcess = new Process();
            cliProcess.StartInfo = new ProcessStartInfo()
            {
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = _pathToCli,
                Arguments = argsString

            };
            cliProcess.Start();

            StringBuilder sb = new StringBuilder();

            cliProcess.OutputDataReceived += (_, e) => sb.AppendLine(e.Data);
            cliProcess.BeginOutputReadLine();

            cliProcess.WaitForExit(_timeOut);

            result = sb.ToString();

            return cliProcess.ExitCode;
        }
    }
}
