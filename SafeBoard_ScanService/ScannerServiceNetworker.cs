using SafeBoard_ScanAPI;
using SafeBoard_ScanAPI.Contracts;
using SafeBoard_ScanAPI.Packets;
using SafeBoard_ScanAPI.Server;
using SafeBoard_SecondTask.DirectoryScanner;
using ScanAPI.Contracts;
using System;
using System.Net;

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

        public ScanReturnsPacket StartScan(string directoryPath, ScannerRule[] rules = null, int? maxDegreeOfParallelism = null)
        {
            var packet = new ScanReturnsPacket() { Returns = new ScanReturns() };
            try
            {
                var scannerTask = _service.AddAndRunNewDefaultTaskAsync(directoryPath, rules, maxDegreeOfParallelism);
                packet.Returns.ScanTaskGuid = scannerTask.Guid;
                packet.Returns.Started = true;
            }
            catch(Exception e)
            {
                packet.Returns.Started = false;
                packet.Returns.Message = e.Message;
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
