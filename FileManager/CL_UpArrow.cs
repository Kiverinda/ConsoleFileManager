using System;


namespace FileManager
{
    /// <summary>
    /// Класс перебора комманд FIFO в буфере и добавления их в командную строку 
    /// </summary>
    public class CL_UpArrow : ICommand<ConsoleKeyInfo>
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
            if (CommandLine.GetInstance().BufferCommands.Count != 0)
            {
                if (CommandLine.GetInstance().CommandNumberInBuffer < CommandLine.GetInstance().BufferCommands.Count - 1)
                {
                    CommandLine.GetInstance().CommandNumberInBuffer++;
                    CommandLine.GetInstance().Line = CommandLine.GetInstance().BufferCommands[CommandLine.GetInstance().CommandNumberInBuffer];
                    CommandLine.GetInstance().CursorPositionInLine = CommandLine.GetInstance().Line.Length;
                }
            }

            return false;
        }
    }
}