using System;
using System.Net;
using System.Net.Sockets;
using NetCoreServer;
using NDesk.Options;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool help = false;
            int port = 1111;

            var options = new OptionSet()
            {
                {"h|?|help", v => help = v != null },
                {"p|port=", v => port = int.Parse(v) }
            };

            try
            {
                options.Parse(args);
            }
            catch(OptionException e)
            {
                Console.Write("Command line error: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `--help` to get usage information.");
                return;
            }

            if(help)
            {
                Console.Write("Usage: ");
                options.WriteOptionDescriptions(Console.Out);
                return;
            }

            Console.WriteLine($"Server port: {port}");

            Console.WriteLine();

            Server server = new Server(IPAddress.Any, port);

            Console.Write("Server starting... ");
            server.Start();

            Console.WriteLine("Done!");

            Console.WriteLine("Press Enter to stop the server or '!' to restart the server");

            for(; ; )
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                if(line == "!")
                {
                    Console.Write("Server restarting... ");
                    server.Restart();
                    Console.WriteLine("Done!");
                }
            }

            Console.Write("Server stopping... ");
            server.Stop();
            Console.WriteLine("Done!");
        }
    }
}