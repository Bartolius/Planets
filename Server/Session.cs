using NetCoreServer;
using Server.Objects;
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
            LoginObj login = JsonSerializer.Deserialize<LoginObj>(res.responseObj);
            if(login.Login.Equals("Bartolius")&& login.Password.Equals("1234")){
                res.responseBool = true;
            }
            res.responseObj = null;

            string response = JsonSerializer.Serialize(res,typeof(Response));


            base.SendAsync(response);
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Session caught an error with code {error}");
        }
    }
}
