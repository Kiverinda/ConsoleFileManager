using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    /// <summary>
    /// Класс, удаляющий символ до курсора
    /// </summary>
    public class CL_Backspace : ICommand<ConsoleKeyInfo>
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
            if (CommandLine.GetInstance().CursorPositionInLine > 0)
            {
                CommandLine.GetInstance().CursorPositionInLine --;
                CommandLine.GetInstance().Line = CommandLine.GetInstance().Line.Remove(CommandLine.GetInstance().CursorPositionInLine, 1);
            }
            return false;
        }
    }
}
