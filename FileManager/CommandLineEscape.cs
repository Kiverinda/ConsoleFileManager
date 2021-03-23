using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    /// <summary>
    /// Выход из режима командной строки
    /// </summary>
    public class CommandLineEscape : ICommand<ConsoleKeyInfo>
    {
        /// <summary>
        /// Проверка нажатия горячей клавиши
        /// </summary>
        /// <param name="click">Код горячей клавиши</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.Escape;
        }
        /// <summary>
        /// Выход из режима командной строки
        /// </summary>
        public bool Execute()
        {
            return true;
        }
    }
}
