using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Session : TcpSession
    {
        public Session(TcpServer server) : base(server) { }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            SendAsync(buffer,offset, size);
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Session caught an error with code {error}");
        }
    }
}
