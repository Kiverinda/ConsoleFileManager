using System;
using System.Collections.Generic;
using System.Text;


namespace FileManager
{
    /// <summary>
    /// Класс, обрабатывающий командную строку после ввода
    /// </summary>
    public class CommandLineEnter : ICommand<ConsoleKeyInfo>                           
    {
        /// <summary>
        /// Проверка нажатия горячей клавиши
        /// </summary>
        /// <param name="click">Код горячей клавиши</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.Enter;
        }
        /// <summary>
        /// Обработка командной строки после ввода
        /// </summary>
        public bool Execute()
        {
            CommandLine commandLine = CommandLine.GetInstance();

            if (commandLine.BufferCommands.Count >= commandLine.SizeBuffer)
            {
                commandLine.BufferCommands.RemoveAt(0);
            }

            commandLine.BufferCommands.Add(commandLine.Line);
            commandLine.CommandNumberInBuffer = commandLine.BufferCommands.Count;
            commandLine.ListUserCommands = CustomMethods.SplitString(" ", commandLine.Line);
            commandLine.Action();
            commandLine.Line = "";
            commandLine.CursorPositionInLine = commandLine.Line.Length;
            Desktop.GetInstance().Update();
            new View().OldCursor(commandLine.CurrentPanel);
            return false;
        }
    }
}
