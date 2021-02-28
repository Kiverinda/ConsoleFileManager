using System;

namespace FileManager
{

    /// <summary>
    /// Класс выбора коммандной строки
    /// </summary>
    public class SelectCommandLine : ICommand <ConsoleKeyInfo>
    {

        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return ((click.Modifiers & ConsoleModifiers.Control) != 0) && (click.Key == ConsoleKey.Z);
        }

        /// <summary>
        /// Переключение на коммандную строку
        /// </summary>
        /// <returns>Выход из прогаммы</returns>
        public bool Execute()
        {
            View view = new View();
            view.OldCursor(Desktop.GetInstance().ActivePanel);
            CommandLine line = CommandLine.GetInstance();
            line.Management();
            view.CurrentCursor(Desktop.GetInstance().ActivePanel);
            return false;
        }
    }
}
