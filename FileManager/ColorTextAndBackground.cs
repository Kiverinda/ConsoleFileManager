using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    public static class ColorTextAndBackground
    {

        public static void Base()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }
        public static void InverseBase()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        }
        public static void ForFile()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }
        public static void InverseForFile()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.Cyan;
        }

        public static void ForMessageAlarm()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }

        public static void Select()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }
        public static void InverseSelect()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.White;
        }
    }
}
