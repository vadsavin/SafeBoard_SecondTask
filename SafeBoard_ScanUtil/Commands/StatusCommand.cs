using System;
using System.Collections.Generic;
using System.Text;

namespace SafeBoard_ScanCLI.Commands
{
    public class StatusCommand : BaseCommand, ICommand
    {
        public Guid Guid { get; }

        public StatusCommand(Guid guid) : base()
        {
            Guid = guid;
        }

        public void Execute()
        {
            var result = _facade.GetStatus(Guid);
            var status = result.Status;

            _facade.SendOutput(nameof(status.IsRunning), status.IsRunning.ToString());
            _facade.SendOutput(nameof(status.ScanningTime), status.ScanningTime.ToString());
            _facade.SendOutput(nameof(status.DirectoryPath), status.DirectoryPath);
            _facade.SendOutput(nameof(status.BytesRead), status.BytesRead.ToString());
            _facade.SendOutput(nameof(status.FilesProcessed), status.FilesProcessed.ToString());
            _facade.SendOutput(nameof(status.ReportsByType), GetStringFromDictionary(status.ReportsByType));
            _facade.SendOutput(nameof(status.ReportsByRule), GetStringFromDictionary(status.ReportsByRule));
        }

        private string GetStringFromDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null) return string.Empty;

            var sb = new StringBuilder("[\n");
            foreach (var pair in dictionary)
            {
                sb.AppendLine($"   {pair.Key}: {pair.Value}");
            }
            sb.AppendLine("]");

            return sb.ToString();
        }
    }
}
