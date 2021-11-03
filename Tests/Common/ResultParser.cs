using System.Collections.Generic;
using System.Linq;

namespace Tests.Common
{
    public class ResultParser
    {
        public static Dictionary<string, object> Parse(string input)
        {
            var result = new Dictionary<string, object>();

            string[] inputSplitted = input.Split('\n');

            string readingArray = null;

            foreach (string line in inputSplitted)
            {
                if (!string.IsNullOrEmpty(readingArray))
                {
                    if(line.StartsWith("]"))
                    {
                        readingArray = null;
                        continue;
                    }

                    string[] separatedLine = line.Split(": ");
                    var arrayKey = separatedLine[0].Trim();
                    var arrayValue = separatedLine[1];

                    var dictionary = result[readingArray] as Dictionary<string, string>;

                    dictionary.Add(arrayKey, arrayValue);

                    continue;
                }

                if (!line.StartsWith('#'))
                {
                    continue;
                }

                var meaningLine = new string(line.Skip(1).ToArray());

                string[] separatedMeaningLine = meaningLine.Split(": ");
                var key = separatedMeaningLine[0];
                var value = separatedMeaningLine[1];

                if (separatedMeaningLine[1].StartsWith("["))
                {
                    readingArray = key;
                    result.Add(readingArray, new Dictionary<string, string>());
                    continue;
                }

                result.Add(key, value);
            }

            return result;
        }
    }
}
