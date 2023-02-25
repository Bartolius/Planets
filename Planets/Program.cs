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

        private static void Init()
        {
            ConsoleMenu.SetAsciCMDConsole();

            WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Fill);
            //ConsoleMenu.DisableCloseButton();
            ConsoleMenu.BlockResize();
            //Console.SetBufferSize(width: Console.LargestWindowWidth - 3, height: Console.LargestWindowHeight);
            Console.CursorVisible = false;

            MenuConsole.Init(MenuComponent.MENUNOTLOGIN);
        }


        static void Main(string[] args)
        {
            Init();


            PerlinNoise noise = new PerlinNoise(123456789);
            StringBuilder stringBuilder = new StringBuilder();

            for (int z = 0; z < 1500; z++)
            {
                for (int y = 0; y < 16 * 3; y++)
                {
                    for (int x = 0; x < 16 * 6; x++)
                    {
                        double noisevalue = noise.OctaveNoise(x, y, z,8);
                        double color=(noisevalue+1)/2;

                        stringBuilder.Append(Color.BackgroundColor(new Color((byte)(color*255), (byte)(color * 255), (byte)(color * 255))) + " ");
                    }
                    stringBuilder.Append("\n");
                }
                Console.SetCursorPosition(0, 0);
                FastConsole.Write(stringBuilder.ToString());
                FastConsole.Flush();
                stringBuilder.Clear();
            }


            /*Init();

            Program.client = new Client("127.0.0.1", 1111);

            client.ConnectAsync();
            while(!client.IsConnected) { Thread.Sleep(1); }
            MenuConsole.Appear(MenuComponent.MENUNOTLOGIN);

            while (client.IsConnected)
            {
                Thread.Sleep(10000);
            }*/
        }
    }
}