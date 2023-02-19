using Library;

namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string asciiColor = Color.ForegroundColor(new Color(12, 34, 211));

            Assert.Equal("\x1b[38;2;12;34;211m", asciiColor);
        }
    }
}