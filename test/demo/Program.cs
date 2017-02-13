using System;
using Engineering.Measurements;

namespace TestConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Units demo code");
            // todo: provide demo code
            var t = 5.0.Meters();
            Console.WriteLine($"Extension test: {t}");
        }
    }
}
