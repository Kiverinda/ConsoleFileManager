using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    /// <summary>
    /// Цветовые схемы
    /// </summary>
    public static class ColorTextAndBackground
    {

        /// <summary>
        /// Основная цветовая схема
        /// </summary>
        public static void Base()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }

        /// <summary>
        /// Инверсная цветовая схема
        /// </summary>
        public static void InverseBase()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Основная цветовая схема для файла
        /// </summary>
        public static void ForFile()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }

        /// <summary>
        /// Инверсная цветовая схема для файла
        /// </summary>
        public static void InverseForFile()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.Cyan;
        }

        /// <summary>
        /// Основная цветовая схема для сообщений об ошибках
        /// </summary>
        public static void ForMessageAlarm()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }

        /// <summary>
        /// Основная цветовая схема для выбранных элементов
        /// </summary>
        public static void Select()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }

        /// <summary>
        /// Инверсная цветовая схема для выбранных элементов
        /// </summary>
        public static void InverseSelect()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.White;
        }
    }
}
