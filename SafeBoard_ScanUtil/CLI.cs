using SafeBoard_ScanAPI.Client;
using SafeBoard_ScanAPI.Packets;
using SafeBoard_ScanCLI.Commands;
using SafeBoard_ScanCLI.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanCLI
{
    public class CLI
    {
        public ScanClient Client { get; }

        public bool IsRunning { get; private set; }

        public CLI()
        {
            var handler = new ScanCLIMessageHandler(this, null);
            Client = new ScanClient("127.0.0.1", 2021, handler);
            Client.ConnectAsync();
            SendMessageToConsole($"Welcome to {Client.Endpoint}");
        }

        public void InitializeParser<T>() where T : ICommand, new()
        {
            CommonParser.AddParser(new Parser<T>());
        }

        public void ScanDirectory(string directory)
        {
            ExecutionPacket packet = new ExecutionPacket();
            packet.Directory = directory;
            Client.Send(packet.Serialize());
        }

        public void GetStatus(string guid)
        {
            StatusPacket packet = new StatusPacket();
            packet.Guid = guid;
            Client.Send(packet.Serialize());
        }

        public void SendMessageToConsole(string message)
        {
            Console.WriteLine(string.Join("\n",message.Split('\n').Select(line => "# "+line)));
        }

        public void Run()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Handle(ExecutionPacket packet)
        {
            SendMessageToConsole($"New task started with guid: {packet.Guid}");
        }

        public void Handle(StatusPacket packet)
        {
            SendMessageToConsole(packet.Status);
        }
    }
}
