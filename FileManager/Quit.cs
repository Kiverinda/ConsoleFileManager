using System;

namespace FileManager
{
    
    /// <summary>
    /// Класс выхода из приложения с сохранением текущийх настроек пользователя  
    /// </summary>
    public class Quit : ICommand<ConsoleKeyInfo>
    {

        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.Escape;
        }

        /// <summary>
        /// Сохранение настроек пользователя и завершение работы приложения
        /// </summary>
        /// <returns>Выход из приложения</returns>
        public bool Execute()
        {
            ThreadControlSizeWindow.GetInstance().Close = true;
            UserData.GetInstance().Serialize();
            return true;
        }
    }
}
