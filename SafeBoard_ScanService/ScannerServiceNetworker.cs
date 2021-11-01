using SafeBoard_ScanAPI;
using SafeBoard_ScanAPI.Packets;
using SafeBoard_ScanAPI.Server;
using SafeBoard_ScanService.Utils;
using SafeBoard_SecondTask.DirectoryScanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanService
{
    public class ScannerServiceNetworker
    {
        public ScanServer Server { get; }

        private ScannerService _service;

        public ScannerServiceNetworker()
        {
            Func<ScanSession, MessageHandler> createSession = session => new ScanServiceMessageHandler(this, session);

            Server = new ScanServer("127.0.0.1", 2021, createSession);

            _service = new ScannerService();
        }

        public void Run()
        {
            Server.Start();
        }

        public void Handle(ExecutionPacket packet, MessageHandler handler)
        {
            var scannerTask = _service.AddAndRunNewDefaultTaskAsync(packet.Directory);
            packet.Guid = scannerTask.Guid.ToString();
            handler.SendPacket<ExecutionPacket>(packet);
        }

        public void Handle(StatusPacket packet, MessageHandler handler)
        {
            packet.Status = _service.GetTaskStatus(packet.Guid);
            
            handler.SendPacket<StatusPacket>(packet);
        }
    }
}
