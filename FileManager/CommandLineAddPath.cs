using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    /// <summary>
    /// Класс, добавляющий текщий путь в командную строку
    /// </summary>
    public class CommandLineAddPath : ICommand<ConsoleKeyInfo>
    {

        /// <summary>
        /// Проверка нажатия горячей клавиши
        /// </summary>
        /// <param name="click">Код горячей клавиши</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.P && (click.Modifiers & ConsoleModifiers.Control) != 0;
        }

        /// <summary>
        /// Добавление текщего пути в командную строку
        /// </summary>
        public bool Execute()
        {
            CommandLine commandLine = CommandLine.GetInstance();
            commandLine.Line += commandLine.CurrentPanel.CurrentPath;
            commandLine.CursorPositionInLine = commandLine.Line.Length;
            return false;
        }
    }
}
