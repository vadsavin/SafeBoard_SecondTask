using Newtonsoft.Json;
using SafeBoard_ScanAPI.Contracts;
using SafeBoard_ScanCLI;
using SafeBoard_ScanCLI.Commands;
using SafeBoard_ScanCLI.Parsers;
using ScanAPI.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Tests.Common
{
    public class CLICommands
    {
        private readonly CLIExecutor _cliExecutor;

        public CLICommands(CLIExecutor cliExecutor)
        {
            _cliExecutor = cliExecutor;
        }

        public ScanReturns RunScan(string directory, ScannerRule[] rules = null, int? maxDegreeOfParallelism = null)
        {
            string scanCommand = ScanCommandBuilder(directory, rules, maxDegreeOfParallelism);

            int exitCode = _cliExecutor.Execute(scanCommand, out var result);

            if (exitCode != 0)
            {
                throw new Exception($"CLI error with code: {exitCode}");
            }

            var resultDictionary = ResultParser.Parse(result);

            var scanReturns = new ScanReturns();

            scanReturns.Started = bool.Parse((string)resultDictionary[nameof(scanReturns.Started)]);

            if (scanReturns.Started)
            {
                scanReturns.ScanTaskGuid = Guid.Parse((string)resultDictionary[nameof(scanReturns.ScanTaskGuid)]);
            }
            else
            {
                scanReturns.Message = (string)resultDictionary[nameof(scanReturns.Message)];
            }

            return scanReturns;
        }

        public ScanStatus GetStatus(Guid guid)
        {
            string statusCommand = StatusCommandBuilder(guid);

            int exitCode = _cliExecutor.Execute(statusCommand, out var result);

            if (exitCode != 0)
            {
                throw new Exception($"CLI error with code: {exitCode}");
            }

            var resultDictionary = ResultParser.Parse(result);

            var scanStatus = new ScanStatus();

            scanStatus.FilesProcessed = int.Parse((string)resultDictionary[nameof(scanStatus.FilesProcessed)]);
            scanStatus.DirectoryPath = (string)resultDictionary[nameof(scanStatus.DirectoryPath)];
            scanStatus.BytesRead = int.Parse((string)resultDictionary[nameof(scanStatus.BytesRead)]);
            scanStatus.IsRunning = bool.Parse((string)resultDictionary[nameof(scanStatus.IsRunning)]);
            scanStatus.ScanningTime = TimeSpan.Parse((string)resultDictionary[nameof(scanStatus.ScanningTime)]);

            var byRuleDictionary = ((Dictionary<string, string>)resultDictionary[nameof(scanStatus.ReportsByRule)])
                .Select(pair => new KeyValuePair<string, int>(pair.Key, int.Parse(pair.Value)))
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            scanStatus.ReportsByRule = byRuleDictionary;

            var byTypeDictionary = ((Dictionary<string, string>)resultDictionary[nameof(scanStatus.ReportsByType)])
                .Select(pair => new KeyValuePair<DetectionReportType, int>(Enum.Parse<DetectionReportType>(pair.Key), int.Parse(pair.Value)))
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            scanStatus.ReportsByType = byTypeDictionary;

            return scanStatus;
        }

        private string StatusCommandBuilder(Guid guid)
        {
            return $"status --guid {guid}";
        }

        private string ScanCommandBuilder(string directory, ScannerRule[] rules = null, int? maxDegreeOfParallelism = null)
        {
            StringBuilder sb = new StringBuilder("scan");
            sb.Append($" --d \"{directory}\"");

            if (rules != null)
            {
                string path = SaveRules(rules);
                sb.Append($" --r \"{path}\"");
            }

            if (maxDegreeOfParallelism != null)
            {
                sb.Append($" --n {maxDegreeOfParallelism}");
            }

            return sb.ToString();
        }

        private string SaveRules(ScannerRule[] rules)
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            var ruleString = JsonConvert.SerializeObject(rules);

            using (var file = File.CreateText(path))
            {
                file.Write(ruleString);
            }

            return path;
        }
    }
}
