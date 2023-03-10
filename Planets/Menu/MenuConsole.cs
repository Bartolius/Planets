using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Planets.Menu
{
    class MenuConsole
    {
        static string? Menu = "";
        static bool active;
        static List<Placeholder> placeholders = new List<Placeholder>();
        static Placeholder prev;
        static int selected;

        public static void Appear(List<MenuBlock> list)
        {
            if (Menu == "") Init(list);
            active = true;
            Console.Clear();
            FastConsole.Write($"{Menu}");
            FastConsole.Flush();

            Control();
        }

        private static void AppearCursor()
        {
            if (prev != null)
            {
                Console.SetCursorPosition(prev.x, prev.y);
                Console.Write(" ");
            }
            
            Console.SetCursorPosition(placeholders[selected].x-1, placeholders[selected].y-1);
            Console.Write("<");
            prev = new Placeholder(placeholders[selected].x - 1, placeholders[selected].y-1);
        }

        private static void Control()
        {
            while(active)
            {
                AppearCursor();
                ConsoleKey key = Console.ReadKey(true).Key;
                if(key == ConsoleKey.DownArrow) 
                {
                    selected++;
                    if (selected == placeholders.Count)selected=0;
                }
                if(key==ConsoleKey.UpArrow)
                {
                    if (selected == 0) selected = placeholders.Count;
                    selected--;
                }
                if(key==ConsoleKey.Enter) 
                {
                    active = false;
                    placeholders[selected].func.Invoke();
                }
            }
        }

        public static void Init(List<MenuBlock> list)
        {
            StringBuilder sb = new StringBuilder();
            placeholders = new List<Placeholder>();
            selected = 0;

            int y= list[0].gap;
            for(int i=0;i<list.Count;i++)
            {
                sb.Append(Block(list[i]));

                if (list[i].placeholder != null) {
                    placeholders.Add(new Placeholder(list[i].x + list[i].placeholder.x, y + list[i].placeholder.y, list[i].placeholder.func));
                }
                if (i + 1 < list.Count) y += list[i].height + list[i + 1].gap;
            }

            Menu = sb.ToString();
        }

        private static string Block(MenuBlock block)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < block.gap; i++)
            {
                sb.AppendLine();
            }

            string spaces = FreeSpace(block.x);
            sb.Append(spaces);
            if (block.text != null) sb.Append(ChangeColor(block.background, block.border));
            for (int i=0; i < block.width; i++)
            {
                sb.Append("#");
            }
            sb.AppendLine(Color.CLEAR);

            for(int i=0;i<(block.height-3)/2; i++)
            {
                sb.Append(spaces);
                if (block.text != null) sb.Append(ChangeColor(block.background, block.border));
                sb.Append('#');
                for (int j = 0; i < block.width-2; i++)
                {
                    sb.Append(' ');
                }
                sb.Append('#');
                sb.AppendLine(Color.CLEAR);
            }

            sb.Append(spaces);
            if (block.text != null) sb.Append(ChangeColor(block.background, block.border));
            sb.Append('#');
            for(int i = 0; i < (block.width - block.name.Length) / 2-1; i++)
            {
                sb.Append(' ');
            }
            if (block.text != null) sb.Append(ChangeColor(block.background, block.text));
            sb.Append(block.name);
            if (block.text != null) sb.Append(ChangeColor(block.background, block.border));
            for (int i = 0; i < (block.width - block.name.Length) / 2-1; i++)
            {
                sb.Append(' ');
            }
            sb.Append('#');
            sb.AppendLine(Color.CLEAR);

            for (int i = 0; i < (block.height - 3) / 2; i++)
            {
                sb.Append(spaces);
                if (block.text != null) sb.Append(ChangeColor(block.background, block.border));
                sb.Append('#');
                for (int j = 0; i < block.width - 2; i++)
                {
                    sb.Append(' ');
                }
                sb.Append('#');
                sb.AppendLine(Color.CLEAR);
            }

            sb.Append(spaces);
            if (block.text != null) sb.Append(ChangeColor(block.background, block.border));
            for (int i = 0; i < block.width; i++)
            {
                sb.Append("#");
            }
            sb.AppendLine(Color.CLEAR);



            return sb.ToString();
        }

        private static string FreeSpace(int offset)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < offset; i++)
            {
                sb.Append(" ");
            }
            return sb.ToString();
        }
        private static string ChangeColor(Color background,Color foreground)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Color.ForegroundColor(foreground));
            sb.Append(Color.BackgroundColor(background));
            return sb.ToString();
        }
    }
}
