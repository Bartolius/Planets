using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets.Menu
{
    class MenuComponent
    {
        public static List<MenuBlock> MENUNOTLOGIN { get; } = new List<MenuBlock>
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
                Settings.Appear();
                return true;
            })),
            new MenuBlock("Exit",60,5,(Console.WindowWidth-60)/2,2,new Color(200,25,25),new Color(25,200,25),new Color(25,25,200),new Placeholder(65,3,()=>
            {
                Environment.Exit(0);
                return true;
            }))
        };
        public static List<MenuBlock> MENULOGIN { get; } = new List<MenuBlock>()
        {
            new MenuBlock("Game",60,5,(Console.WindowWidth-60)/2,0),
            new MenuBlock("Host Game ",60,5,(Console.WindowWidth-60)/2,2,new Color(200,25,25),new Color(25,200,25),new Color(25,25,200),new Placeholder(65,3,()=>
            {
                return true;
            })),
            new MenuBlock("Join Game ",60,5,(Console.WindowWidth-60)/2,2,new Color(200,25,25),new Color(25,200,25),new Color(25,25,200),new Placeholder(65,3,()=>
            {
                return true;
            })),
            new MenuBlock("Settings",60,5,(Console.WindowWidth-60)/2,2,new Color(200,25,25),new Color(25,200,25),new Color(25,25,200),new Placeholder(65,3,()=>
            {
                Settings.Appear();
                return true;
            })),
            new MenuBlock("Exit",60,5,(Console.WindowWidth-60)/2,2,new Color(200,25,25),new Color(25,200,25),new Color(25,25,200),new Placeholder(65,3,()=>
            {
                Environment.Exit(0);
                return true;
            })),
        };
    }
}
