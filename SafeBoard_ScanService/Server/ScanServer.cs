using NetCoreServer;
using System;

namespace SafeBoard_ScanAPI.Server
{
    public class ScanServer : TcpServer
    {
        public ScanServer(string address, int port, Func<ScanSession, MessageHandler> _createSession)
            : base(address, port)
        {
            createSession = _createSession;
        }

        private Func<ScanSession, MessageHandler> createSession;
        protected override TcpSession CreateSession()
        {
            return new ScanSession(this, session => createSession.Invoke(session));
        }
    }
}
