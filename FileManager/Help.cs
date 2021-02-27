using System;


namespace FileManager
{
    
    /// <summary>
    /// Класс для вывода справки по приложению на экран
    /// </summary>
    public class Help : ICommand <ConsoleKeyInfo>
    {

        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F1;
        }

        /// <summary>
        /// Вывод справки по приложению на экран
        /// </summary>
        /// <returns>Выход из программы</returns>
        public bool Execute()
        {
            new View().Help();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            } while (key.Key != ConsoleKey.Escape);

            Desktop.GetInstance().Update();
            return false;
        }
    }
}
