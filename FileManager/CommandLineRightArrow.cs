using System;

namespace FileManager
{
    /// <summary>
    /// Класс, перемещающий курсор вправо
    /// </summary>
    public class CommandLineRightArrow : ICommand<ConsoleKeyInfo>
    {
        /// <summary>
        /// Проверка нажатия горячей клавиши
        /// </summary>
        /// <param name="click">Код горячей клавиши</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.RightArrow;
        }

        /// <summary>
        /// Перемещение курсора вправо
        /// </summary>
        public bool Execute()
        {
            if (CommandLine.GetInstance().Line.Length > 0 && CommandLine.GetInstance().CursorPositionInLine > 0)
            {
                CommandLine.GetInstance().CursorPositionInLine ++;
            }

            return false;
        }
    }
}

