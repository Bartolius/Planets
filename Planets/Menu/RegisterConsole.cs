using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Planets.Menu
{
    static class RegisterConsole
    {
        public static bool active;
        private static int _curPos;
        public static bool responded = false;

        public static void Appear()
        {
            active = true;
            Console.Clear();

            Console.Write("Login: ");
            string username = CursorHandler(0);
            Console.WriteLine();

            Console.Write("Email: ");
            string email = CursorHandler(1);
            Console.WriteLine();

            Console.Write("Password: ");
            string password = CursorHandler(2);
            Console.WriteLine();

            Console.Write("Repeat Password: ");
            string rPassword = CursorHandler(3);
            Console.WriteLine();



            LoginObj login = new LoginObj();
            login.Login = username;
            login.Email = email;
            login.Password = password;

            Response res = new Response();
            res.typ = typeof(LoginObj).ToString();
            res.responseBool = false;
            res.responseObj = JsonSerializer.Serialize(login, typeof(LoginObj));

            string resString = JsonSerializer.Serialize(res);

            Program.client.SendAsync(Encoding.UTF8.GetBytes(resString));
        }

        public static void Check()
        {
            if (!Program.client.connected)
            {
                Appear();
            }
            else
            {
                active = false;
                MenuConsole.Init(MenuComponent.MENULOGIN);
                MenuConsole.Appear(MenuComponent.MENULOGIN);
            }
        }





        public static string CursorHandler(int pos)
        {
            StringBuilder stringB = new StringBuilder();
            _curPos = 0;

            while (true)
            {
                PrintText(stringB.ToString(), pos);
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (char.IsLetter(key.KeyChar) || char.IsNumber(key.KeyChar))
                {
                    stringB.Append(key.KeyChar);
                    _curPos++;
                }
                if (key.Key == ConsoleKey.Enter)
                {
                    return stringB.ToString();
                }
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (_curPos > 0)
                    {
                        stringB.Remove(stringB.Length - 1, 1);
                        _curPos--;
                    }
                }
            }
        }
        private static void PrintText(string text, int type)
        {
            switch (type)
            {
                case 0:
                case 1:
                    Console.SetCursorPosition(7, type);
                    Console.Write(text);
                    Console.Write(" ");
                    break;
                case 2:
                    Console.SetCursorPosition(10, 2);
                    for (int i = 0; i < text.Length; i++)
                    {
                        Console.Write("#");
                    }
                    Console.Write(" ");
                    break;
                case 3:
                    Console.SetCursorPosition(17, 3);
                    for (int i = 0; i < text.Length; i++)
                    {
                        Console.Write("#");
                    }
                    Console.Write(" ");
                    break;
            }
        }
    }
}
