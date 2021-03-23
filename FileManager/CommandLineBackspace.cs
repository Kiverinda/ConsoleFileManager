using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    /// <summary>
    /// Класс, удаляющий символ до курсора
    /// </summary>
    public class CommandLineBackspace : ICommand<ConsoleKeyInfo>
    {
        
        /// <summary>
        /// Проверка нажатия горячей клавиши
        /// </summary>
        /// <param name="click">Код горячей клавиши</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.Backspace;
        }

        /// <summary>
        /// Удаление символа до курсора и сдвиг курсора
        /// </summary>
        public bool Execute()
        {
            CommandLine commandLine = CommandLine.GetInstance();
            if (commandLine.CursorPositionInLine > 0)
            {
                commandLine.CursorPositionInLine --;
                commandLine.Line = commandLine.Line.Remove(commandLine.CursorPositionInLine, 1);
            }
            return false;
        }
    }
}
