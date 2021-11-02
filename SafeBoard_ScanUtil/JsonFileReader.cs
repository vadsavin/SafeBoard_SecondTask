using System;
using System.IO;
using System.Text.Json;

namespace SafeBoard_ScanCLI
{
    public class JsonFileReader
    {
        public static T Read<T>(string filePath)
        {
            if (filePath == null) return default;

            using (FileStream stream = File.OpenRead(filePath))
            using (var streamReader = new StreamReader(stream))
            {
                try
                {
                    return JsonSerializer.Deserialize<T>(streamReader.ReadToEnd());
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    return default;
                } 
            }
        }
    }
}
