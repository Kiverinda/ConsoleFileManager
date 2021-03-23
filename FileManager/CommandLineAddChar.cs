using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    /// <summary>
    /// Класс обрабатывающий все нажатия клавиш, кроме указанных выше 
    /// в методе Management() класса CommandLine
    /// </summary>
    public class CommandLineAddChar : ICommand<ConsoleKeyInfo>
    {
        ConsoleKeyInfo Click { get; set; }

        /// <summary>
        /// Присвоение полю Click кода нажатой клавиши
        /// </summary>
        /// <param name="click"></param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            Click = click;
            
            return true;
        }

        /// <summary>
        /// Добавление символа нажатой клавиши в командной строке
        /// </summary>
        public bool Execute()
        {
            CommandLine commandLine = CommandLine.GetInstance();

            if (commandLine.CursorPositionInLine == commandLine.Line.Length)
            {
                commandLine.Line += Click.KeyChar;
                commandLine.CursorPositionInLine += 1;
            }
            else if (commandLine.CursorPositionInLine < commandLine.Line.Length)
            {
                commandLine.Line = commandLine.Line.Insert(commandLine.CursorPositionInLine,
                    Click.KeyChar.ToString());
                commandLine.CursorPositionInLine += 1;
            }

            return false;
        }
    }
}
