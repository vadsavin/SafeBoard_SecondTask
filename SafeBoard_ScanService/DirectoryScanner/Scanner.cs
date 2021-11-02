using SafeBoard_SecondTask.DirectoryScanner.Contacts;
using System.Threading.Tasks;

namespace SafeBoard_SecondTask.DirectoryScanner
{
    public class Scanner
    {
        public ScannerRule[] Rules { get; }

        public ReportInfo ReportInfo => _reportInfo ?? new ReportInfo();

        public int MaxParallelScanningFiles { get; set; } = 10;

        private ReportInfo _reportInfo;

        public Scanner(ScannerRule[] rules)
        {
            Rules = rules;
        }

        public void Scan(string path)
        {
            ScanAsync(path).Wait();
        }

        /// <summary>
        /// Запуск работы сканера. Parallel используется для увелечения скорости работы. 
        /// </summary>
        public Task ScanAsync(string path)
        {
            try
            {
                _reportInfo = new ReportInfo(Rules);
                _reportInfo.ScanningDirectory = path;
                _reportInfo.StartScanning();

                return Task.Run(() => RunScanJob(path));
            }
            catch
            {
                _reportInfo.EndScanning();
                throw;
            }
        }

        private void RunScanJob(string path)
        {
            var parallelOptions = new ParallelOptions() 
            { 
                MaxDegreeOfParallelism = MaxParallelScanningFiles 
            };

            var filesEnumerator = FilesManager.GetAllFilesFromDirectory(path);

            Detector detector = new Detector(Rules);

            Parallel.ForEach(filesEnumerator, parallelOptions, filePath => ScanSingleFile(detector, filePath));

            _reportInfo.EndScanning();
        }

        private void ScanSingleFile(Detector detector, string filePath)
        {
            var report = detector.CheckFile(filePath);

            _reportInfo.AddReport(report);
        }
    }
}
