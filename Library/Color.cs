namespace Library
{
    public class Color
    {
        private byte R { get; set; }
        private byte G { get; set; }
        private byte B { get; set; }

        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public static string ForegroundColor(Color color)
        {
            return $"\x1b[38;2;{color.R};{color.G};{color.B}m";
        }
        public static string BackgroundColor(Color color)
        {
            return $"\x1b[48;2;{color.R};{color.G};{color.B}m";
        }

        public const string CLEAR = "\x1b[0m";
    }
}