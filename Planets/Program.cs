using Library;
using NDesk.Options;
using NetCoreServer;
using Planets.Client_Connection;
using Planets.Menu;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Planets
{
    
    internal class Program
    {
        private static void Init()
        {
            WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Fill);
            //ConsoleMenu.DisableCloseButton();
            ConsoleMenu.DisableMinMaxButtons();
            Console.SetBufferSize(Console.LargestWindowWidth-3,Console.LargestWindowHeight);
            Console.CursorVisible = false;

            MenuConsole.Init();
        }
        static void Main(string[] args)
        {
            Init();

            MenuConsole.Appear();
        }
    }
}