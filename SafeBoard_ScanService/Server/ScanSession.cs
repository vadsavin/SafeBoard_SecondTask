using NetCoreServer;
using System;
using System.Text;

namespace SafeBoard_ScanAPI.Server
{
    public class ScanSession : TcpSession, ISession
    {
        MessageHandler MessageHandler { get; }

        public ScanSession(TcpServer server, Func<ScanSession, MessageHandler> createSession)
            : base(server)
        {
            MessageHandler = createSession.Invoke(this);
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);

            MessageHandler.Handle(message);
        }
    }
}
