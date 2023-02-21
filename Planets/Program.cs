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

using System;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Planets
{
    
    internal class Program
    {
        public static Client client;
        private static List<MenuBlock> list = new List<MenuBlock>
        {
            new MenuBlock("Game",60,5,(Console.WindowWidth-60)/2,0),
            new MenuBlock("Log in",60,5,(Console.WindowWidth-60)/2,2,new Color(200,25,25),new Color(25,200,25),new Color(25,25,200),new Placeholder(65,3,()=>
            {
                LoginConsole.Appear();
                return true;
            })),
            new MenuBlock("Sign in ",60,5,(Console.WindowWidth-60)/2,2,new Color(200,25,25),new Color(25,200,25),new Color(25,25,200),new Placeholder(65,3,()=>
            {
                RegisterConsole.Appear();
                return true;
            })),
            new MenuBlock("Settings",60,5,(Console.WindowWidth-60)/2,2,new Color(200,25,25),new Color(25,200,25),new Color(25,25,200),new Placeholder(65,3,()=>
            {
                return true;
            })),
            new MenuBlock("Exit",60,5,(Console.WindowWidth-60)/2,2,new Color(200,25,25),new Color(25,200,25),new Color(25,25,200),new Placeholder(65,3,()=>
            {
                System.Environment.Exit(1);
                return true;
            }))
        };

        private static void Init()
        {
            var stdout = Console.OpenStandardOutput();
            var con = new StreamWriter(stdout, Encoding.ASCII);
            con.AutoFlush = true;
            Console.SetOut(con);

            ConsoleMenu.SetAsciCMDConsole();

            WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Fill);
            ConsoleMenu.DisableCloseButton();
            ConsoleMenu.DisableMinMaxButtons();
            Console.SetBufferSize(width: Console.LargestWindowWidth - 3, height: Console.LargestWindowHeight);
            Console.CursorVisible = false;

            MenuConsole.Init(list);
        }
        static void Main(string[] args)
        {
            Init();

            Program.client = new Client("127.0.0.1", 1111);

            client.ConnectAsync();
            while(!client.IsConnected) { Thread.Sleep(1); }

            //Console.WriteLine("\x1b[36mTEST\x1b[0m");
            MenuConsole.Appear(list);

            while (client.IsConnected)
            {
                Thread.Sleep(10000);
            }
        }
    }
}