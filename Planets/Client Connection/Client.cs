using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDesk.Options;
using System.Net.Sockets;
using System.Text.Json;
using Planets.Menu;
using Planets.Objects;
using TcpClient = NetCoreServer.TcpClient;

namespace Planets.Client_Connection
{
    class Client : TcpClient
    {
        public Response response;


        public Client(string address, int port) : base(address, port) { }
        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            string res = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            response = JsonSerializer.Deserialize<Response>(res);

            respond();
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Client caught an error with code {error}");
        }

        private void respond()
        {
            if (LoginConsole.active == true)
            {
                LoginConsole.Check(response);
            }
        }


    }
}
