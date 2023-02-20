using Library;
using NDesk.Options;
using NetCoreServer;
using Planets.Client_Connection;
using Planets.Menu;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Planets
{
    
    internal class Program
    {
        public static Client client;


        private static void Init()
        {
            WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Fill);
            //ConsoleMenu.DisableCloseButton();
            ConsoleMenu.DisableMinMaxButtons();
            Console.SetBufferSize(width: Console.LargestWindowWidth - 3, height: Console.LargestWindowHeight);
            Console.CursorVisible = false;

            MenuConsole.Init();
        }
        static void Main(string[] args)
        {
            //Init();

            //Program.client = new Client("127.0.0.1", 1111);


            Program.client = new Client("127.0.0.1", 1111);

            client.ConnectAsync();
            while(!client.IsConnected) { Thread.Sleep(1); }

            MenuConsole.Appear();

            while (client.IsConnected)
            {
                Thread.Sleep(10000);
            }
        }
    }
}