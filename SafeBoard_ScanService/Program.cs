using System;
using System.Threading.Tasks;

namespace SafeBoard_ScanService
{
    class Program
    {
        static void Main(string[] args)
        {
            var networker = new ScannerServiceNetworker();

            networker.Run();
            Console.WriteLine($"Server started at {networker.ServerEndPoint}");

            Console.ReadKey();
            networker.Stop();
        }
    }
}
