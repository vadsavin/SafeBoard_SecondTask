using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanCLI
{
    public class ArgumentParser
    {
        public static Dictionary<string, string> Parse(string[] args)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (args.Length < 1) return null;

            string name = args[0];
            result["name"] = name;

            args = args.Skip(1).ToArray();

            string key = null;
            foreach (string arg in args)
            {
                if(arg.StartsWith("--") && key == null)
                {
                    key = arg;
                }
                else if(!arg.StartsWith("--") && key != null)
                {
                    result[key[2..]] = arg;
                    key = null;
                }
            }

            return result;
        }
    }
}
