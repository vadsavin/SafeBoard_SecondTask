using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard_ScanAPI.Client
{
    public class ScanClient : TcpClient
    {
        public MessageHandler MessageHandler { get; }

        public ScanClient(string address, int port, MessageHandler handler)
            : base(address, port)
        {
            MessageHandler = handler;
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);

            MessageHandler.Handle(message);
        }
    }
}
