using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    /// <summary>
    /// Класс обрабатывающий все нажатия клавиш, кроме указанных выше 
    /// в методе Management() класса CommandLine
    /// </summary>
    public class CL_AddChar : ICommand<ConsoleKeyInfo>
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
            if (CommandLine.GetInstance().CursorPositionInLine == CommandLine.GetInstance().Line.Length)
            {
                CommandLine.GetInstance().Line += Click.KeyChar;
                CommandLine.GetInstance().CursorPositionInLine += 1;
            }
            else if (CommandLine.GetInstance().CursorPositionInLine < CommandLine.GetInstance().Line.Length)
            {
                CommandLine.GetInstance().Line = CommandLine.GetInstance().Line.Insert(CommandLine.GetInstance().CursorPositionInLine,
                    Click.KeyChar.ToString());
                CommandLine.GetInstance().CursorPositionInLine += 1;
            }

            return false;
        }
    }
}
