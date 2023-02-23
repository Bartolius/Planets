using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets.Menu
{
    class MenuBlock
    {
        public string name { get; }
        public int width { get; }
        public int height { get; }
        public int x { get; }
        public int gap { get; }
        public Placeholder placeholder { get; }
        public Color text { get; }
        public Color background { get; }
        public Color border { get; }


        public MenuBlock(string name, int width, int height, int x, int gap)
        {
            this.name = name;
            this.width = width;
            this.height = height;
            this.x = x;
            this.gap = gap;
        }
        public MenuBlock(string name, int width, int height, int x, int gap, Placeholder placeholder) : this(name, width, height, x, gap)
        {
            this.placeholder = placeholder;
        }
        public MenuBlock(string name, int width, int height, int x, int gap, Color text, Color background, Color border, Placeholder placeholder) : this(name, width, height, x, gap, placeholder)
        {
            this.text = text;
            this.background = background;
            this.border = border;
        }
    }
}
