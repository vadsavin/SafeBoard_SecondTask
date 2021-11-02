using SafeBoard_ScanAPI;
using SafeBoard_ScanAPI.Packets;
using SafeBoard_ScanAPI.Server;
using SafeBoard_ScanService.Utils;
using SafeBoard_SecondTask.DirectoryScanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanService
{
    public class ScannerServiceNetworker
    {
        private readonly ScanServer _server;
        private readonly ScannerService _service;

        public IPEndPoint ServerEndPoint => _server?.Endpoint;

        public ScannerServiceNetworker()
        {
            Func<ScanSession, MessageHandler> createSession = session => new ScanServiceMessageHandler(this, session);

            _server = new ScanServer("127.0.0.1", 2021, createSession);

            _service = new ScannerService();
        }

        public void Run()
        {
            _server.Start();
        }

        public void Stop()
        {
            _server.Stop();
        }

        public ScanReturnsPacket StartScanInDirectory(string directoryPath)
        {
            var packet = new ScanReturnsPacket();
            try
            {
                var scannerTask = _service.AddAndRunNewDefaultTaskAsync(directoryPath);
                packet.ScanTaskGuid = scannerTask.Guid;
                packet.Started = true;
            }
            catch(Exception e)
            {
                packet.Started = false;
                packet.Message = e.Message;
            }

            return packet;
        }

        public StatusPacket GetScanTaskStatus(Guid guid)
        {
            var status = _service.GetTaskStatus(guid);

            var packet = new StatusPacket();
            packet.Status = status;

            return packet;
        }
    }
}
