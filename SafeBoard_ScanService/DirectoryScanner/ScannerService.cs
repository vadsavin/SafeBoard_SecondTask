using SafeBoard_ScanService;
using SafeBoard_ScanService.Utils;
using SafeBoard_SecondTask.DirectoryScanner.Contacts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeBoard_SecondTask.DirectoryScanner
{
    public class ScannerService
    {
        //Дефолтные значения правил сканирования, созданные по заданию
        private static readonly ScannerRule[] _defaultRules = new ScannerRule[] 
        { 
            new ScannerRule("JS", @".*\.js\Z", "<script>evil_script()</script>"),
            new ScannerRule("rm -rf", @"rm -rf %userprofile%\Documents"),
            new ScannerRule("Rundll32", "Rundll32 sus.dll SusEntry")
        };

        ConcurrentDictionary<Guid, ScannerTask> Tasks;

        public ScannerService()
        {
            Tasks = new ConcurrentDictionary<Guid, ScannerTask>();
        }

        public ScannerService(string[] args) : this()
        {
            if (args.Length != 1)
            {
                throw new ArgumentException();
            }
            else
            {
                AddAndRunNewDefaultTaskAsync(args[0]);
            }
        }

        /// <summary>
        /// Создание задачи и запуск сканирования с дефолтными правилами.
        /// </summary>
        public ScannerTask AddAndRunNewDefaultTaskAsync(string directory)
        {
            return AddAndRunNewTaskAsync(directory, _defaultRules);
        }

        /// <summary>
        /// Создание задачи и запуск сканирования по определенным правилам.
        /// </summary>
        public ScannerTask AddAndRunNewTaskAsync(string directory, ScannerRule[] rules)
        {
            var scannerTask = AddNewTask(directory, rules);
            scannerTask.RunAsync();
            return scannerTask;
        }

        private ScannerTask AddNewTask(string directory, ScannerRule[] rules)
        {
            var scannerTask = new ScannerTask(directory, rules, Guid.NewGuid());
            Tasks.AddOrUpdate(scannerTask.Guid, _ => scannerTask, (_, _) => scannerTask);

            return scannerTask;
        }

        public IEnumerable<ScannerTask> GetAllTasks()
        {
            return Tasks.Values;
        }

        public IEnumerable<ScannerTask> GetAllRunningTasks()
        {
            return GetAllTasks().Where(task => task.Scanner.ReportInfo.ScanInProgress);
        }

        public string GetTaskStatus(string id)
        {
            Guid guid;
            try
            {
                guid = new Guid(id);
            }
            catch
            {
                return DefaultMessages.InvalidGuidFormat;
            }

            if (Tasks.TryGetValue(guid, out var scanTask))
            {
                return StatusBuilder.GenereteStatus(scanTask.Scanner.ReportInfo);
            }
            else
            {
                return DefaultMessages.NoSuchScannerTaskId;
            }
        }
    }
}
