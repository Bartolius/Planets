using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDesk.Options;
using System.Net.Sockets;
using System.Text.Json;
using Planets.Menu;
using TcpClient = NetCoreServer.TcpClient;
using Library;

namespace Planets.Client_Connection
{
    class Client : TcpClient
    {
        private Response response;
        public bool connected;


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

        private bool check()
        {
            if (typeof(LoginObj).ToString().Equals(response.typ))
            {
                return response.responseBool;
            }
            else return connected;
        }
        private void respond()
        {
            connected = check();

            if (LoginConsole.active == true)
            {
                LoginConsole.Check();
            }
            if(RegisterConsole.active == true)
            {
                RegisterConsole.Check();
            }
        }


    }
}
