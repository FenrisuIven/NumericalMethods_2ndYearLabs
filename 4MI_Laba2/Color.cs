using System;

namespace _4MI_Laba2
{
    public class Color
    {
        private static ConsoleColor[] Colors =
        {
            ConsoleColor.White,     
            ConsoleColor.Gray,      
            ConsoleColor.DarkGray,  
            ConsoleColor.Red,       
            ConsoleColor.DarkGreen,
            ConsoleColor.DarkYellow
        };
        private static void Set(ConsoleColor color) => 
            Console.ForegroundColor = color;

        public static void WriteLine(int idx, string msg)
        {
            Set(Colors[idx]);
            Console.WriteLine(msg);
            Set(Colors[1]);
        }

        public static void Write(int idx, string msg)
        {
            Set(Colors[idx]);
            Console.Write(msg);
            Set(Colors[2]);
        }
    }
}