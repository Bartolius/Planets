using Library;
using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server
{
    class Session : TcpSession
    {
        public Session(TcpServer server) : base(server) { }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            Response res = JsonSerializer.Deserialize<Response>(Encoding.UTF8.GetString(buffer, (int)offset, (int)size));

            string response="";

            if (res.typ == typeof(LoginObj).ToString() && res.responseBool == true)
            {
                LoginObj login = JsonSerializer.Deserialize<LoginObj>(res.responseObj);

                LoginObj loginObj = login;

                bool selected = Program.connection.login(loginObj);

                Response resLog = new Response();
                resLog.typ = typeof(LoginObj).ToString();
                resLog.responseBool = selected;

                response = JsonSerializer.Serialize(resLog, typeof(Response));
            }
            if(res.typ == typeof(LoginObj).ToString() && res.responseBool == false)
            {
                LoginObj register = JsonSerializer.Deserialize<LoginObj>(res.responseObj);

                LoginObj registerObj = register;

                bool selected = Program.connection.register(registerObj);

                Response resLog = new Response();
                resLog.typ = typeof(LoginObj).ToString();
                resLog.responseBool = selected;

                response = JsonSerializer.Serialize(resLog, typeof(Response));
            }

            base.SendAsync(response);
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Session caught an error with code {error}");
        }
    }
}
