using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets.Menu
{
    class Placeholder
    {
        public int x;
        public int y;
        public Func<bool> func;
        public Placeholder() { }
        public Placeholder(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Placeholder(int x, int y, Func<bool> func) : this(x, y)
        {
            this.func = func;
        }
    }
}
