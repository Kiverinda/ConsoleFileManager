using System;

namespace FileManager
{
    /// <summary>
    /// Класс, перемещающий курсор влево
    /// </summary>
    public class CommandLineLeftArrow : ICommand<ConsoleKeyInfo>
    {
        /// <summary>
        /// Проверка нажатия горячей клавиши
        /// </summary>
        /// <param name="click">Код горячей клавиши</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.LeftArrow;
        }
        /// <summary>
        /// Перемещение курсора влево
        /// </summary>
        public bool Execute()
        {
            if (CommandLine.GetInstance().Line.Length > 0 && CommandLine.GetInstance().CursorPositionInLine > 0)
            {
                CommandLine.GetInstance().CursorPositionInLine --;
            }

            return false;
        }
    }
}
