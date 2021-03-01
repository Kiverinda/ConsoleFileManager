using System;
using System.Collections.Generic;
using System.Text;


namespace FileManager
{
    /// <summary>
    /// Класс, обрабатывающий командную строку после ввода
    /// </summary>
    public class CL_Enter : ICommand<ConsoleKeyInfo>                           
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
            if (CommandLine.GetInstance().BufferCommands.Count >= CommandLine.GetInstance().SizeBuffer)
            {
                CommandLine.GetInstance().BufferCommands.RemoveAt(0);
            }
            CommandLine.GetInstance().BufferCommands.Add(CommandLine.GetInstance().Line);
            CommandLine.GetInstance().CommandNumberInBuffer = CommandLine.GetInstance().BufferCommands.Count;
            CommandLine.GetInstance().ListUserCommands = CustomMethods.SplitString(" ", CommandLine.GetInstance().Line);
            CommandLine.GetInstance().Action();
            CommandLine.GetInstance().Line = "";
            CommandLine.GetInstance().CursorPositionInLine = CommandLine.GetInstance().Line.Length;
            Desktop.GetInstance().Update();
            new View().OldCursor(CommandLine.GetInstance().CurrentPanel);
            return false;
        }
    }
}
