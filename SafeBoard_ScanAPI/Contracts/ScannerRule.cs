using System.Text.RegularExpressions;

namespace ScanAPI.Contracts
{
    public class ScannerRule
    {
        public string RuleName { get; }
        public string FileNamePattern { get; }
        public string MalvareString { get; }

        public ScannerRule(string ruleName, string malvareString, string fileNamePattern = null)
        {
            RuleName = ruleName;
            MalvareString = malvareString;
            FileNamePattern = fileNamePattern;
        }

        /// <summary>
        /// Проверяет соответствие названия файла вышеуказанному паттерну.
        /// </summary>
        public bool CheckFileName(string fileName) 
        {
            return string.IsNullOrEmpty(FileNamePattern)
                || Regex.IsMatch(fileName, FileNamePattern);
        }
    }
}
