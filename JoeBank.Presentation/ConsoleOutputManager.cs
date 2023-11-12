using System;

namespace JoeBank.Presentation
{
    public class ConsoleOutputManager
    {
         //private readonly string format = "{0," + Console.WindowWidth / 2 + "}";
         private readonly string format = "{0,-32} {1}";

        public void Write(string message)
        {
            Console.Write(format, " ", message);
        }
        public void WriteLine(string message)
        {
            Console.WriteLine(format, " ", message);
        }
        //public void ReadLine()
        //{
        //    Console.SetCursorPosition(10,10);
        //    Console.ReadLine();
        //}
    }
} 