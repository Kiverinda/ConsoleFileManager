using System;


namespace FileManager
{
    /// <summary>
    /// Класс перебора комманд FIFO в буфере и добавления их в командную строку 
    /// </summary>
    public class CommandLineUpArrow : ICommand<ConsoleKeyInfo>
    {

        /// <summary>
        /// Проверка нажатия горячей клавиши
        /// </summary>
        /// <param name="click">Код горячей клавиши</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.UpArrow;
        }

        /// <summary>
        /// Перебор команд FIFO в буфере и добавление их в командную строку
        /// </summary>
        public bool Execute()
        {
            CommandLine commandLine = CommandLine.GetInstance();

            if (commandLine.BufferCommands.Count != 0)
            {
                if (commandLine.CommandNumberInBuffer < commandLine.BufferCommands.Count - 1)
                {
                    commandLine.CommandNumberInBuffer++;
                    commandLine.Line = commandLine.BufferCommands[commandLine.CommandNumberInBuffer];
                    commandLine.CursorPositionInLine = commandLine.Line.Length;
                }
            }

            return false;
        }
    }
}